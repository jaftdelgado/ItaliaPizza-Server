using System;
using System.IO;

namespace ServerUtilities
{
    public static class Logger
    {
        private static readonly string logFilePath = "ServerLogger.log";

        public static void Log(string message)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(logFilePath, true))
                {
                    writer.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - {message}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al escribir en el archivo de log: {ex.Message}");

            }
        }
    }
}

