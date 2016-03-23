using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GIC.Common
{
    public class MiscUtility
    {
        public static void LogHistory(string text)
        {
            string logfilename = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "History.log");
            FileUtility.SaveFile(logfilename, string.Format("[{0}] - {1}", DateTime.Now.ToString(), text));
        }
    }
}
