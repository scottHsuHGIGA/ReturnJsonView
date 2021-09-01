using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ReturnJsonView.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult TestViewToString()
        {
            string model = "我是model";
            ViewData["TestViewData"] = "我是ViewData";
            return Json(new
            {
                TestView = ConvertViewToString("_TestPartial", model, ViewData)
            });
        }

        /// <summary>
        /// 轉換View To String
        /// </summary>
        /// <param name="viewName">View名稱</param>
        /// <param name="model">model</param>
        /// <param name="viewData">viewData</param>
        /// <returns></returns>
        private string ConvertViewToString(string viewName, object model = null, ViewDataDictionary viewData = null)
        {
            using (StringWriter writer = new StringWriter())
            {
                if (viewData == null)
                {
                    ViewData.Model = model;
                    viewData = ViewData;
                }
                else
                {
                    viewData.Model = model;
                }

                ViewEngineResult vResult = ViewEngines.Engines.FindPartialView(ControllerContext, viewName);
                ViewContext vContext = new ViewContext(this.ControllerContext, vResult.View, viewData, new TempDataDictionary(), writer);
                vResult.View.Render(vContext, writer);
                return writer.ToString();
            }
        }

    }
}