using System;
using Q101.ConsoleHelper.Abstract;
using Q101.ConsoleHelper.Enums;
using Q101.ConsoleHelper.Messages;

namespace Q101.ConsoleHelper.Concrete
{
    /// <summary>
    /// Work with the console
    /// </summary>
    public class Q101ConsoleHelper : IQ101ConsoleHelper
    {
        private string _messageFormat = "[{timestamp}]:|{message}\n";

        private string _timestampFormat = "dd.MM.yyyy HH:mm:ss:ffffff";

        /// <summary>
        /// Messages Helper
        /// </summary>
        public MessagesHelper MessagesHelper { get; }

        /// <summary>
        /// ctor
        /// </summary>
        public Q101ConsoleHelper()
        {
            MessagesHelper = new MessagesHelper();
        }

        /// <summary>
        /// Set message and datetime formats
        /// </summary>
        /// <param name="messageFormat"></param>
        /// <param name="timeFormat"></param>
        public void SetMessageFormat(string messageFormat, string timeFormat = null)
        {
            if (string.IsNullOrEmpty(messageFormat))
            {
                throw new ArgumentNullException(nameof(messageFormat));
            }

            _timestampFormat = timeFormat ?? _timestampFormat;

            if (!messageFormat.Contains("|"))
            {
                throw new ArgumentException(
                    "Missing delimiter | in messageFormat argument");
            }

            if (!messageFormat.Contains("{message}")
                || !messageFormat.Contains("{timestamp}"))
            {
                throw new ArgumentException(
                    "Missing message or timestamp in messageFormat argument");
            }

            _messageFormat = messageFormat;
        }

        /// <summary>
        /// Ask user for confirmation
        /// </summary>
        /// <returns>bool true/false</returns>
        public bool GetToStop()
        {
            if (!GetYorN("Продолжить?")) 
            {
                WriteMessageWithTimeStamp("Обработка остановлена пользователем");
                Console.ReadLine();
                return true;
            }

            return false;
        }

        /// <summary>
        /// Read confirmation line
        /// </summary>
        /// <returns>String entered in console</returns>
        public string ReadLine()
        {
            return Console.ReadLine();
        }

        /// <summary>
        /// Ask user for confirmation Y/N
        /// </summary>
        /// <param name="message">Message for user</param>
        /// <param name="defaultYes">Default value (Y)</param>
        /// <returns>bool true/false depending on set</returns>
        public bool GetYorN(string message, bool defaultYes = true)
        {
            var isCorrectInput = false;
            while (!isCorrectInput)
            {
                var input = GetStringFromConsole($"{message} Y/N (default {(defaultYes ? "Y" : "N")})", true);

                isCorrectInput = string.IsNullOrEmpty(input) ||
                                 input.Equals("Y", StringComparison.OrdinalIgnoreCase) ||
                                 input.Equals("N", StringComparison.OrdinalIgnoreCase);



                if (!string.IsNullOrEmpty(input) &&
                    input.Equals((!defaultYes ? "Y" : "N"), StringComparison.OrdinalIgnoreCase))
                {
                    return !defaultYes;
                }

                if (!isCorrectInput)
                {
                    MessagesHelper.WriteMessageByType(
                        MessageTypes.IncorrectInputTryThis,
                        WriteMessageWithTimeStamp);
                }
            }

            return defaultYes;
        }

        /// <summary>
        /// Get string entered in console
        /// </summary>
        /// <param name="message">Message for user</param>
        /// <param name="maybeNull">Value may be null</param>
        /// <returns>String entered in console</returns>
        /// <returns></returns>
        public string GetStringFromConsole(string message, bool maybeNull = false)
        {
            string value = null;

            while (string.IsNullOrEmpty(value))
            {
                WriteMessageWithTimeStamp(message, ConsoleColor.Yellow);

                value = Console.ReadLine();

                if (string.IsNullOrEmpty(value) && !maybeNull)
                {
                    MessagesHelper.WriteMessageByType(
                        MessageTypes.IncorrectInputTryThis,
                        WriteMessageWithTimeStamp);
                }
                else if (string.IsNullOrEmpty(value) && maybeNull)
                {
                    return null;
                }
            }

            return value;
        }

        /// <summary>
        /// Get int number entered in console
        /// </summary>
        /// <param name="message">Message for user</param>
        /// <param name="defaultValue">Default value (0)</param>
        /// <returns>int entered in console</returns>
        public int GetIntFromConsole(string message, int defaultValue = 0)
        {
            int value = 0;

            var isParsed = false;

            while (!isParsed)
            {
                var defaultMessage = defaultValue == 0
                    ? null
                    : $" (default {defaultValue})";

                WriteMessageWithTimeStamp($"{message}{defaultMessage}", ConsoleColor.Yellow);

                var stingValue = Console.ReadLine();

                isParsed = int.TryParse(stingValue, out value);

                if (!isParsed && defaultValue == 0)
                {
                    MessagesHelper.WriteMessageByType(
                        MessageTypes.IncorrectInputTryThis,
                        WriteMessageWithTimeStamp);
                }
                else if (!isParsed && defaultValue != 0)
                {
                    return defaultValue;
                }
            }

            return value;
        }

        /// <summary>
        /// Display console message in the specified format
        /// </summary>
        /// <param name="message">Message for user</param>
        /// <param name="color">Text color (default white)</param>
        public void WriteMessageWithTimeStamp(string message, ConsoleColor? color = ConsoleColor.White)
        {
            var messageFormatArr = _messageFormat.Split('|');

            for (int i = 0; i < 2; i++)
            {
                var partOfMessage = messageFormatArr[i];

                if (partOfMessage.Contains("{timestamp}"))
                {
                    var timeStampMessage = partOfMessage.Replace(
                        "{timestamp}",
                        DateTime.Now.ToString(_timestampFormat));

                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.Write($"{timeStampMessage}");
                }

                if (partOfMessage.Contains("{message}"))
                {
                    var textMessage = partOfMessage.Replace(
                        "{message}",
                        message);

                    Console.ForegroundColor = color ?? ConsoleColor.White;
                    Console.Write($" {textMessage}\n");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="color"></param>
        public void Message(string message, ConsoleColor? color = ConsoleColor.White)
        {
            WriteMessageWithTimeStamp(message, color);
        }

        /// <summary>
        /// Write Application End Phrase
        /// </summary>
        /// <param name="waitUserInput"></param>
        /// <param name="hasErrors"></param>
        public void WriteApplicationEndPhrase(bool hasErrors, bool waitUserInput = false)
        {
            if (hasErrors)
            {
                MessagesHelper.WriteMessageByType(
                    MessageTypes.ApplicationEndWithErrors,
                    WriteMessageWithTimeStamp);
            }
            else
            {
                MessagesHelper.WriteMessageByType(
                    MessageTypes.ApplicationEndWithoutErrors,
                    WriteMessageWithTimeStamp);
            }

            if (!waitUserInput)
            {
                Console.ReadLine();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="exception"></param>
        /// <param name="color"></param>
        public void WriteException(Exception exception, ConsoleColor color = ConsoleColor.Red)
        {
            WriteMessageWithTimeStamp(
                $"Произошла ошибка: {exception.Message}\n{exception.StackTrace}",
                color);
        }
    }
}
