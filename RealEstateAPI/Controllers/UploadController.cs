using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace RealEstateAPI.Controllers
{
    public class UploadController : ApiController
    {
        private List<string> ListExtension = new List<string>
        {
            "xlsx", "csv"
        };

        public string Files()
        {
            try
            {
                foreach (string fileKey in HttpContext.Current.Request.Files)
                {
                    HttpPostedFile file = HttpContext.Current.Request.Files[fileKey];
                    if (file != null)
                    {
                        var ran = new Random();
                        string extension = System.IO.Path.GetExtension(file.FileName);
                        if (!ListExtension.Any(m => m.ToLower() == extension.Substring(1, extension.Length - 1).ToLower()))
                        {
                            return "False";
                        }
                        string uploadFileName = file.FileName;
                        if (uploadFileName.IndexOf(@"\") > 0)
                        {
                            uploadFileName = uploadFileName.Substring(uploadFileName.LastIndexOf("\\") + 1);
                        }
                        string Name = System.IO.Path.GetFileNameWithoutExtension(uploadFileName);
                        Name = Name.Replace(" ", "");
                        string fileName = DateTime.Now.ToString("yyyyMMddhhmmss") + "_" + ran.Next(1000, int.MaxValue) + "_PhatSandaru_" + Name;
                        string filePath = "~/Uploads/Temp/" + fileName + extension;
                        file.SaveAs(HttpContext.Current.Server.MapPath(filePath));
                        return filePath.Replace("~/", "");
                        //return fileName + extension;
                    }
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return "False";
        }
    }
}
