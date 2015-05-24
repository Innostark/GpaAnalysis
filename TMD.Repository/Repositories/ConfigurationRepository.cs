using System;
using System.Data.Entity;
using Microsoft.Practices.Unity;
using TMD.Interfaces.Repository;
using TMD.Models.DomainModels;
using TMD.Repository.BaseRepository;

namespace TMD.Repository.Repositories
{
    public sealed class ConfigurationRepository: BaseRepository<Configuration>, IConfigurationRepository
    {
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public ConfigurationRepository(IUnityContainer container)
            : base(container)
        {
        }

        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<Configuration> DbSet
        {
            get { return db.Configurations; }
        }
        #endregion

        public string GetEbayLoadStartTimeFrom()
        {
            return db.GetEbayLoadStartTimeFrom();
        }

        public int UpsertEbayLoadStartTimeFromConfiguration(DateTime ebayLoadStartTimeFrom)
        {
            return db.UpsertEbayLoadStartTimeFromConfiguration(ebayLoadStartTimeFrom);
        }
    }
}
