
using System;
using System.Runtime.CompilerServices;

namespace ImSubShared.Logger
{
    /// <summary>
    /// Provides all the methods for writing logs
    /// </summary>
    public interface IImSubLogger
    {
        /// <summary>
        /// Creates a new log entry with severity "Debug"
        /// </summary>
        /// <param name="telegramId"></param>
        /// <param name="message"></param>
        /// <param name="methodName"></param>
        void Debug(string telegramId, string message, [CallerMemberName] string methodName = "");
        /// <summary>
        /// Creates a new log entry with severity "Debug"
        /// </summary>
        /// <param name="telegramId"></param>
        /// <param name="message"></param>
        /// <param name="details"></param>
        /// <param name="methodName"></param>
        void Debug(string telegramId, string message, string details, [CallerMemberName] string methodName = "");
        /// <summary>
        /// Creates a new log entry with severity "Fatal"
        /// </summary>
        /// <param name="telegramId"></param>
        /// <param name="message"></param>
        /// <param name="methodName"></param>
        void Fatal(string telegramId, string message, [CallerMemberName] string methodName = "");
        /// <summary>
        /// Creates a new log entry with severity "Fatal"
        /// </summary>
        /// <param name="telegramId"></param>
        /// <param name="message"></param>
        /// <param name="details"></param>
        /// <param name="methodName"></param>
        void Fatal(string telegramId, string message, string details, [CallerMemberName] string methodName = "");
        /// <summary>
        /// Creates a new log entry with severity "Fatal"
        /// </summary>
        /// <param name="telegramId"></param>
        /// <param name="message"></param>
        /// <param name="ex"></param>
        /// <param name="methodName"></param>
        void Fatal(string telegramId, string message, Exception ex, [CallerMemberName] string methodName = "");
        /// <summary>
        /// Creates a new log entry with severity "Info"
        /// </summary>
        /// <param name="telegramId"></param>
        /// <param name="message"></param>
        /// <param name="methodName"></param>
        void Info(string telegramId, string message, [CallerMemberName] string methodName = "");
        /// <summary>
        /// Creates a new log entry with severity "Info"
        /// </summary>
        /// <param name="telegramId"></param>
        /// <param name="message"></param>
        /// <param name="details"></param>
        /// <param name="methodName"></param>
        void Info(string telegramId, string message, string details, [CallerMemberName] string methodName = "");
        /// <summary>
        /// Creates a new log entry with severity "Error"
        /// </summary>
        /// <param name="telegramId"></param>
        /// <param name="message"></param>
        /// <param name="methodName"></param>
        void Error(string telegramId, string message, [CallerMemberName] string methodName = "");
        /// <summary>
        /// Creates a new log entry with severity "Error"
        /// </summary>
        /// <param name="telegramId"></param>
        /// <param name="message"></param>
        /// <param name="details"></param>
        /// <param name="methodName"></param>
        void Error(string telegramId, string message, string details, [CallerMemberName] string methodName = "");
        /// <summary>
        /// Creates a new log entry with severity "Error"
        /// </summary>
        /// <param name="telegramId"></param>
        /// <param name="message"></param>
        /// <param name="ex"></param>
        /// <param name="methodName"></param>
        void Error(string telegramId, string message, Exception ex, [CallerMemberName] string methodName = "");
        /// <summary>
        /// Creates a new log entry with severity "Warn"
        /// </summary>
        /// <param name="telegramId"></param>
        /// <param name="message"></param>
        /// <param name="methodName"></param>
        void Warn(string telegramId, string message, [CallerMemberName] string methodName = "");
        /// <summary>
        /// Creates a new log entry with severity "Warn"
        /// </summary>
        /// <param name="telegramId"></param>
        /// <param name="message"></param>
        /// <param name="details"></param>
        /// <param name="methodName"></param>
        void Warn(string telegramId, string message, string details, [CallerMemberName] string methodName = "");
    }
}
