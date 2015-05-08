using System;

namespace TMD.Models.LoggerModels
{
    /// <summary>
    /// Log class for database logging
    /// </summary>
    public class Log
    {
        /// <summary>
        /// Log Id
        /// </summary>
        public int LogID { get; set; }

        /// <summary>
        /// Event Id
        /// </summary>
        public int EventID { get; set; }

        /// <summary>
        /// Priority
        /// </summary>
        public int Priority { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Severity { get; set; }

        /// <summary>
        /// Title
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Time Stamp
        /// </summary>
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// Machine Name
        /// </summary>
        public string MachineName { get; set; }

        /// <summary>
        /// App Domain
        /// </summary>
        public string AppDomainName { get; set; }

        /// <summary>
        /// Process ID
        /// </summary>
        public string ProcessID { get; set; }

        /// <summary>
        /// Process Name
        /// </summary>
        public string ProcessName { get; set; }

        /// <summary>
        /// Thread Name
        /// </summary>
        public string ThreadName { get; set; }

        /// <summary>
        /// Thread Id
        /// </summary>
        public string Win32ThreadId { get; set; }

        /// <summary>
        /// Message
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Formatted Message
        /// </summary>
        public string FormattedMessage { get; set; }

        /// <summary>
        /// Handling Instance Id
        /// </summary>
        public string HandlingInstanceId { get; set; }
    }
}
