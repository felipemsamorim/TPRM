using Npgsql;
using System.Web;
using System.Web.Mvc;

namespace Prime
{
    public class CustomErrorHandler : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            var exception = filterContext.Exception;
            if (filterContext.ExceptionHandled || !filterContext.HttpContext.IsCustomErrorEnabled)
            {
                return;
            }

            if (new HttpException(null, filterContext.Exception).GetHttpCode() != 500)
            {
                return;
            }

            if (!ExceptionType.IsInstanceOfType(filterContext.Exception))
            {
                return;
            }


            bool ajax = filterContext.HttpContext.Request.Headers["X-Requested-With"] == "XMLHttpRequest";

            #region JSON
            //SE A PÁGINA ESPERA UM JSON
            if (ajax)
            {
                var data = new JsonResult();

                //EXCEÇÕES SQL EM INNER EXCEPTION (ORM)
                if (exception.InnerException as NpgsqlException != null)
                {
                    exception = exception.InnerException;
                }


                var sqlException = exception as NpgsqlException;
                
                    data = new JsonResult
                    {
                        JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                        Data = sqlException
                    };
                
                
                    filterContext.Result = data;
                
                
                filterContext.HttpContext.Response.StatusCode = 500;
            }
            #endregion
            #region PAGINA
            else //SE UMA PÁGINA É ESPERADA COMO REPOSTA
            {
                var controllerName = (string)filterContext.RouteData.Values["controller"];
                var actionName = (string)filterContext.RouteData.Values["action"];
                var model = new HandleErrorInfo(filterContext.Exception, controllerName, actionName);

                filterContext.Result = new ViewResult
                {
                    ViewName = View,
                    MasterName = Master,
                    ViewData = new ViewDataDictionary<HandleErrorInfo>(model),
                    TempData = filterContext.Controller.TempData
                };
                filterContext.HttpContext.Response.StatusCode = 500;
            }
            #endregion

            filterContext.ExceptionHandled = true;
            //filterContext.HttpContext.Response.Clear();

            filterContext.HttpContext.Response.TrySkipIisCustomErrors = true;


        }
    }
}