using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using GPAA.Interfaces.Repository;
using GPAA.Models.DomainModels;
using Microsoft.Practices.Unity;

namespace GPAA.Repository.BaseRepository
{
    /// <summary>
    /// Base Repository
    /// </summary>
    public abstract class BaseRepository<TDomainClass> : IBaseRepository<TDomainClass, int>
       where TDomainClass : class
    {
        #region Private

        private readonly IUnityContainer container;
        #endregion
        #region Protected

        /// <summary>
        /// Primary database set
        /// </summary>
        protected abstract IDbSet<TDomainClass> DbSet { get; }

        


        #endregion
        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        protected BaseRepository(IUnityContainer container)
        {

            if (container == null)
            {
                throw new ArgumentNullException("container");
            }
            this.container = container;
            string connectionString = ConfigurationManager.ConnectionStrings["BaseDbContext"].ConnectionString;
            db = (BaseDbContext)container.Resolve(typeof(BaseDbContext), new ResolverOverride[] { new ParameterOverride("connectionString", connectionString) });

        }

        #endregion
        #region Public
        /// <summary>
        /// base Db Context
        /// </summary>
        public BaseDbContext db;
        /// <summary>
        /// Get all
        /// </summary>
        public virtual IQueryable<TDomainClass> GetAll(TDomainClass instance)
        {
            return DbSet.Find(instance) as IQueryable<TDomainClass>;
        }

        /// <summary>
        /// Create object instance
        /// </summary>
        public virtual TDomainClass Create()
        {
            TDomainClass result = container.Resolve<TDomainClass>();
            return result;
        }
        /// <summary>
        /// Find entry by key
        /// </summary>
        public virtual IQueryable<TDomainClass> Find(TDomainClass instance)
        {
            return DbSet.Find(instance) as IQueryable<TDomainClass>;
        }
        /// <summary>
        /// Find Entity by Id
        /// </summary>
        public TDomainClass Find(int id)
        {
            return DbSet.Find(id);
        }
        /// <summary>
        /// Find Entity by Id
        /// </summary>
        public TDomainClass Find(string id)
        {
            return DbSet.Find(id);
        }
        /// <summary>
        /// Find Entity by Id
        /// </summary>
        public TDomainClass Find(long id)
        {
            return DbSet.Find(id);
        }
        /// <summary>
        /// Get All Entites 
        /// </summary>
        /// <returns></returns>
        public IQueryable<TDomainClass> GetAll()
        {
            return DbSet;
        }
        /// <summary>
        /// Save Changes in the entities
        /// </summary>
        public void SaveChanges()
        {
            db.SaveChanges();
        }
        /// <summary>
        /// Delete an entry
        /// </summary>
        public virtual void Delete(TDomainClass instance)
        {
            DbSet.Remove(instance);

        }
        /// <summary>
        /// Add an entry
        /// </summary>
        public virtual void Add(TDomainClass instance)
        {
            DbSet.Add(instance);
        }
        /// <summary>
        /// Add an entry
        /// </summary>
        public virtual void Update(TDomainClass instance)
        {
            DbSet.AddOrUpdate(instance);
        }

        public IEnumerable<AspNetRole> Roles()
        {
            return db.UserRoles.Where(r => !r.Name.Equals("SuperAdmin"));
            //return null;
        }

        #endregion
    }
}