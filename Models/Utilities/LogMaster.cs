using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MyRealWorld.Models.Utilities
{
    public class LogMaster
    {
        protected string Path { get; }
        public LogMaster()
        {
            Path = @"C:\MyRealWorld\Data\Logs";
            if(!Directory.Exists(Path))
            {
                Directory.CreateDirectory(Path);
            }
        }
        public void SetLog(string log)
        {
            DateTime dt = DateTime.Now;
            string filename = $"MyRealWorld_{dt.ToShortDateString().Replace("/", "_")}.log";

            if (!File.Exists($"{Path}\\{filename}"))
            {
                using (StreamWriter sw = File.CreateText($"{Path}\\{filename}"))
                {
                    sw.WriteLine(log);
                }
            }
        }
        public void SetLogException(string classfrom, string function, string exception)
        {
            string msg = $"{classfrom}->{function}->{exception}";
            SetLog(msg);
        } 

    }
}
