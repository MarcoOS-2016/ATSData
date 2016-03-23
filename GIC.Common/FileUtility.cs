using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GIC.Common
{
    public class FileUtility
    {
        public static void SaveFile(string filename, string text)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(filename, true))
                {
                    writer.WriteLine(text);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Saving the file - {0} failed: {1}", filename, ex.Message));
            }
        }
    }
}
