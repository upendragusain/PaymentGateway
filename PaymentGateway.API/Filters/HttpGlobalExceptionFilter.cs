using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PaymentGateway.Domain.Exceptions;
using System.Net;

namespace PaymentGateway.API.Filters
{
    public class HttpGlobalExceptionFilter : IExceptionFilter
    {
        private readonly IWebHostEnvironment env;
        private readonly ILogger<HttpGlobalExceptionFilter> logger;

        public HttpGlobalExceptionFilter(IWebHostEnvironment env,
                                         ILogger<HttpGlobalExceptionFilter> logger)
        {
            this.env = env;
            this.logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            logger.LogError(new EventId(context.Exception.HResult),
                context.Exception,
                context.Exception.Message);

            if (env.IsDevelopment())
            {
                var problemDetails = new ValidationProblemDetails()
                {
                    Instance = context.HttpContext.Request.Path,
                    Detail = "Please refer to the errors property for additional details."
                };

                if (context.Exception.GetType() == typeof(PaymentDomainException))
                {
                    problemDetails.Status = StatusCodes.Status400BadRequest;
                    problemDetails.Errors.Add("DomainValidations", new string[] { context.Exception.Message.ToString() });
                    context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                }
                else
                {
                    problemDetails.Status = StatusCodes.Status500InternalServerError;
                    problemDetails.Title = "Please try again later";
                    problemDetails.Errors.Add("Exception", new string[] { context.Exception.Message.ToString() });
                    context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                }

                context.Result = new BadRequestObjectResult(problemDetails);
            }
            else
            {
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }

            context.ExceptionHandled = true;
        }
    }

}
