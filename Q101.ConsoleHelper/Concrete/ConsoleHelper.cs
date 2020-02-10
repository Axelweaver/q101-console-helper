using System;
using System.Resources;
using Q101.ConsoleHelper.Abstract;

namespace Q101.ConsoleHelper.Concrete
{
    /// <summary>
    /// Work with the console
    /// </summary>
    public class ConsoleHelper : IConsoleHelper
    {
        private string _messageFormat = "[{timestamp}]:|{message}\n";

        private string _timestampFormat = "dd.MM.yyyy HH:mm:ss:ffffff";


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
                    WriteMessageWithTimeStamp("Некорретный ввод. Повторите", ConsoleColor.Red);
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
                    WriteMessageWithTimeStamp("Некорректный ввод, введите ещё раз", ConsoleColor.Red);
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
                    WriteMessageWithTimeStamp("Некорректный ввод, введите ещё раз", ConsoleColor.Red);
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
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.Write($"[{DateTime.Now:dd.MM.yyyy HH:mm:ss:ffffff}]");
            Console.ForegroundColor = color ?? ConsoleColor.White;
            Console.Write($" {message}\n");
            Console.ForegroundColor = ConsoleColor.White;
        }

        private string FormatMessage(string message)
        {
            var messageFormatArr = _messageFormat.Split('|');

            var resultMessageArr = new string[2];

            for (int i = 0; i < 2; i++)
            {
                var partOfMessage = messageFormatArr[i];

                if (partOfMessage.Contains("{timestamp}"))
                {
                    resultMessageArr[i] = partOfMessage.Replace(
                        "{timestamp}",
                        DateTime.Now.ToString(_timestampFormat));
                }

                if (partOfMessage.Contains("{message}"))
                {
                    resultMessageArr[i] = partOfMessage.Replace(
                        "{message}",
                        message);
                }


            }

            var messageFormat =
                _messageFormat.Replace("{timestamp}", "{0}")
                              .Replace("{message}", "{1}");

            var timestamp = DateTime.Now;

            var resultMessage = string.Format(
                messageFormat,
                timestamp.ToString(_timestampFormat),
                message);

            return resultMessage;
        }

    }
}
