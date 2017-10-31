using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Web;
using System.Web.Mvc;
using YH.Utils;

namespace YH.MVC.Controller.ActionResult
{
    public  class NetJsonResult: System.Web.Mvc.ActionResult
    {
        private object _data = null;
        private JsonRequestBehavior _requestBehavior = JsonRequestBehavior.DenyGet;
        private static IsoDateTimeConverter ISO_TIME_CONVERT = new IsoDateTimeConverter { DateTimeFormat = "yyyy-MM-dd HH:mm:ss" };

        public NetJsonResult(object data, JsonRequestBehavior jsonRequestBehavior)
        {
            this._data = data;
            this._requestBehavior = jsonRequestBehavior;
        }



        public object Data
        {
            get
            {
                return this._data;
            }
        }

        public JsonRequestBehavior JsonRequestBehavior
        {
            get
            {
                return this._requestBehavior;
            }
        }

        public override void ExecuteResult(ControllerContext context)
        {
            if (context != null)
            {
                HttpRequestBase request = context.HttpContext.Request;
                HttpResponseBase response = context.HttpContext.Response;

                //response.AddHeader("Access-Control-Allow-Origin", "*");
                //response.AddHeader("Access-Control-Allow-Methods", "POST,GET,PUT,DELETE,OPTIONS");
                //response.AddHeader("Access-Control-Allow-Headers", "X-Requested-With,X-HTTP-Method-Override,Content-Type,Accept,Cache-Control,x-hwy-token");

                bool isGet = request.HttpMethod.Equals("GET", StringComparison.CurrentCultureIgnoreCase);

                if (!isGet || this._requestBehavior == System.Web.Mvc.JsonRequestBehavior.AllowGet)
                {
                    if (this._data != null)
                    {
                        //由于输出DataTime都为int类型时间戳，所以把转换Json时间格式去除
                        string jsonString = JsonConvert.SerializeObject(this._data, Formatting.None, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Include });
                        LogHelper.WriteResponseLog(string.Format("RequestUrl : [ {0} ] \r\nResponseData : \r\n{1}", context.Controller.ControllerContext.HttpContext.Request.Url, jsonString));
                        response.Write(jsonString);
                    }
                    else
                    {
                        LogHelper.WriteResponseLog(string.Format("RequestUrl : [ {0} ] Response Empty", context.Controller.ControllerContext.HttpContext.Request.Url));
                        response.Write(string.Empty);
                    }
                }
                else
                {
                    LogHelper.WriteResponseLog(string.Format("RequestUrl : [ {0} ] Response Illegal request", context.Controller.ControllerContext.HttpContext.Request.Url));
                    response.Write("Illegal request");
                }
            }
        }
    }
}
