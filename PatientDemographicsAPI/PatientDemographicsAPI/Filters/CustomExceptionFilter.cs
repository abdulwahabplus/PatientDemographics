using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;  
using System.Net.Http;  
using System.Web.Http.Filters;
using PatientDemographics.Logger; 

namespace PatientDemographicsAPI.Filters
{
    public class CustomExceptionFilter: ExceptionFilterAttribute  
    {  
        public override void OnException(HttpActionExecutedContext actionExecutedContext)  
        {  
            string exceptionMessage = string.Empty;  
            if (actionExecutedContext.Exception.InnerException == null)  
            {  
                exceptionMessage = actionExecutedContext.Exception.Message;  
            }  
            else  
            {  
                exceptionMessage = actionExecutedContext.Exception.InnerException.Message;
            }

            //Logging exception
            LogHelper.Log(exceptionMessage);
 
            var response = new HttpResponseMessage(HttpStatusCode.InternalServerError)  
            {  
                Content = new StringContent("An unhandled exception was thrown by service."), 
                ReasonPhrase = "Internal Server Error.Please Contact your Administrator."  
            };  
            actionExecutedContext.Response = response;  
        }  
    }  
}