using log4net;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace YH.MVC
{
    public class WxExceptionFilter : ActionFilterAttribute, IExceptionFilter
    {
        static ILog _log = LogManager.GetLogger(typeof(ActionFilter));

        public void OnException(ExceptionContext filterContext)
        {
            HttpRequestBase request = filterContext.HttpContext.Request;
            filterContext.ExceptionHandled = true;
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("-------------------------------------------------------------------------------------");

            AppendFormat(sb, "userAgent : {0}", request.UserAgent);
            AppendFormat(sb, "requestUrl : {0}", request.Url.ToString());
            try
            {
                var parmStr = new StringBuilder();
                foreach (var key in request.Form.AllKeys)
                {
                    parmStr.AppendFormat("{0}={1}&", key, request.Form[key]);
                }
                if (parmStr.Length > 0)
                    parmStr = parmStr.Remove(parmStr.Length - 1, 1);
                string formData = parmStr.ToString();
                AppendFormat(sb, "formData : {0}", formData);
            }
            catch
            {
            }
            sb.AppendLine("\r\n-------------------------------------------------------------------------------------");
            sb.AppendLine("****************************************End******************************************\r\n");

            _log.Error(sb.ToString(), filterContext.Exception);

            BaseException mindsException = null;
            if (filterContext.Exception.InnerException != null)
            {
                mindsException = filterContext.Exception.InnerException as BaseException;
            }
            else
            {
                mindsException = filterContext.Exception as BaseException;
            }

            //HttpResponseBase response = filterContext.HttpContext.Response;
            //
            //response.StatusCode = 200;
            //response.AddHeader("Access-Control-Allow-Origin", "*");//允许跨域请求
            //response.AddHeader("Access-Control-Allow-Methods", "POST,GET,PUT,DELETE,OPTIONS");//允许跨域请求
            //response.AddHeader("Access-Control-Allow-Headers", "X-Requested-With,X-HTTP-Method-Override,Content-Type,Accept,x-moutai-token");//允许跨域请求
            ResultModel model = null;
            if (mindsException != null)
            {
                model = ResultModel.Error(0, mindsException.ErrorCode, mindsException.Message);
                AppendFormat(sb, mindsException.Message);
                filterContext.Result = new JsonResult
                {
                    Data = model,
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
            else
            {
                model = ResultModel.Error(0, 0, string.Format("未知错误：{0}", filterContext.Exception.Message));
                AppendFormat(sb, "unknow error");
                 
                filterContext.Result =  new JsonResult
                {
                    Data = model,
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
        }

        void AppendFormat(StringBuilder sb, string format, params object[] args)
        {
            if (args == null || args.Length == 0)
            {
                sb.Append(format);
            }
            else
            {
                sb.AppendFormat(format, args);
            }
            sb.AppendLine("\r\n-------------------------------------------------------------------------------------");
        }
    }
}
