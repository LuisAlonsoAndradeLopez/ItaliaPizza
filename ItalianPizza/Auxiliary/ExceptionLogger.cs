using System;
using System.IO;

namespace ItalianPizza.Auxiliary
{
    public class ExceptionLogger
    {
        public void LogException(Exception ex)
        {
            string logFilePath = "C:\\Users\\Luis Alonso\\Documents\\ItaliaPizza\\error_log.txt";
            try
            {
                using (StreamWriter writer = new StreamWriter(logFilePath, true))
                {
                    writer.WriteLine($"Timestamp: {DateTime.Now}");
                    writer.WriteLine("Exception Details:");
                    writer.WriteLine(ex.ToString());
                    writer.WriteLine(new string('-', 50));
                    writer.WriteLine();
                }
            }
            catch (Exception) { }
        }
    }
}
