using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STText
{
    public static class TextExt
    {
        public static void OpenPath(this string pathtorun)
        {
            Process myProcess = new Process();
            myProcess.StartInfo.FileName = pathtorun;
            myProcess.Start();
        }
    }
}
