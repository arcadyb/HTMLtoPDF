using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

//use: STLogger.AppLog.Log("Test", Severity.info);
namespace STHtmlToPdf
{
    public delegate void SetFormTextCallback(string txtValue, Severity severity);
    public enum Severity
    {
        info ,
        warning,
        error,
        fatal,
        important
    }
    public class ItemObject : Object
    {
        public ItemObject(string message, Severity severity)
        {
            TimeStamp = DateTime.Now.ToString();
            Message = message;
            Severity = severity;
        }
        public string TimeStamp { set; get; }
        public string Message { set; get; }
        public Severity Severity { set; get; }
        public override string ToString()
        {
            return Message;
        }
    }
    public interface ILogger
    {
        void Log(string message, Severity level, [CallerFilePath] string filePath = "",
        [CallerLineNumber] int lineNumber = 0);
    }
    public class STLogger : ILogger
    {
        private Object locker = new Object();
        public SetFormTextCallback ReportDelegate { set; get; }
        public void Log(string message, Severity level, [CallerFilePath] string filePath = "",
        [CallerLineNumber] int lineNumber = 0)
        {
            lock (locker)
            {
                //if (STLogger.AppLog.ReportDelegate != null)
                //    STLogger.AppLog.ReportDelegate(message, level.ToString(), level);
                LogFull(message, level, filePath, lineNumber);
            }
        }
        public void LogFull(string message, Severity level,
            [CallerFilePath] string filePath = "",
        [CallerLineNumber] int lineNumber = 0)
        {
            string str = string.Format("{0};{1};{2}({3}) ;  \t{4}", DateTime.Now.ToString(), level.ToString(), filePath, lineNumber, message);
            string calledfrom = new StackTrace().GetFrame(1).GetMethod().Name;
            if (ReportDelegate != null)
                    ReportDelegate(str, level);
            STLogger.WriteErrorLog(str + " Caller:" + new StackTrace().GetFrame(2).GetMethod().Name,level);
        }
        public static string LogFile
        {
            get
            {
                DateTime dateAndTime = DateTime.Now;
                int year = dateAndTime.Year;
                int month = dateAndTime.Month;
                int day = dateAndTime.Day;

                string prefix = string.Format("{0}_{1}_{2}_", day, month,  year);
                string filename = prefix + "STRenderServerLog.txt";
                return filename;
            }
        }
        public static string LogRoot
        {
            get
            {
                return Path.Combine("c:\\servi-tech\\", "Logs");
            }
        }
        public  static string LogPath
        {
            get
            {
                DateTime dateAndTime = DateTime.Now;
                int year = dateAndTime.Year;
                int month = dateAndTime.Month;
                string datefolder = string.Format("{0}_{1}",  month, year);     
                return Path.Combine( LogRoot, datefolder, LogFile);
            }
        }
        private static void WriteErrorLog(Exception ex)
        {
            if(string.IsNullOrEmpty(LogPath))
            {
                string installdir = "c:\\servi-tech";
                
            }
            string folder = System.IO.Path.GetDirectoryName(LogPath);
            if (!System.IO.Directory.Exists(folder))
            {
                System.IO.Directory.CreateDirectory(folder);
            }
            StreamWriter sw = null;
            //AppDomain.CurrentDomain.BaseDirectory + "\\STFWLogFile.txt"
                using (sw = new StreamWriter(LogPath, true))
                {
                try
                {
                    sw.WriteLine(DateTime.Now.ToString() + ";error;Exception " + ex.Source.ToString().Trim() + "; " + ex.Message.ToString().Trim());
                    sw.Flush();
                    sw.Close();
                }
                catch (Exception)
                {

                    sw?.Flush();
                    sw?.Close();
                }
                }
            }
 
        

        private static void WriteErrorLog(string Message,Severity severity)
        {
            string folder = System.IO.Path.GetDirectoryName(LogPath);
            if (!System.IO.Directory.Exists(folder))
            {
                System.IO.Directory.CreateDirectory(folder);
            }
            StreamWriter sw = null;
 
           // FileStream s2 = new FileStream(LogPath, FileMode.Append, FileAccess.Write, FileShare.Write);
            try
            {
                //FileStream s2 = new FileStream(LogPath, FileMode.Append, FileAccess.Write, FileShare.Write);
                FileStream s2 = new FileStream(LogPath, FileMode.Append, FileAccess.Write, FileShare.Write);

                using (sw = new StreamWriter(s2))
                {
                    try
                    {
                        sw.WriteLine(DateTime.Now.ToString() + "; " + severity.ToString() + ";" + Message.Replace("\n", "|"));
                        sw.Flush();
                        s2.Close();
                    }
                    catch (Exception)
                    {
                        sw?.Flush();
                        s2?.Close();
                    }
                }
            }
            catch (Exception ex)
            {

                
            }

 
        }
        public static STLogger AppLog { set; get; } = new STLogger();
    }
}
