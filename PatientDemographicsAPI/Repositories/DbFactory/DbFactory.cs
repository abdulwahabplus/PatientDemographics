using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class DbFactory : Disposable, IDbFactory
    {
        PatientDemographicEntities dbContext;

        public PatientDemographicEntities Init()
        {
            return dbContext ?? (dbContext = new PatientDemographicEntities());
        }

        protected override void DisposeCore()
        {
            if (dbContext != null)
                dbContext.Dispose();
        }
    }
}
