﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineExam.Models;
namespace OnlineExam.Controllers.Background
{
    [Authorize]
    public partial class SubjectController : Controller
    {
        // GET: Subject
        ExamEntities ee = new ExamEntities();
        public virtual ActionResult Index()
        {
            JsonPaser jp = new JsonPaser();
            List<Major> majors;
            List<Subject> subjects;
            ViewBag.Tree = jp.GetSubjectTree(out majors, out subjects);
            ViewBag.Majors = majors;
            ViewBag.Subjects = subjects;
            return View();
        }
        // GET: Subject/Details/5
        public virtual ActionResult Details(int id)
        {
            return View();
        }
        [HttpPost]
        public virtual JsonResult AppendSubject(string subjectId, string majorId)
        {
            JsonReturn jr = new JsonReturn();
            Major_Subject ms = new Major_Subject();
            try
            {
                ms.MajorID = int.Parse(majorId);
                ms.SubjectID = int.Parse(subjectId);
                ee.Major_Subject.Add(ms);
                ee.SaveChanges();
                jr.Success = 1;
            }
            catch (Exception ex)
            {
                jr.Success = 0;
                jr.Message = ex.Message;
            }
            return Json(jr, JsonRequestBehavior.AllowGet);
        }
        // POST: Subject/Create
        [HttpPost]
        public virtual ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        // GET: Subject/Edit/5
        [HttpPost]
        public virtual ActionResult RemoveSubject(string subjectId, string majorId)
        {
            JsonReturn jr = new JsonReturn();
            try
            {
                int mid = int.Parse(majorId);
                int sid = int.Parse(subjectId);
                var temp = ee.Major_Subject.Where(ms => ms.MajorID == mid &&
           ms.SubjectID == sid).ToList();
                if (temp.Count > 0)
                {
                    ee.Major_Subject.Remove(temp[0]);
                    ee.SaveChanges();
                    jr.Success = 1;
                }
            }
            catch (Exception ex)
            {
                jr.Success = 0;
                jr.Message = ex.Message;
            }
            return Json(jr, JsonRequestBehavior.AllowGet);
        }
        class SubjectItem
        {
            public int SubjectID { get; set; }
            public int SubjectName { get; set; }
        }
        static List<SelectListItem> majorList;
        public JsonResult GetSubjectByMajor(int id)
        {
            JsonReturn jr = new JsonReturn();
            var list = (from s in ee.Subject
                        from ms in ee.Major_Subject
                        where (ms.MajorID == id || id == 0) && s.SubjectID == ms.SubjectID
                        select new { SubjectID = s.SubjectID, SubjectName = s.SubjectName }).ToList();
            jr.Success = 1;
            jr.Data = new { lenght = list.Count(), list = list };
            //jr.Data = nECew { { SubjectID = s.SubjectID, SubjectName = s.SubjectName },{ SubjectID = s.SubjectID, SubjectName = s.SubjectName }
            //};
            return Json(jr, JsonRequestBehavior.AllowGet);
        }
        public virtual ActionResult SubjectDropDown(int? MajorId, string MajorName)
        {
            try
            {
                if (!MajorId.HasValue)
                {
                    majorList = new SelectList(ee.Major.AsEnumerable(), "MajorID", "MajorName").ToList();
                    var item = new SelectListItem();
                    item.Text = "全部"; item.Value = "0";
                    majorList.Insert(0, item);
                    ViewBag.SubjectList = new SelectList(ee.Subject.AsEnumerable(), "SubjectID", "SubjectName").ToList().AsEnumerable();
                    ViewBag.MajorName = "全部";
                    ViewBag.MajorId = 0;
                }
                else
                {
                    var list = from s in ee.Subject
                               from ms in ee.Major_Subject
                               where (MajorId.Value == 0 || ms.MajorID == MajorId.Value) && s.SubjectID == ms.SubjectID
                               select s;
                    ViewBag.SubjectList = new SelectList(list, "SubjectID", "SubjectName");
                    ViewBag.MajorId = MajorId.Value;
                    ViewBag.MajorName = MajorName;
                }
                ViewBag.MajorList = majorList;
                return View();
            }
            catch
            {
                return View("获取科目列表是遇到错误");
            }
        }
        [HttpPost]
        public virtual ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        // GET: Subject/Delete/5
        public virtual ActionResult Delete(int id)
        {
            return View();
        }
        // POST: Subject/Delete/5
        [HttpPost]
        public virtual ActionResult Delete(int id, FormCollection collection)
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
