using Microsoft.Practices.Unity;
using PatientDemographics.Core;
using PatientDemographicsAPI.DependencyInjection;
using PatientDemographicsAPI.Filters;
using PatientDemographicsAPI.Handlers;
using Repositories;
using Repositories.Repositories;
using Repositories.UnitofWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.ExceptionHandling;

namespace PatientDemographicsAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            //To enable cross origin
            var cors = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(cors);


            // Web API routes
            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );


            //Dependency injection related configuration
            DependencyConfiguration dependencyConfiguration = new DependencyConfiguration();
            dependencyConfiguration.Configure(config);

            //Registering filters globally to unhandled 
            config.Filters.Add(new CustomExceptionFilter());

            //Registering filters globally to Handle
            //Error inside the exception filter.
            //Exception related to routing.
            //Error inside the Message Handlers class.
            //Error in Controller Constructor.
            config.Services.Replace(typeof(IExceptionHandler), new GlobalExceptionHandler());  
        }
    }
}
