using DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service
{
    public interface ILoggingService
    {
        List<DayLog> GetLogData(string _filePath);
    }
}
