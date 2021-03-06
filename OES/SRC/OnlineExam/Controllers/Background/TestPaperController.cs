﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineExam.Models;
using MvcContrib.UI.Grid;
namespace OnlineExam.Controllers.Background
{
    [Authorize]
    public class TestPaperController : Controller
    {
        // GET: PaperClip
        Models.ExamEntities ee = new ExamEntities();
        int PageSize = CKey.DefaultPageCount;
        public virtual ActionResult Index(string searchWord, int? audit, int? active, string examType
            , bool? isReady, int? subjectId, DateTime? startDate, DateTime? endDate, int? startDuration, int? endDuration
          , string teacherId, DataView? viewType, int? pageSize, GridSortOptions gridSortOptions, int? page)
        {
            ViewData["ViewType"] = viewType;
            int CurrentPageSize = 0;
            if (!pageSize.HasValue)
                CurrentPageSize = (viewType == DataView.GridView ? CKey.GridViewPageCount : CKey.DataListPageCount);
            else CurrentPageSize = pageSize.Value;
            var pagedViewModel = new PagedViewModel<TestPaper>
            {
                ViewData = ViewData,
                Query = ee.TestPaper.AsQueryable(),
                GridSortOptions = gridSortOptions,
                DefaultSortColumn = "ID",
                Page = page,
                PageSize = CurrentPageSize
            }
            .AddFilter("searchWord", searchWord, a => a.Name.Contains(searchWord) || a.ModificationTeacher.Contains(searchWord) || a.Info.Contains(searchWord) || a.ExamType.Contains(searchWord));
            if (active.HasValue)
            {
                pagedViewModel.AddFilter("active", active, a => a.Active == active.Value);
            }
            if (!string.IsNullOrWhiteSpace(examType))
            {
                pagedViewModel.AddFilter("examType", examType, a => a.ExamType == examType);
            }
            if (!string.IsNullOrWhiteSpace(teacherId))
            {
                pagedViewModel.AddFilter("teacherId", teacherId, a => a.ModificationTeacherID == teacherId);
            }
            if (audit.HasValue && audit.Value != -1)
            {
                pagedViewModel.AddFilter("audit", audit, a => a.Audit == audit.Value);
            }
            if (isReady.HasValue)
            {
                pagedViewModel.AddFilter("isReady", isReady, a => a.IsReady == isReady.Value);
            }
            if (subjectId.HasValue)
            {
                pagedViewModel.AddFilter("subjectId", subjectId, a => a.SubjectID == subjectId.Value);
            }
            if (startDuration.HasValue)
            {
                pagedViewModel.AddFilter("startDuration", startDuration, a => a.TimeDuration > startDuration.Value);
            }
            if (endDuration.HasValue)
            {
                pagedViewModel.AddFilter("endDuration", endDuration, a => a.TimeDuration < endDuration.Value);
            }
            if (startDate.HasValue)
            {
                pagedViewModel.AddFilter("startDate", startDate, a => a.ExamTime > startDate.Value);
            }
            if (endDate.HasValue)
            {
                pagedViewModel.AddFilter("endDate", endDate, a => a.ExamTime > endDate.Value);
            }
            pagedViewModel.Setup();
            ViewBag.SubjectList = ee.Subject.ToList();
            //ViewData = pagedViewModel.ViewData;
            return View(pagedViewModel);
        }
        // GET: PaperClip/Details/5
        public ActionResult Details(string id)
        {
            var q = ee.TestPaper.Where(m => m.ID == id).SingleOrDefault();
            if (q == null || q.ID != id) return HttpNotFound();
            return View(q);
        }
        // GET: PaperClip/Create
        public ActionResult EditQuestionList(string id)
        {
            var q = ee.TestPaper.Where(m => m.ID == id).SingleOrDefault();
            if (q == null || q.ID != id) return HttpNotFound();

            return View(q);
        }
        [HttpPost]
        public JsonResult EditQuestionList(string content, string ID)
        {
            JsonReturn jr = new JsonReturn();
            try {
                ee.Paper_Choice.RemoveRange(ee.Paper_Choice.Where(m => m.PaperID == ID));
                ee.Paper_Essay.RemoveRange(ee.Paper_Essay.Where(m => m.PaperID == ID));
                ee.Paper_QuestionCategory.RemoveRange(ee.Paper_QuestionCategory.Where(m => m.PaperID == ID));
                ee.SaveChanges();
                var jsCateList = JsonHelper.JsonDeserialize<List<QuestionCategoryJson>>(Server.HtmlDecode(content));
                var cateList = new List<Paper_QuestionCategory>();
                var choiceList = new List<Paper_Choice>();
                var essayList = new List<Paper_Essay>();
                foreach (var cate in jsCateList)
                {
                    Paper_QuestionCategory qc = new Paper_QuestionCategory();
                    qc.PaperID = ID;
                    qc.QuestionType = cate.QuestionType;
                    qc.Title = cate.Title;
                    qc.Sequence = cate.Sequence;
                    qc.ScorePerQuestion = cate.ScorePerQuestion;
                    if (cate.QuestionType == QuestionType.SingleChoice.ToString() || cate.QuestionType == QuestionType.MultipleChoice.ToString())
                    {
                        foreach (var q in cate.ChoiceList)
                        {
                            Paper_Choice pc = new Paper_Choice();
                            pc.SmallQuestionNumber = q.SmallQuestionNumber;
                            pc.Score = q.Score;
                            pc.QuestionID = q.ID;
                            pc.PaperID = ID;
                            pc.BigQuestionNumber = cate.Sequence;
                            choiceList.Add(pc);
                        }
                        qc.Quantity = cate.ChoiceList.Count();
                    }
                    else
                    {
                        foreach (var q in cate.EssayList)
                        {
                            Paper_Essay pe = new Paper_Essay();
                            pe.SmallQuestionNumber = q.SmallQuestionNumber;
                            pe.Score = q.Score;
                            pe.QuestionID = q.ID;
                            pe.PaperID = ID;
                            pe.BigQuestionNumber = cate.Sequence;
                            essayList.Add(pe);
                        }
                        qc.Quantity = cate.EssayList.Count();
                     
                    }
                    cateList.Add(qc);
                }
                ee.Paper_QuestionCategory.AddRange(cateList);
                ee.Paper_Choice.AddRange(choiceList);
                ee.Paper_Essay.AddRange(essayList);
                ee.SaveChanges();
                jr.Success = 1;
            }catch(Exception ex)
            {
                jr.Success = 0;
                jr.Message = ex.Message;
            }
            return Json(jr, JsonRequestBehavior.AllowGet);
        }
        // POST: PaperClip/Create
        [HttpPost]
        public ActionResult Create(TestPaper model)
        {
            try
            {
                model.ModificationTeacher = SessionHelper.UserProfile.RealName;
                model.ModificationTeacherID = SessionHelper.UserProfile.UserId;
                model.ModificationDate = DateTime.Now;
                model.ID = Guid.NewGuid().ToString();
                model.Assessment = "";
                if (ModelState.IsValid)
                {
                    string cateList = Request.Params["QuestionCategory"] as string;
                    if (!string.IsNullOrWhiteSpace(cateList))
                    {
                        var list = cateList.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                        foreach (var s in list)
                        {
                            Paper_QuestionCategory cate = JsonHelper.JsonDeserialize<Paper_QuestionCategory>(s);
                            model.Paper_QuestionCategory.Add(cate);
                        }
                        ee.TestPaper.Add(model);
                    }
                    ee.SaveChanges();
                }
                //return View("Details", model);
                return RedirectToAction("Details", new { id = model.ID });
            }
            catch(Exception ex)
            {
                //throw ex;
                ModelState.AddModelError("SaveError", ex.Message);
                ViewBag.EditType = EditType.Create;
                return View("Edit",model);
            }
        }
        public ActionResult Edit(string id)
        {
            if (!string.IsNullOrWhiteSpace(id))
            {
                ViewBag.EditType = EditType.Edit;
                var q = ee.TestPaper.Where(m => m.ID == id).SingleOrDefault();
                if (q == null || q.ID != id) return HttpNotFound();
                return View(q);
            }
            else
            {
                ViewBag.EditType = EditType.Create;
                return View(new TestPaper());
            }
        }
        [HttpPost]
        public ActionResult Edit(TestPaper model)
        {
            try
            {
                model.ModificationTeacher = SessionHelper.UserProfile.RealName;
                model.ModificationTeacherID = SessionHelper.UserProfile.UserId;
                model.ModificationDate = DateTime.Now;
                model.Active = Activity.Active;
                model.Audit = Auditing.Unreviewed;
                //model.SubjectID; 
                //model.ID = Guid.NewGuid().ToString();
                if (model.Assessment == null)
                    model.Assessment = "";
                //model.Assessment = "";
                if (ModelState.IsValid)
                {
                    ee.Entry(model).State = System.Data.Entity.EntityState.Modified;
                    //ee.SaveChanges();
                    //重新设置题型
                    ee.Paper_QuestionCategory.RemoveRange(ee.Paper_QuestionCategory.Where(m => m.PaperID == model.ID));
                    string cateList = Request.Params["QuestionCategory"] as string;
                    if (!string.IsNullOrWhiteSpace(cateList))
                    {
                        var list = cateList.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                        foreach (var s in list)
                        {
                            Paper_QuestionCategory cate = JsonHelper.JsonDeserialize<Paper_QuestionCategory>(s);
                            cate.PaperID = model.ID;
                            cate.Quantity = 5;
                            ee.Paper_QuestionCategory.Add(cate);
                        }
                        ee.SaveChanges();
                    }
                    return RedirectToAction("Details", new { id = model.ID });
                }
            }
            catch (Exception ex)
            {
                ViewBag.EditType = EditType.Edit;
#if DEBUG
                throw ex;
#endif
            }
            return View(model);
        }
        // GET: PaperClip/Delete/5
        [HttpPost]
        public JsonResult Delete()
        {
            try
            {
                string id = Request.Params["ID"];
                //var q = ee.TestPaper.Where(m => m.ID == id).Single();
                //ee.Paper_QuestionCategory.RemoveRange(q.Paper_QuestionCategory);
                ee.Paper_Choice.RemoveRange(ee.Paper_Choice.Where(m => m.PaperID == id));
                ee.Paper_Essay.RemoveRange(ee.Paper_Essay.Where(m => m.PaperID == id));
                ee.Paper_QuestionCategory.RemoveRange(ee.Paper_QuestionCategory.Where(m => m.PaperID == id));
                ee.TestPaper.RemoveRange(ee.TestPaper.Where(m => m.ID == id));
                ee.SaveChanges();
                JsonReturn jr = new JsonReturn();
                jr.Success = 1;
                return Json(jr, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                JsonReturn jr = new JsonReturn();
                jr.Success = 0;
                jr.Message = ex.Message;
                return Json(jr, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public virtual JsonResult Censor()
        {
            try
            {
                var fc = HttpContext.Request.Params;
                string id = fc["ID"];
                int ad = Convert.ToInt16(fc["Audit"]);
                string ass = Convert.ToString(fc["Assessment"]);
                var q = ee.TestPaper.Where(u => u.ID == id).Single();
                q.Audit = ad == Auditing.Pass ? Auditing.Pass : Auditing.Fail;
                q.Assessment = ass;
                ee.Entry(q).State = System.Data.Entity.EntityState.Modified;
                ee.SaveChanges();
                JsonReturn jr = new JsonReturn();
                jr.Success = 1;
                return Json(jr, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                JsonReturn jr = new JsonReturn();
                jr.Success = 0;
                return Json(jr, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
