using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineExam.Models;
using MvcContrib.UI.Grid;

namespace OnlineExam.Controllers.Background
{
    //[Authorize(Roles = "Teacher")]
    //[Authorize(Roles = "Ediotr")]
    //[Authorize(Roles = "Ediotr")]
    [Authorize]
    public partial class ChoiceController : Controller
    {
        // GET: Choice
        Models.ExamEntities ee = new ExamEntities();
        int PageSize = CKey.DefaultPageCount;
        public virtual ActionResult Index(string searchWord, int? frequency, int? difficulty, int? audit, DataView? viewType, int? pageSize, GridSortOptions gridSortOptions, int? page, bool? isMultiple)
        {

            ViewData["ViewType"] = viewType;
            int CurrentPageSize = 0;
            if (!pageSize.HasValue)
                CurrentPageSize = (viewType == DataView.GridView ? CKey.GridViewPageCount : CKey.DataListPageCount);
            else CurrentPageSize = pageSize.Value;
            var pagedViewModel = new PagedViewModel<QuestionChoice>
            {
                ViewData = ViewData,
                Query = ee.QuestionChoice.AsQueryable(),
                GridSortOptions = gridSortOptions,
                DefaultSortColumn = "ID",
                Page = page,
                PageSize = CurrentPageSize
            }
            .AddFilter("searchWord", searchWord, a => a.Question.Contains(searchWord) || a.ModificationTeacher.Contains(searchWord) || a.OptionA.Contains(searchWord) || a.OptionA.Contains(searchWord) || a.OptionB.Contains(searchWord) || a.OptionC.Contains(searchWord) || a.OptionD.Contains(searchWord))
            //.AddFilter("genreId", genreId, a => a.GenreId == genreId, _service.GetGenres(), "Name")
            //.AddFilter("artistId", artistId, a => a.ArtistId == artistId, _service.GetArtists(), "Name")
        ;
            if (frequency.HasValue && frequency.Value != -1) { pagedViewModel.AddFilter("frequency", frequency, a => a.Frequency == frequency.Value); }

            if (difficulty.HasValue && difficulty.Value != -1) { pagedViewModel.AddFilter("difficulty", difficulty, a => a.Difficulty == difficulty.Value); }

            if (audit.HasValue && audit.Value != -1)
            {
                pagedViewModel.AddFilter("audit", audit, a => a.Audit == audit.Value);
            }
            if (isMultiple.HasValue)
            {
                pagedViewModel.AddFilter("isMultiple", isMultiple, a => a.IsMultiple == isMultiple.Value);
            }
            pagedViewModel.Setup();
            //ViewData = pagedViewModel.ViewData;
            return View(pagedViewModel);
        }
        // GET: Choice/Details/5
        public virtual ActionResult Details(long id)
        {
            ViewBag.RelateSubject = GetRelateSubject(id);

            var q = ee.QuestionChoice.Where(m => m.ID == id).SingleOrDefault();
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
        public virtual ActionResult Create(QuestionChoice Model)
        {
            ViewBag.isMultiple = Model.IsMultiple;
            string sids = Request.Params["SubjectIds"];
            if (sids == null) ModelState.AddModelError("subjectID", "所属科目不能为空");
            sids = sids.Substring(1);
            try
            {
                //Model.ID = -1; 

                Model.IsImport = false;
                Model.ModificationTeacher = SessionHelper.UserProfile.RealName;
                Model.ModificationTeacherID = User.Identity.Name;

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


                    ee.QuestionChoice.Add(Model);
                    ee.SaveChanges();
                    //AppendSubjectChoice(sids, Model.ID);
                    Model.AppendSubjects(sids);
                    // TODO: Add insert logic here

                    return RedirectToAction("Index", new { isMultiple = Model.IsMultiple });
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
                        from sq in ee.Subject_Choice
                        where s.SubjectID == sq.SubjectID && sq.QuestionID == qId
                        select s).ToList();
            return list;
        }
        // GET: Choice/Edit/5
        public virtual ActionResult Edit(long? id, bool? isMultiple)
        {

            if (id.HasValue)
            {
                ViewBag.EditType = EditType.Edit;
                ViewBag.RelateSubject = GetRelateSubject(id.Value);
                var q = ee.QuestionChoice.Where(m => m.ID == id).SingleOrDefault();
                if (q == null || q.ID != id) RedirectToAction("");
                if (isMultiple.HasValue)
                    ViewBag.isMultiple = isMultiple.Value;
                else
                    ViewBag.isMultiple = q.IsMultiple;
                return View(q);
            }
            else
            {

                if (isMultiple.HasValue)
                    ViewBag.isMultiple = isMultiple.Value;
                else ViewBag.isMultiple = false;
                ViewBag.EditType = EditType.Create;
                return View();
            }
        }
        // POST: Choice/Edit/5
        [HttpPost]
        [ValidateInput(false)]
        public virtual ActionResult Edit(QuestionChoice Model)
        {
            string sids = Request.Params["SubjectIds"];
            if (sids == null) ModelState.AddModelError("subjectID", "所属科目不能为空");
            sids = sids.Substring(1);

            ViewBag.isMultiple = Model.IsMultiple;
            Utitlity.ModelStandardize(Model);

            if (ModelState.IsValid)
            {

                Model.IsImport = false;
                Model.ModificationTeacher = SessionHelper.UserProfile.RealName;
                Model.ModificationTeacherID = User.Identity.Name;
                ee.Entry(Model).State = System.Data.Entity.EntityState.Modified;

                //if (Model.OptionD == null) { Model.OptionD = ""; }
                //if (Model.OptionE == null) { Model.OptionE = ""; }
                //if (Model.OptionF == null) { Model.OptionF = ""; }
                //if (Model.Description == null) { Model.Description = ""; }

                //AppendSubjectChoice(sids, Model.ID);
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
                var q = ee.QuestionChoice.Where(m => m.ID == id).Single();
                ee.QuestionChoice.Remove(q);
                ee.Subject_Choice.RemoveRange(ee.Subject_Choice.Where(m => m.QuestionID == id));
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
                var q = ee.QuestionChoice.Where(u => u.ID == id).Single();
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
