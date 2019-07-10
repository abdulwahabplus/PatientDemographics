using Microsoft.Practices.Unity;
using PatientDemographics.Core;
using PatientDemographicsAPI.DependacyInjection;
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
            var container = new UnityContainer();
            container.RegisterType<IUnitOfWork, UnitOfWork>(new HierarchicalLifetimeManager());
            container.RegisterType<IDbFactory, DbFactory>(new HierarchicalLifetimeManager());
            container.RegisterType<IPatientRepository, PatientRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IPatientService, PatientService>(new HierarchicalLifetimeManager());
            config.DependencyResolver = new UnityResolver(container);

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
