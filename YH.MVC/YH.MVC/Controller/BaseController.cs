using YH.MVC.ActionResult;
using YH.MVC.Controller.ActionResult;
using System.Web.Mvc;

namespace YH.MVC
{
    public  class BaseController: System.Web.Mvc.Controller
    {
        public JSonpResult JSonp(object data)
        {

            JSonpResult result = new JSonpResult(data);

            return result;
        }

        public  NetJsonResult  Json(object data)
        {
            NetJsonResult result = new NetJsonResult(data, JsonRequestBehavior.AllowGet);
            return result;
        }

        public NetJsonResult Json(object data , JsonRequestBehavior behavior)
        {
            NetJsonResult result = new NetJsonResult(data, behavior);
            return result;
        }
    }   

}
