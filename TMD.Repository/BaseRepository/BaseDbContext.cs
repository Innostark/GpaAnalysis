using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using TMD.Models.DomainModels;
using TMD.Models.LoggerModels;
using TMD.Models.MenuModels;
using Microsoft.Practices.Unity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Core.Objects;
using System;

namespace TMD.Repository.BaseRepository
{
    /// <summary>
    /// Base Db Context. Implements Identity Db Context over Application User
    /// </summary>
    public sealed class BaseDbContext : DbContext
    {
        #region Private
        private IUnityContainer container;
        #endregion
        #region Protected
        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    //base.OnModelCreating(modelBuilder);
        //    //modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        //    //modelBuilder.Entity<Product>().HasKey(p => p.Id);
        //    //modelBuilder.Entity<Product>().Property(c => c.Id)
        //    //    .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        //}
        #endregion
        #region Constructor
        public BaseDbContext()
        {            
        }
        #endregion
        #region Public

        public BaseDbContext(string connectionString,IUnityContainer container)
            : base(connectionString)
        {
            this.container = container;
        }
        //#region Logger

        /// <summary>
        /// Logs
        /// </summary>
        public DbSet<Log> Logs { get; set; }
        /// <summary>
        /// Log Categories
        /// </summary>
        public DbSet<LogCategory> LogCategories { get; set; }
        /// <summary>
        /// Category Logs
        /// </summary>
        public DbSet<CategoryLog> CategoryLogs { get; set; }
        #endregion
        #region Menu Rights and Security
        /// <summary>
        /// Menu Rights
        /// </summary>
        public DbSet<MenuRight> MenuRights { get; set; }
        /// <summary>
        /// Menu
        /// </summary>
        public DbSet<Menu> Menus { get; set; }
        #endregion
        /// <summary>
        /// Users
        /// </summary>
        public DbSet<AspNetUser> Users { get; set; }

        /// <summary>
        /// User Roles
        /// </summary>
        public DbSet<AspNetRole> UserRoles { get; set; }
        public DbSet<AspNetUserClaim> UserClaims { get; set; }
        public DbSet<AspNetUserLogin> UserLogins { get; set; }
        public DbSet<AspNetUser> AspNetUsers { get; set; }

        /// <summary>
        /// Staging Ebay
        /// </summary>
        public DbSet<StagingEbayBatchImport> StagingEbayBatchImports { get; set; }

        public DbSet<StagingEbayItem> StagingEbayItems { get; set; }

        public DbSet<Configuration> Configurations { get; set; }

        /// <summary>
        /// Calls the database stored procedure spIsEbayLoadRunning
        /// Check if an ebay load is already running, return the count of IsProcessing records in the database
        /// </summary>
        /// <returns>true if load is running, otherwise false</returns>
        public bool IsEbayLoadRunning()
        {
            ObjectResult<int> results = ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<int>("spIsEbayLoadRunning");

            foreach(int?  result in results)
            {
                if(result == 0)
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Calls the database stored procedure spIsEbayLoadRunning
        /// Check if an ebay load is already running, return the count of IsProcessing records in the database
        /// </summary>
        /// <returns>true if load is running, otherwise false</returns>
        public bool EbayItemExists(string itemId)
        {
            ObjectResult<Collection<StagingEbayItem>> results = ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Collection<StagingEbayItem>>("EbayItemExists", new ObjectParameter[] { new ObjectParameter("itemId", itemId) });

            //foreach (int? result in results)
            //{
            //    if (result == 0)
            //    {
            //        return false;
            //    }
            //}

            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string GetEbayLoadStartTimeFrom()
        {
            ObjectResult<string> results = ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<string>("spGetEbayLoadStartTimeFrom");

            foreach (string result in results)
            {
                return result;
            }

            return null;
        }

        public int UpsertEbayLoadStartTimeFromConfiguration(DateTime ebayLoadStartTimeFrom)
        {
            ObjectResult<int> results = ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<int>("spUpsertEbayLoadStartTimeFromConfiguration", new ObjectParameter("EbayLoadStartTimeFrom", typeof(DateTime)) { Value = ebayLoadStartTimeFrom });

            foreach (int result in results)
            {
                return result;
            }

            return int.MinValue;
        }
    }
}
