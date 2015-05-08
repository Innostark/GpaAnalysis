using System.Collections.Generic;
using System.Diagnostics;
using GPAA.Interfaces.IServices;
using Microsoft.Practices.EnterpriseLibrary.Logging;

namespace GPAA.Implementation.Services
{
    /// <summary>
    /// Logger class that manages log entries 
    /// </summary>
    public sealed class LoggerService : ILogger
    {
        /// <summary>
        /// Write Log to database
        /// </summary>
        public void Write(string message, string category, int priority, int eventId, TraceEventType severity, string title)
        {
            Logger.Write(message, category, priority, eventId, severity, title);
        }

        /// <summary>
        /// Write Log to database
        /// </summary>
        public void Write(object message, string category, int priority, int eventId, TraceEventType severity, string title, IDictionary<string, object> properties)
        {
            Logger.Write(message, category, priority, eventId, severity, title, properties);
        }
    }
}
