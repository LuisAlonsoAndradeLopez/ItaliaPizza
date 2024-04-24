using System;
using System.IO;
using System.Windows;

namespace ItalianPizza.Auxiliary
{
    public class ExceptionLogger
    {
        public void LogException(Exception ex)
        {
            string incompletePath = Path.GetFullPath("error_log.txt");
            string pathPartToDelete = "ItalianPizza\\bin\\Debug\\";
            string completePath = incompletePath.Replace(pathPartToDelete, "");

            try
            {
                using (StreamWriter writer = new StreamWriter(completePath, true))
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
