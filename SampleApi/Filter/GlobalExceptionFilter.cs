using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using SampleApi.Models;
using System.Net;
using System.Security.Claims;

//using Microsoft.AspNetCore.Mvc.Controllers;
//using Microsoft.AspNetCore.Mvc.Filters;
//using System.Reflection.Metadata;
//using System.Security.Claims;
//using NLog;
//using mvc_api.Util.Logger;
//using mvc_api.Models.Response;
//using System.Net;
//using Microsoft.AspNetCore.Mvc;

namespace SampleApi.Filter
{
    // todo エラー処理ミドルウェアが推奨されている
    // https://learn.microsoft.com/ja-jp/aspnet/core/mvc/controllers/filters?view=aspnetcore-8.0

    public class GlobalExceptionFilter : IExceptionFilter
    {
        static NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();

        //ILoggerManager _logger;

        //public GlobalExceptionFilter(ILoggerManager logger)
        //{
        //    _logger = logger;
        //}


        public void OnException(ExceptionContext context)
        {
            this.OutputExceptionLog(context);
            this.SetExceptionResult(context);
        }


        private void SetExceptionResult(ExceptionContext context)
        {
            var errorResponse = new ErrorResponse(HttpStatusCode.InternalServerError);
#if DEBUG
            errorResponse.AddError("500100", context.Exception.ToString());
#else
            errorResponse.AddError("500100", "Internal Server Error");
#endif
            context.Result = new ObjectResult(errorResponse)
            {
                StatusCode = 500
            };
        }


        private void OutputExceptionLog(ExceptionContext context)
        {
            try
            {
                var controllerActionDescriptor = context.ActionDescriptor as ControllerActionDescriptor;
                var name = context.HttpContext.User.FindFirst(ClaimTypes.Name)?.Value;

                _logger.Error(
                    $"Controller:{controllerActionDescriptor!.ControllerName} " +
                    $"Action:{controllerActionDescriptor.ActionName} " +
                    $"User:{(name ?? "No User")} " +
                    "予期せぬ例外が発生。" + Environment.NewLine +
                    "************************************************" + Environment.NewLine +
                    $"{context.Exception}" + Environment.NewLine +
                    "************************************************"
                    );
            }
            catch (Exception ex)
            {
                _logger.Error("\r\n" + "ログ出力時にエラーが発生。" + ex);
            }
        }
    }
}
