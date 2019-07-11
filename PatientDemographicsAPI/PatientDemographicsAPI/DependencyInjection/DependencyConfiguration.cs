using Microsoft.Practices.Unity;
using PatientDemographics.Core;
using Repositories;
using Repositories.Repositories;
using Repositories.UnitofWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace PatientDemographicsAPI.DependencyInjection
{
    public class DependencyConfiguration
    {
        //Dependency injection related configuration
        public void Configure(HttpConfiguration config)
        {
            var container = new UnityContainer();
            container.RegisterType<IUnitOfWork, UnitOfWork>(new HierarchicalLifetimeManager());
            container.RegisterType<IDbFactory, DbFactory>(new HierarchicalLifetimeManager());
            container.RegisterType<IPatientRepository, PatientRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IPatientService, PatientService>(new HierarchicalLifetimeManager());
            config.DependencyResolver = new UnityResolver(container);
        }
    }
}