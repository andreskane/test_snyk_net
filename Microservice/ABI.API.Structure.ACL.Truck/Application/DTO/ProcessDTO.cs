using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace ABI.API.Structure.ACL.Truck.Application.DTO
{
    public class ProcessDTO
    {
        public int? StructureId { get; set; }
        private Stopwatch ProcessStopwatch { get; set; }
        public string ProcessStartDateTime { get; set; }
        public string ProcessTime { get; set; }

        public List<string> ProcessLog { get; set; }
        public List<string> ProcessError { get; set; }

        public ProcessDTO()
        {
            ProcessStopwatch = new Stopwatch();
            ProcessLog = new List<string>();
            ProcessError = new List<string>();
        }

        public string GetDateTime()
        {
            return DateTimeOffset.UtcNow.ToString("{dd/MM/yyyy HH:mm:ss zzz}");
        }

        public void Start()
        {
            ProcessStartDateTime = GetDateTime();
            ProcessStopwatch.Start();
        }

        public void Stop()
        {
            ProcessStopwatch.Stop();
            ProcessTime = ProcessStopwatch.Elapsed.ToString("hh\\:mm\\:ss\\.fff");
        }

        public void AddLog(string text)
        {
            var time = ProcessStopwatch.Elapsed.ToString("hh\\:mm\\:ss\\.fff");
            ProcessLog.Add($"{GetDateTime()} - {text} - {time}");
        }

        public void AddError(string text)
        {
            var time = ProcessStopwatch.Elapsed.ToString("hh\\:mm\\:ss\\.fff");
            ProcessError.Add($"{GetDateTime()} - {text} - {time}");
        }
    }
}
