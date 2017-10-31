using System.Web.Mvc;

namespace YH.MVC
{
    /// <summary>
    /// 自定义ActionFilterAttribute
    /// </summary>
    public class ActionFilter : ActionFilterAttribute
	{
		/// <summary>
		/// <para>拦截通信,扩展MVC的Action参数转换赋值功能</para>
		/// <para>原生MVC的Action中,假如参数列表中出现集合类型参数或者有多个实体类类型参数则会赋值失败</para>
		/// </summary>
		/// <param name="filterContext"></param>
		public override void OnActionExecuting (ActionExecutingContext filterContext)
		{
			base.OnActionExecuting (filterContext);
		}

		public override void OnActionExecuted (ActionExecutedContext filterContext)
		{
			base.OnActionExecuted (filterContext);
		}

		public override void OnResultExecuting (ResultExecutingContext filterContext)
		{
			base.OnResultExecuting (filterContext);
		}

		public override void OnResultExecuted (ResultExecutedContext filterContext)
		{
			//如果允许所有域名
			//HttpResponseBase response = filterContext.HttpContext.Response;
   //         //response.AddHeader("Access-Control-Allow-Origin", "*");//允许跨域请求
   //         //response.AddHeader("Access-Control-Allow-Methods", "POST,GET,PUT,DELETE,OPTIONS");//允许跨域请求
   //         //response.AddHeader("Access-Control-Allow-Headers", "X-Requested-With,X-HTTP-Method-Override,Content-Type,Accept,x-moutai-token");//允许跨域请求

			base.OnResultExecuted (filterContext);
		}
	}
}