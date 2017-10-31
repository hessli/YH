using System;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace YH.MVC.ActionResult
{
    public class JSonpResult : System.Web.Mvc.ActionResult
    {
        public JSonpResult(object data)
        {
            _data = data;
        }
        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            var callback = context.HttpContext.Request["callback"];

            if (callback == null)
            {
                throw new ArgumentNullException("callback is error");
            }

            HttpResponseBase response = context.HttpContext.Response;

            response.ContentType = "application/json";
            response.AddHeader("Access-Control-Allow-Origin", "*");
            response.AddHeader("Access-Control-Allow-Origin", "YHToken");//允许跨域请求
            //response.ContentType = "application/json";
            response.ContentEncoding = Encoding.UTF8;

            if (this._data != null)
            {
                var jsonData = JsonConvert.SerializeObject(this._data);

                response.Write(callback + "(" + jsonData + ")");
            }
        }

        private object _data;
        public object Data
        {
            get
            {
                return _data;
            }
            set
            {
                _data = value;
            }
        }
    }
}
