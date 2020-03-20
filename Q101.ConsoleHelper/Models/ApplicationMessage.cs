using System;
using Q101.ConsoleHelper.Enums;

namespace Q101.ConsoleHelper.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class ApplicationMessage
    {
        /// <summary>
        /// 
        /// </summary>
        public MessageTypes Type { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public ConsoleColor Color { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Message { get; set; }
    }
}
