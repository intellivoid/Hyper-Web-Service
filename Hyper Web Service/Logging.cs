using System;
using System.Diagnostics;
using System.IO;

namespace Intellivoid.HyperWS
{
    /// <summary>
    /// Logging Class
    /// </summary>
    public static class Logging
    {
        /// <summary>
        /// If set to True, general logging messages will be displayed in the CLI
        /// </summary>
        public static bool Enabled { get; set; }

        /// <summary>
        /// If set to True, alongside general logging messages; debugging messages will be shown on the CLI
        /// </summary>
        public static bool VerboseLogging { get; set; }

        /// <summary>
        /// The output file to output all the data to (AllowLogging doesn't need to be set to True for this to work)
        /// </summary>
        public static string OutputFile { get; set; } = string.Empty;

        /// <summary>
        /// Writes a vebrose Log Entry
        /// </summary>
        /// <param name="moduleName"></param>
        /// <param name="entryText"></param>
        public static void WriteVerboseEntry(string moduleName, string entryText)
        {
            if (VerboseLogging == false)
            {
                return;
            }
            
            WriteEntry(LogType.Verbose, moduleName, entryText);
        }
        
        /// <summary>
        /// Writes a Log Entry
        /// </summary>
        /// <param name="loggingType"></param>
        /// <param name="moduleName"></param>
        /// <param name="entryText"></param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static void WriteEntry(LogType loggingType, string moduleName, string entryText)
        {
            if(Enabled == false)
            {
                return;
            }
            
            var timestamp = DateTime.Now.ToString(@"h\:mm tt");

            switch (loggingType)
            {
                case LogType.Success:
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write(LoggingResources.WarningSymbol);
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(LoggingResources.Timestamp, timestamp);
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.Write(LoggingResources.ModuleName, moduleName);
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(entryText);
                    Console.WriteLine();
                    Console.ResetColor();
                    break;
                
                case LogType.Information:
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write(LoggingResources.InformationSymbol);
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(LoggingResources.Timestamp, timestamp);
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.Write(LoggingResources.ModuleName, moduleName);
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(entryText);
                    Console.WriteLine();
                    Console.ResetColor();
                    break;

                case LogType.Warning:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(LoggingResources.WarningSymbol);
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(LoggingResources.Timestamp, timestamp);
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.Write(LoggingResources.ModuleName, moduleName);
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(entryText);
                    Console.WriteLine();
                    Console.ResetColor();
                    break;

                case LogType.Error:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write(LoggingResources.ErrorSymbol);
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(LoggingResources.Timestamp, timestamp);
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.Write(LoggingResources.ModuleName, moduleName);
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(entryText);
                    Console.WriteLine();
                    Console.ResetColor();
                    break;

                case LogType.Debug:
                    Console.ForegroundColor = ConsoleColor.DarkMagenta;
                    Console.Write(LoggingResources.DebugSymbol);
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(LoggingResources.Timestamp, timestamp);
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.Write(LoggingResources.ModuleName, moduleName);
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(entryText);
                    Console.WriteLine();
                    Console.ResetColor();
                    break;
                    
                case LogType.Verbose:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(LoggingResources.VerboSymbol);
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(LoggingResources.Timestamp, timestamp);
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.Write(LoggingResources.ModuleName, moduleName);
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(entryText);
                    Console.WriteLine();
                    Console.ResetColor();
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(loggingType), loggingType, null);
            }
        }
    }

    /// <summary>
    /// Log Type Class for CLI Output
    /// </summary>
    public enum LogType
    {
        /// <summary>
        /// Success type message
        /// </summary>
        Success = 0,

        /// <summary>
        /// Information type message
        /// </summary>
        Information = 1,

        /// <summary>
        /// Warning type message
        /// </summary>
        Warning = 2,

        /// <summary>
        /// Error type message
        /// </summary>
        Error = 3,

        /// <summary>
        /// Debug type message
        /// </summary>
        Debug = 4,

        /// <summary>
        /// Verbose type message
        /// </summary>
        Verbose = 5
    }
}
