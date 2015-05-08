
namespace GPAA.Models.LoggerModels
{
    /// <summary>
    /// Log Category Class for database logging
    /// </summary>
    public sealed class LogCategory
    {
        /// <summary>
        /// LogCategory Id
        /// </summary>
        public int LogCategoryID { get; set; }

        /// <summary>
        /// Category Name
        /// </summary>
        public string CategoryName { get; set; }
    }
}
