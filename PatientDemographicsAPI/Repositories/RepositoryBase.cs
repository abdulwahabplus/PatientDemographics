using Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public abstract class RepositoryBase<T> where T : class
    {
        #region Properties
        private PatientDemographicEntities _dataContext;
        private readonly IDbSet<T> _dbSet;

        protected IDbFactory DbFactory
        {
            get;
            private set;
        }

        protected PatientDemographicEntities DbContext
        {
            get { return _dataContext ?? (_dataContext = DbFactory.Init()); }
        }
        #endregion

        protected RepositoryBase(IDbFactory dbFactory)
        {
            DbFactory = dbFactory;
            _dbSet = DbContext.Set<T>();
        }

        #region Implementation
        public virtual T Add(T entity)
        {
            return _dbSet.Add(entity);
        }

        public virtual IEnumerable<T> GetAll()
        {
            return _dbSet.ToList();
        }
        #endregion

    }
}
