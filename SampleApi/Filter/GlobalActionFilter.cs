using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SampleApi.Models;
using System.Net;
using System.Security.Claims;

namespace SampleApi.Filter
{
    public class GlobalActionFilter: IActionFilter
    {
        public GlobalActionFilter() 
        {
                
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            //_logger.Info(this.AccessInfo(context) + "OnActionExecuting");

            if (!context.ModelState.IsValid)
            {
                // 自動モデルバインダーフィルターを無効、自分でレスポンスを作成
                this.SetBadRequestResponse(context);
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            //_logger.Info(this.AccessInfo(context) + "OnActionExecuted");
        }

        private void SetBadRequestResponse(ActionExecutingContext context)
        {
            // 検証をカスタムした場合

            //{
            //  "status": 400,
            //  "errors": [
            //    {
            //      "code": "400",
            //      "message": "Age is required."
            //    }
            //  ]
            //}

            //{
            //  "status": 400,
            //  "errors": [
            //    {
            //      "code": "400",
            //      "message": "Name is length cant'be more than 20."
            //    }
            //  ]
            //}

            var errorResponse = new ErrorResponse(HttpStatusCode.BadRequest);

            context.ModelState.ToList().ForEach(argument =>
            {
                if (argument.Value?.ValidationState == ModelValidationState.Invalid)
                {
                    argument.Value?.Errors.ToList().ForEach(error =>
                    {
                        errorResponse.AddError("400", error.ErrorMessage);
                    });
                }
            });

            context.Result = new BadRequestObjectResult(errorResponse);
        }

        private string AccessInfo(FilterContext context)
        {
            var controllerActionDescriptor = context.ActionDescriptor as ControllerActionDescriptor;
            var controllerName = controllerActionDescriptor!.ControllerName;
            var actionName = controllerActionDescriptor!.ActionName;
            var userName = context.HttpContext.User.FindFirst(ClaimTypes.Name)?.Value;
            return $"Controller={controllerName},Action={actionName},User={userName},";
        }

    }
}

