
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using NLog;
using NLog.Config;
using NLog.Targets;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace Project.LoggingSystem{


    public static class Logging
    {
        private static readonly Dictionary<string, NLog.Logger> Loggers_cache = new Dictionary<string, NLog.Logger>();


        private static string log_folder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "MyGames/HexMarks/Logs");
        /// <summary>
        /// NLog initialization
        /// </summary>
        static Logging()
        {

            var config = new LoggingConfiguration();
                   
            var debug_logFile = new FileTarget("debug_logfile")
            {
                Layout = "[${longdate}][${level}][${logger}] MESSAGE: ${message} ${exception:format=tostring}",
                FileName = Path.Combine(log_folder, "[${shortdate}]debug.log"),
                KeepFileOpen = true,
                ConcurrentWrites = false,
                ArchiveNumbering = ArchiveNumberingMode.Date,
                ArchiveEvery = FileArchivePeriod.Day,
                MaxArchiveFiles = 1
            }; 
            var errors_logFile = new FileTarget("errors_logfile")
            {
                Layout = "$[{longdate}][${level}][${logger}] MESSAGE: ${message} ${exception:format=tostring}",
                FileName = Path.Combine(log_folder, "[${shortdate}]errors.log"),
                KeepFileOpen = false,
                ConcurrentWrites = false,
                ArchiveNumbering = ArchiveNumberingMode.Date,
                ArchiveEvery = FileArchivePeriod.Day,
                MaxArchiveFiles = 1
            };

            config.AddRule(LogLevel.Trace, LogLevel.Warn, debug_logFile);
            config.AddRule(LogLevel.Error, LogLevel.Fatal, errors_logFile);


            LogManager.Configuration = config;
        }

        #region Public Logging Methods

        public static void Info(string message,
            [CallerMemberName] string callerMemberName = "",
            [CallerFilePath] string callerFilePath = "",
            [CallerLineNumber] int callerLineNumber = 0)
        {
            Log(LogLevel.Info, message, callerMemberName, callerFilePath, callerLineNumber);
        }

        public static void Info(Exception exception, string message = "",
            [CallerMemberName] string callerMemberName = "",
            [CallerFilePath] string callerFilePath = "",
            [CallerLineNumber] int callerLineNumber = 0)
        {
            Log(LogLevel.Info, exception, message, callerMemberName, callerFilePath, callerLineNumber);
        }

        public static void Warn(string message,
            [CallerMemberName] string callerMemberName = "",
            [CallerFilePath] string callerFilePath = "",
            [CallerLineNumber] int callerLineNumber = 0)
        {
            Log(LogLevel.Warn, message, callerMemberName, callerFilePath, callerLineNumber);
        }

        public static void Warn(Exception exception, string message = "",
            [CallerMemberName] string callerMemberName = "",
            [CallerFilePath] string callerFilePath = "",
            [CallerLineNumber] int callerLineNumber = 0)
        {
            Log(LogLevel.Warn, exception, message, callerMemberName, callerFilePath, callerLineNumber);
        }

        public static void Error(string message,
            [CallerMemberName] string callerMemberName = "",
            [CallerFilePath] string callerFilePath = "",
            [CallerLineNumber] int callerLineNumber = 0)
        {
            Log(LogLevel.Error, message, callerMemberName, callerFilePath, callerLineNumber);
        }

        public static void Error(Exception exception, string message = "",
            [CallerMemberName] string callerMemberName = "",
            [CallerFilePath] string callerFilePath = "",
            [CallerLineNumber] int callerLineNumber = 0)
        {
            Log(LogLevel.Error, exception, message, callerMemberName, callerFilePath, callerLineNumber);
        }

        #endregion

        private static void Log(LogLevel logLevel, string message,
            string callerMemberName = "", string callerFilePath = "", int callerLineNumber = 0)
        {
            Log(logLevel, null, message, callerMemberName, callerFilePath, callerLineNumber);
        }

        private static void Log(LogLevel logLevel, Exception exception, string message = "",
            string callerMemberName = "", string callerFilePath = "", int callerLineNumber = 0)
        {
            string logMessage = $"({callerMemberName}:{callerLineNumber}) {message}";
            
            var logger = GetLogger(callerFilePath);
            logger.Log(logLevel, exception, logMessage);
        }

        private static NLog.Logger GetLogger(string callerFilePath)
        {
            var logger_name = Path.GetFileName(callerFilePath);

            if (Loggers_cache.TryGetValue(logger_name, out var logger)){
                return logger;
            }
            logger = LogManager.GetLogger(logger_name);
            
            Loggers_cache.Add(logger_name, logger);

            return logger;
        }
    }
}
