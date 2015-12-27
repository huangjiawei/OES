using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineExam.Models;
using MvcContrib.UI.Grid;

namespace OnlineExam.Controllers.Background
{
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
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PaperClip/Create
        public ActionResult Create()
        {

            return View();
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
               
                if (ModelState.IsValid)
                {
                    string cateList = Request.Params["QuestionCategory"] as string;
                    if (!string.IsNullOrWhiteSpace(cateList))
                    {
                        var list= cateList.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                        foreach(var s in list)
                        {
                            Paper_QuestionCategory cate = JsonHelper.JsonDeserialize<Paper_QuestionCategory>(s);
                            model.Paper_QuestionCategory.Add(cate);

                        }
                        ee.TestPaper.Add(model);
                        ee.SaveChanges();
                    }


                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
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
        // POST: PaperClip/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: PaperClip/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PaperClip/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
