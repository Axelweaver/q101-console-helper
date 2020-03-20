using System;
using System.Collections.Generic;
using Q101.ConsoleHelper.Enums;
using Q101.ConsoleHelper.Models;

namespace Q101.ConsoleHelper.Messages
{
    /// <summary>
    /// 
    /// </summary>
    public class MessagesList
    {
        /// <summary>
        /// 
        /// </summary>
        public IList<ApplicationMessage> Messages { get; }

        /// <summary>
        /// ctor
        /// </summary>
        public MessagesList()
        {
            Messages = new List<ApplicationMessage>()
            {
                GetMessage(MessageTypes.ApplicationEndWithoutErrors,
                            "Работа приложения завершена без ошибок",
                            ConsoleColor.Green),
                GetMessage(MessageTypes.ApplicationEndWithErrors,
                            "Работа приложения завершена с ошибками",
                            ConsoleColor.Red),
                GetMessage(MessageTypes.IncorrectInputTryThis,
                            "Некорректный ввод, введите ещё раз", 
                            ConsoleColor.Red),
                GetMessage(MessageTypes.WriteException,
                            "Произошла ошибка: ",
                            ConsoleColor.Red)
            };
        }

        private ApplicationMessage GetMessage(MessageTypes type, 
                                              string text, 
                                              ConsoleColor color = ConsoleColor.White)
        {
            var message = new ApplicationMessage
            {
                Type = type,
                Message = text,
                Color = color
            };

            return message;
        }
    }
}
