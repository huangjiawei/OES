using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using OnlineExam.Models;
using System.Reflection;
using System.Text;
using System.Globalization;
//using ExampleBase;
using NetOffice;
using Word = NetOffice.WordApi;
using NetOffice.WordApi.Enums;
//using System.IO;
//using DocumentFormat.OpenXml;
//using DocumentFormat.OpenXml.Packaging;
//using DocumentFormat.OpenXml.Wordprocessing;
//using A = DocumentFormat.OpenXml.Drawing;
//using DW = DocumentFormat.OpenXml.Drawing.Wordprocessing;
//using PIC = DocumentFormat.OpenXml.Drawing.Pictures;
//using System.IO.Packaging;
namespace OnlineExam.Controllers
{
    [Authorize]
    public partial class PaperImportController : Controller
    {
        ExamEntities ee = new ExamEntities();
        //允许的文件格式
        List<string> acceptFileType = new List<string>() { ".doc", ".docx", ".wps" };
        // GET: PaperImport
        [HttpGet]
        public virtual ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public virtual ActionResult UploadFile()
        {
            var postedFile = Request.Files["DocFile"];
            if (!string.IsNullOrWhiteSpace(postedFile.FileName))
            {
                //var docfiles = new List<string>();
                //foreach (string file in Request.Files)
                //{
                string filePath;
                string fileType = postedFile.FileName.Substring(postedFile.FileName.LastIndexOf("."));
                if (!acceptFileType.Contains(fileType))
                {
                    ViewBag.FileError = "请上传指定的文件格式";
                    return View("Index");
                }
                string fileName;
                do
                {
                    fileName = Guid.NewGuid().ToString() + fileType;
                    filePath = CUrl.MapPath(CUrl.ImportPaper + fileName);
                }
                while (System.IO.File.Exists(filePath));
                //try
                //{
                postedFile.SaveAs(filePath);
                PaperImporter pi = new PaperImporter();
                var iPaper = pi.GetPaperFormFile(filePath);
                string key = Guid.NewGuid().ToString();
                ViewBag.PaperKey = key;
                Session[SessionString.ImportPaper + key] = iPaper;
                return View(iPaper);
                //}
                //catch
                //{
                //}
                //docfiles.Add(filePath);
                //}
                return View();
            }
            else
            {
                ViewBag.FileError = "请上传文件";
                return View("Index");
            }
        }
        // GET: PaperImport/Details/5
        public virtual JsonResult Delete(int id, string key)
        {
            JsonReturn jr = new JsonReturn();
            try
            {
                ImportPaper ip = (ImportPaper)Session[SessionString.ImportPaper + key];
                var q = ip.Questions.Where(m => m.ID == id).SingleOrDefault();
                ip.Questions.Remove(q);
                jr.Success = 1;
            }
            catch (Exception e)
            {
                jr.Success = 0;
                jr.Message = e.Message;
            }
            return Json(jr, JsonRequestBehavior.AllowGet);
        }
        // GET: PaperImport/Create
        public virtual JsonResult Import(int id, string key, string subjectIds)
        {
            JsonReturn jr = new JsonReturn();
            ImportPaper ip = (ImportPaper)Session[SessionString.ImportPaper + key];
            var q = ip.Questions.Where(m => m.ID == id).Single();
            object o = q.ToSpecificQuestion();
            switch (q.QType)
            {
                case QuestionType.SingleChoice:
                    ee.QuestionChoice.Add((QuestionChoice)o);
                    break;
                case QuestionType.MultipleChoice:
                    ee.QuestionChoice.Add((QuestionChoice)o);
                    break;
                case QuestionType.Completion:
                    ee.QuestionEssay.Add((QuestionEssay)o);
                    break;
                case QuestionType.Discussion:
                    ee.QuestionEssay.Add((QuestionEssay)o);
                    break;
                case QuestionType.ShortAnswer:
                case QuestionType.UnKnown:
                    ee.QuestionEssay.Add((QuestionEssay)o);
                    break;
            }
            try
            {
                ee.SaveChanges();
                if (o is QuestionEssay)
                {
                    jr.Result = ((QuestionEssay)o).ID.ToString(); jr.Message = "/Essay/Details/" + jr.Result;
                    if (string.IsNullOrWhiteSpace(subjectIds)) subjectIds = ip.SubjectId.ToString();
                    if (subjectIds != "-1")
                    {
                        //添加 关联科目
                        ((QuestionEssay)o).AppendSubjects(subjectIds);
                    }
                }
                if (o is QuestionChoice)
                {
                    jr.Result = ((QuestionChoice)o).ID.ToString(); jr.Message = "/Choice/Details/" + jr.Result;
                    if (string.IsNullOrWhiteSpace(subjectIds)) subjectIds = ip.SubjectId.ToString();
                    if (subjectIds != "-1")
                    {
                        //添加关联科目
                        ((QuestionChoice)o).AppendSubjects(subjectIds);
                    }
                }
                jr.Success = 1;
            }
            catch (Exception ex)
            {
                jr.Success = 0;
                jr.Message = ex.Message;
            }
            return Json(jr, JsonRequestBehavior.AllowGet);
        }
        // POST: PaperImport/Create
        //[HttpPost]
        //public ActionResult Create(FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add insert logic here
        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}
