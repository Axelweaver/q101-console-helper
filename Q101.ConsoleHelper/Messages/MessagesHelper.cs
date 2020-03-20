using System;
using System.Collections.Generic;
using System.Linq;
using Q101.ConsoleHelper.Enums;
using Q101.ConsoleHelper.Models;

namespace Q101.ConsoleHelper.Messages
{
    /// <summary>
    /// Messages Helper
    /// </summary>
    public class MessagesHelper
    {

        /// <summary>
        /// Messages for app
        /// </summary>
        public IList<ApplicationMessage> Messages { get; }

        /// <summary>
        /// ctor
        /// </summary>
        public MessagesHelper()
        {
            Messages = new MessagesList().Messages;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="action"></param>
        public void WriteMessageByType(MessageTypes type, Action<string, ConsoleColor?> action)
        {
            var message = 
                Messages.FirstOrDefault(m => m.Type == type);

            if (message == null)
            {
                throw new ArgumentException("No message this type", nameof(type));
            }

            action(message.Message, message.Color);
        }
    }
}
