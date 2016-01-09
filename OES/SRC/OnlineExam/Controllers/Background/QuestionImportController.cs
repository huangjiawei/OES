using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Office.Tools.Word;
namespace OnlineExam.Controllers.Background
{
    [Authorize]
    public partial class QuestionImportController : Controller
    {
        // GET: QuestionImport
        public virtual ActionResult Index()
        {
            //_Application app = new 
            ////创建word文档
            //_Document doc = null;
            //object unknow = Type.Missing;
            //doc = app.Documents.Open(ref fileName,
            //               ref unknow, ref unknow, ref unknow, ref unknow, ref unknow,
            //               ref unknow, ref unknow, ref unknow, ref unknow, ref unknow,
            //               ref unknow, ref unknow, ref unknow, ref unknow, ref unknow);
            return View();
        }
    }
}