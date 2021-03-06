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
    public partial class EssayController : Controller
    {
        // GET: Choice
        Models.ExamEntities ee = new ExamEntities();
        int PageSize = CKey.DefaultPageCount;
        public virtual ActionResult Index(string searchWord, int? frequency, int? difficulty, int? audit, DataView? viewType, int? pageSize, GridSortOptions gridSortOptions, int? page, QuestionType? questionType)
        {
            ViewData["ViewType"] = viewType;
            int CurrentPageSize = 0;
            if (!pageSize.HasValue)
                CurrentPageSize = (viewType == DataView.GridView ? CKey.GridViewPageCount : CKey.DataListPageCount);
            else CurrentPageSize = pageSize.Value;
            var pagedViewModel = new PagedViewModel<QuestionEssay>
            {
                ViewData = ViewData,
                Query = ee.QuestionEssay.AsQueryable(),
                GridSortOptions = gridSortOptions,
                DefaultSortColumn = "ID",
                Page = page,
                PageSize = CurrentPageSize
            }
            .AddFilter("searchWord", searchWord, a => a.Question.Contains(searchWord) || a.ModificationTeacher.Contains(searchWord) || a.Answer.Contains(searchWord))
            //.AddFilter("genreId", genreId, a => a.GenreId == genreId, _service.GetGenres(), "Name")
            //.AddFilter("artistId", artistId, a => a.ArtistId == artistId, _service.GetArtists(), "Name")
        ;
            if (frequency.HasValue && frequency.Value != -1) { pagedViewModel.AddFilter("frequency", frequency, a => a.Frequency == frequency.Value); }
            if (difficulty.HasValue && difficulty.Value != -1) { pagedViewModel.AddFilter("difficulty", difficulty, a => a.Difficulty == difficulty.Value); }
            if (audit.HasValue && audit.Value != -1)
            {
                pagedViewModel.AddFilter("audit", audit, a => a.Audit == audit.Value);
            }
            if (questionType.HasValue)
            {
                pagedViewModel.AddFilter("questionType", questionType, a => a.QuestionType == questionType.Value.ToString());
            }
            pagedViewModel.Setup();
            //ViewData = pagedViewModel.ViewData;
            return View(pagedViewModel);
        }
        // GET: Choice/Details/5
        public virtual ActionResult Details(int id)
        {
            ViewBag.RelateSubject = GetRelateSubject(id);
            var q = ee.QuestionEssay.Where(m => m.ID == id).SingleOrDefault();
            if (q == null || q.ID != id) RedirectToAction("");
            Utitlity.ModelStandardize(q);
            return View(q);
        }
        //public ActionResult Create()
        //{
        //    return View();
        //}
        //public ActionResult Edit(EditType et)
        //{
        //    ViewBag.EditType = et;
        //    return View();
        //}
        // POST: Choice/Create
        [HttpPost]
        [ValidateInput(false)]
        public virtual ActionResult Create(QuestionEssay Model)
        {
            ViewBag.questionType = Model.QuestionType;
            string sids = Request.Params["SubjectIds"];
            if (sids == null) ModelState.AddModelError("subjectID", "所属科目不能为空");
            sids = sids.Substring(1);
            try
            {
                //Model.ID = -1; 
                Utitlity.ModelStandardize(Model);
                Model.DateAdded = DateTime.Now;
                Model.ModificationDate = DateTime.Now;
                if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
                    Model.ModificationTeacher = System.Web.HttpContext.Current.User.Identity.Name;
                else
                {
                    Model.ModificationTeacher = "System";
                }
                if (ModelState.IsValid)
                {
                    ee.QuestionEssay.Add(Model);
                    ee.SaveChanges();
                    //AppendSubjectEssay(sids, Model.ID);
                    Model.AppendSubjects(sids);
                    ee.SaveChanges();
                    // TODO: Add insert logic here
                    return RedirectToAction("Index", new { questionType = Model.QuestionType });
                }
                else return View("Edit", Model);
            }
            catch (Exception e)
            {
                //throw e;
                return View("Edit", Model);
            }
        }
        List<Subject> GetRelateSubject(long qId)
        {
            var list = (from s in ee.Subject
                        from sq in ee.Subject_Essay
                        where s.SubjectID == sq.SubjectID && sq.QuestionID == qId
                        select s).ToList();
            return list;
        }
        // GET: Choice/Edit/5
        public virtual ActionResult Edit(int? id, string questionType)
        {
            //var subjectList = ee.Subject.ToList();
            //var majorList = ee.Subject.ToList();
            if (id.HasValue)
            {
                ViewBag.RelateSubject = GetRelateSubject(id.Value);
                ViewBag.EditType = EditType.Edit;
                var q = ee.QuestionEssay.Where(m => m.ID == id).SingleOrDefault();
                if (q == null || q.ID != id) RedirectToAction("");
                if (!string.IsNullOrWhiteSpace(questionType))
                    ViewBag.questionType = questionType;
                else
                    ViewBag.questionType = q.QuestionType;
                return View(q);
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(questionType))
                    ViewBag.questionType = questionType;
                else ViewBag.questionType = false;
                ViewBag.EditType = EditType.Create;
                return View();
            }
        }
        // POST: Choice/Edit/5
        [HttpPost]
        [ValidateInput(false)]
        public virtual ActionResult Edit(QuestionEssay Model)
        {
            ViewBag.questionType = Model.QuestionType;
            Utitlity.ModelStandardize(Model);
            string sids = Request.Params["SubjectIds"];
            Model.ModificationTeacher = SessionHelper.UserProfile.RealName;
            Model.ModificationTeacherID = SessionHelper.UserProfile.UserId;
            Model.ModificationDate = DateTime.Now;
            //Model.ModificationTeacher=
            if (sids == null) ModelState.AddModelError("subjectID", "所属科目不能为空");
            sids = sids.Substring(1);
            if (ModelState.IsValid)
            {
                ee.Entry(Model).State = System.Data.Entity.EntityState.Modified;
                //if (Model.OptionD == null) { Model.OptionD = ""; }
                //if (Model.OptionE == null) { Model.OptionE = ""; }
                //if (Model.OptionF == null) { Model.OptionF = ""; }
                //if (Model.Description == null) { Model.Description = ""; }
                //AppendSubjectEssay(sids, Model.ID);
                Model.AppendSubjects(sids);
                ee.SaveChanges();
                return RedirectToAction("Details", new { id = Model.ID });
            }
            else
            {
                return View(Model);
            }
        }
        [HttpPost]
        public virtual JsonResult Delete()
        {
            try
            {
                // TODO: Add delete logic here
                int id = Convert.ToInt32(Request.Params["ID"]);
                var q = ee.QuestionEssay.Where(m => m.ID == id).Single();
                ee.QuestionEssay.Remove(q);
                ee.Subject_Essay.RemoveRange(ee.Subject_Essay.Where(m => m.QuestionID == id));
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
        [HttpPost]
        public virtual JsonResult Censor()
        {
            try
            {
                var fc = HttpContext.Request.Params;
                long id = Convert.ToInt64(fc["ID"]);
                int ad = Convert.ToInt16(fc["Audit"]);
                string ass = Convert.ToString(fc["Assessment"]);
                var q = ee.QuestionEssay.Where(u => u.ID == id).Single();
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
        public virtual JsonResult GetQuestion(long id, string qtype, int subjectId)
        {
            JsonReturn jr = new JsonReturn();
            try
            {
                if (qtype.IndexOf("Choice") >= 0)
                {
                    var isMultiple = qtype == QuestionType.MultipleChoice.ToString() ? true : false;
#if !DEBUG
                    var q = (from c in ee.QuestionChoice
                             from sc in ee.Subject_Choice
                             where c.ID == id && isMultiple == c.IsMultiple && sc.QuestionID == c.ID && sc.SubjectID == subjectId
                             select c).SingleOrDefault();
#else
                    var q = ee.QuestionChoice.Where(m=> m.ID == id&& isMultiple == m.IsMultiple).SingleOrDefault();
#endif
                    if (q != null)
                    {
                        jr.Data = new ChoiceJson
                        {
                            SmallQuestionNumber = -1,
                            BigQuestionNumber = -1,
                            Score = -1,
                            Question = q.Question == null ? "" : q.Question.Replace("\r", "").Replace("\r\n", "").Replace("\n", ""),
                            IsMultiple = q.IsMultiple,
                            OptionA = q.OptionA,
                            OptionB = q.OptionB,
                            OptionC = q.OptionC,
                            OptionD = q.OptionD,
                            OptionE = q.OptionE,
                            OptionF = q.OptionF,
                            AisTrue = q.AisTrue,
                            BisTrue = q.BisTrue,
                            CisTrue = q.CisTrue,
                            DisTrue = q.DisTrue,
                            EisTrue = q.EisTrue,
                            FisTrue = q.FisTrue,
                            Info = q.Info == null ? "" : q.Info.Replace("\r", "").Replace("\r\n", "").Replace("\n", ""),
                            Description = q.Description == null ? "" : q.Description.Replace("\r", "").Replace("\r\n", "").Replace("\n", ""),
                            Difficulty = q.Difficulty,
                            QuestionType = q.IsMultiple ? QuestionType.MultipleChoice.ToString() : QuestionType.SingleChoice.ToString(),
                            ID = q.ID,
                            Frequency = q.Frequency


                        };

                        jr.Success = 1;
                    }
                    else { jr.Success = 0; jr.Message = "题目不存在"; }
                }
                else
                {
#if !DEBUG
                    var q = (from c in ee.QuestionEssay
                             from sc in ee.Subject_Essay
                             where c.ID == id && qtype == c.QuestionType && sc.QuestionID == c.ID && sc.SubjectID == subjectId
                             select c).SingleOrDefault();
#else
                            var q = ee.QuestionEssay.Where(m=> m.ID == id&&m.QuestionType==qtype).SingleOrDefault();
             
#endif
                      if (q != null)
                    {
                        jr.Data = new EssayJson
                        {
                            SmallQuestionNumber = -1,
                            BigQuestionNumber = -1,
                            Score = -1,
                            Question = q.Question == null ? "" : q.Question.Replace("\r", "").Replace("\r\n", "").Replace("\n", ""),
                            Answer = q.Answer == null ? "" : q.Answer.Replace("\r", "").Replace("\r\n", "").Replace("\n", ""),
                            Info = q.Info == null ? "" : q.Info.Replace("\r", "").Replace("\r\n", "").Replace("\n", ""),
                            Description = q.Description == null ? "" : q.Description.Replace("\r", "").Replace("\r\n", "").Replace("\n", ""),
                            Difficulty = q.Difficulty,
                            QuestionType = q.QuestionType,
                            ID = q.ID,
                            Frequency = q.Frequency
                        };
                        jr.Success = 1;
                    }
                    else { jr.Success = 0; jr.Message = "题目不存在"; }
                }
            }
            catch (Exception ex)
            {
                jr.Success = 0;
                jr.Message = ex.Message;

            }
            return Json(jr, JsonRequestBehavior.AllowGet);

        }
    }
}
