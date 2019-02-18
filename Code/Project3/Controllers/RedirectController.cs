using System.Web.Mvc;
using Glass.Mapper.Sc.Web.Mvc;
using System;
using System.Web;
using System.Net;

namespace BGSitecore.Controllers
{
    public class RedirectController : GlassController
    {
        public ActionResult RedirectPage()
        {
            var redirectModel = GetLayoutItem<BGSitecore.Models.RedirectPage>();
            return redirectModel!=null?Redirect(redirectModel.RedirectUrl.Url): null;
            //return View();
        }

        [HttpPost]
        public HttpResponseBase GetFileContentAsPDf()
        {
            try
            {
                string nordisPath = Server.HtmlEncode(Request.QueryString["nApi"]);
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                var request = WebRequest.Create(nordisPath);
                Response.Clear();
                using (var webResponse = request.GetResponse())
                {
                    using (var webStream = webResponse.GetResponseStream())
                    {
                        if (webStream != null)
                        {
                            
                            Response.BufferOutput = true;
                            Response.ContentType = "application/pdf";
                            Response.AppendHeader("Content-Disposition", "attachment;");
                            Response.Cache.SetCacheability(HttpCacheability.NoCache);
                            webStream.CopyTo(Response.OutputStream);
                            Response.Flush();
                            Response.End();
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Sitecore.Diagnostics.Log.Error("RedirectController.GetFileContentAsPDf - Error while getting pdf content:", ex);
                Response.Clear();
                Response.BufferOutput = false;
                Response.Flush();
            }
            return Response;
        }
    }
}