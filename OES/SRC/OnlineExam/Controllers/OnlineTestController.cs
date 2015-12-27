using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineExam;
using OnlineExam.Models;
namespace OnlineExam.Controllers
{
    public class OnlineTestController : Controller
    {
        ExamEntities ee = new ExamEntities();
        // GET: OnlineTest
        public ActionResult Index()
        {

            return View();
        }

        // GET: OnlineTest/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }
        public JsonResult GetPapers(int subjectId)
        {
            JsonReturn jr = new JsonReturn();
            try
            {
                var query = from p in ee.TestPaper.Where(m => m.SubjectID == subjectId)
                            select new { ID = p.ID, Name = p.Name };
                jr.Data = query.ToList();
                jr.Success = 1;
            }
            catch (Exception ex)
            {
                jr.Message = ex.Message;
                jr.Success = 0;
            }
            return Json(jr, JsonRequestBehavior.AllowGet);
        }
        public ContentResult GetTime()
        {
            TimeSpan tspan = (DateTime)Session[SessionString.TestEndTime] - DateTime.Now;
            var min = Math.Round(tspan.TotalMinutes, 0);
            if (min <= 0) return Content(CKey.TestEnd);
            return Content("剩余时间：" + min + "分");
        }
        [HttpPost]
        public void SaveAnswer()
        {
            var pg = SessionHelper.TestPaper;
            var count = pg.Essay.Count() + pg.Choice.Count();
            for (int i = 1; i < count; i++)
            {
                SessionHelper.TestAnswerList[i - 1] = Request.Params["answer" + i.ToString()];

            }
        }
        [HttpPost]
        public void SubmitPaper()
        {
            var pg = SessionHelper.TestPaper;
            var count = pg.Essay.Count() + pg.Choice.Count();
            for (int i = 1; i < count; i++)
            {
                SessionHelper.TestAnswerList[i - 1] = Request.Params["answer" + i.ToString()];
            }
           var answerlist=   pg.GenerateAnswerList(SessionHelper.TestAnswerList);
            pg.Score_Choice(answerlist);

        }
        // GET: OnlineTest/Create
        public ActionResult Test(int
            subjectid, int? paperid)
        {
          
            if (!paperid.HasValue)
            {
                PaperGenerate pg = new PaperGenerate();
                pg.DefaultSetting();
                //pg.Paper.TimeDuration = 120;
                pg.Paper.SubjectID = subjectid;

                pg.AutoGenerate();
#if !DEBUG
                if (Session[SessionString.TestEndTime] == null)
                {
#endif
               
                
                Session[SessionString.TestEndTime] = DateTime.Now.AddMinutes(pg.Paper.TimeDuration.Value);
                SessionHelper.TestPaper = pg;
                SessionHelper.TestAnswerList = new string[pg.Essay.Count() + pg.Choice.Count()];
                return View(pg);
#if !DEBUG
            }
                else
                {
                    return Content("系统检测到你正在进行其他测试，请先完成该测试！");
                }
#endif
            }
            else
            {

            }

            return View();
        }

        // POST: OnlineTest/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
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

        // GET: OnlineTest/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: OnlineTest/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
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

        // GET: OnlineTest/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: OnlineTest/Delete/5
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
