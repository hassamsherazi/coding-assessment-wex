using DTO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace Service
{
    public class LoggingService : ILoggingService
    {
        public List<DayLog> GetLogData(string _filePath)
        {

            try
            {
                string filePath = Path.GetFullPath(_filePath);
                string jsonString = File.ReadAllText(filePath);

                List<DayLog> daylog = JsonSerializer.Deserialize<List<DayLog>>(jsonString);

                return daylog;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
