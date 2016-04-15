using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using GIC.Business;

namespace MainForm
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MainForm());
            }
            else
            {
                ReportHandler handler = new ReportHandler(args[0].ToUpper(), args[1].ToUpper());
                //Console.WriteLine(args[0].ToUpper());
                handler.Process();
            }
            
        }
    }
}
