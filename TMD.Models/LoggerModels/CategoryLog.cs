
namespace TMD.Models.LoggerModels
{
    /// <summary>
    /// Category Log class for database logging
    /// </summary>
    public class CategoryLog
    {
        /// <summary>
        /// Category Log Id
        /// </summary>
        public int CategoryLogID { get; set; }

        /// <summary>
        /// LogCategory Id
        /// </summary>
        public int LogCategoryID { get; set; }

        /// <summary>
        /// Log Id
        /// </summary>
        public int LogID { get; set; }

        /// <summary>
        /// Log Category
        /// </summary>
        public virtual LogCategory Category { get; set; }

        /// <summary>
        /// Log 
        /// </summary>
        public virtual Log Log { get; set; }

    }
}
