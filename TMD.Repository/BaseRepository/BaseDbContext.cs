using System.Data.Entity;
using TMD.Models.DomainModels;
using TMD.Models.LoggerModels;
using TMD.Models.MenuModels;
using Microsoft.Practices.Unity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Core.Objects;

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
        public DbSet<STGEbayBatchImport> STGEbayBatchImports { get; set; }

        public DbSet<STGEbayItem> STGEbayItems { get; set; }

        /// <summary>
        /// Calls the database stored procedure spIsEbayLoadRunning
        /// Check if an ebay load is already running, return the count of IsProcessing records in the database
        /// </summary>
        /// <returns>true if load is running, otherwise false</returns>
        public bool IsEbayLoadRunning()
        {
            ObjectResult<int?> results = ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<int?>("spIsEbayLoadRunning");

            foreach(int?  result in results)
            {
                if(result == 0)
                {
                    return false;
                }
            }

            return true;
        }

    }
}
