using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
//using System.Web.Mvc;
using System.IO;
namespace OnlineExam.Controllers.API
{
   public class UploadResponse
    {
        public int uploaded;
        public string fileName;
        public string url;
        public object error;
    }
    public class QuestionBankResourceController : ApiController
    {
        [HttpGet]
        [HttpPost]
        public System.Web.Http.Results.JsonResult<UploadResponse> UploadFile()
        {
            //HttpResponseMessage result = null;
            var httpRequest = HttpContext.Current.Request;
            UploadResponse result = new UploadResponse();

            if (httpRequest.Files.Count > 0)
            {
                //var docfiles = new List<string>();
                foreach (string file in httpRequest.Files)
                {
                    var postedFile = httpRequest.Files[file];
                    string filePath;
                    string fileType = postedFile.FileName.Substring(postedFile.FileName.LastIndexOf("."));
                    string fileName;
                    do
                    {
                        fileName = Guid.NewGuid().ToString() + fileType;
                        filePath = CUrl.QuestionResourceDir + fileName;
                    }
                    while (File.Exists(filePath));
                    try
                    {
                        postedFile.SaveAs(filePath);
                        result.uploaded = 1;
                        result.url = filePath;
                        result.fileName = fileName;
                        result.uploaded = 1;
                        result.url = CUrl.QuestionResource+fileName;
                        result.fileName = fileName;
                       

                    }
                    catch
                    {
                        result.uploaded = 0;
                        result.url = "";
                        result.fileName = "";
                        result.error = "{message:'保存文件是遇到错误'}";
                    }
                    //docfiles.Add(filePath);
                }
            }
            else
            {
                result.uploaded = 0;
                result.url = "";
                result.fileName = "";
                result.error = "{message:'请求错误，未提供上传文件'}";
            }
            return Json(result);
        }

    }

}
