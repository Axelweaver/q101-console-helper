using System;

namespace Q101.ConsoleHelper.Abstract
{
    /// <summary>
    /// Work with the console
    /// </summary>
    public interface IConsoleHelper
    {
        /// <summary>
        /// Set message and datetime formats
        /// </summary>
        /// <param name="messageFormat">Message format like as "[{timestamp}]: {message}\n"</param>
        /// <param name="timeFormat">Default dd.MM.yyyy HH:mm:ss:ffffff</param>
        void SetMessageFormat(string messageFormat, string timeFormat = null);

        /// <summary>
        /// Ask user for confirmation
        /// </summary>
        /// <returns>bool true/false</returns>
        bool GetToStop();

        /// <summary>
        /// Read confirmation line
        /// </summary>
        /// <returns>String entered in console</returns>
        string ReadLine();

        /// <summary>
        /// Ask user for confirmation Y/N
        /// </summary>
        /// <param name="message">Message for user</param>
        /// <param name="defaultYes">Default value (Y)</param>
        /// <returns>bool true/false depending on set</returns>
        bool GetYorN(string message, bool defaultYes = true);

        /// <summary>
        /// Get string entered in console
        /// </summary>
        /// <param name="message">Message for user</param>
        /// <param name="maybeNull">Value may be null</param>
        /// <returns>String entered in console</returns>
        string GetStringFromConsole(string message, bool maybeNull = false);

        /// <summary>
        /// Get int number entered in console
        /// </summary>
        /// <param name="message">Message for user</param>
        /// <param name="defaultValue">Default value (0)</param>
        /// <returns>int entered in console</returns>
        int GetIntFromConsole(string message, int defaultValue = 0);

        /// <summary>
        /// Display console message in the specified format
        /// </summary>
        /// <param name="message">Message for user</param>
        /// <param name="color">Text color (default white)</param>
        void WriteMessageWithTimeStamp(string message, ConsoleColor? color = ConsoleColor.White);
    }
}
