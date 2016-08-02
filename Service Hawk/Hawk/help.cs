using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hawk
{
    class help
    {
        //to write error log(exception) on log file
        public static void WriteErrorlog(Exception ex)
        {
            StreamWriter sw = null;

            try
            {
                sw = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "\\Logfile.txt", true);
                sw.WriteLine(DateTime.Now.ToString() + " :" + ex.Source.ToString().Trim() + " ;" + ex.Message.ToString().Trim());
                sw.Flush();
                sw.Close();

            }
            catch

            {

            }

        }
        //to write error log(custom error message) on log file
        public static void WriteErrorlog(String Message)
        {
            StreamWriter sw = null;

            try
            {
                sw = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "\\Logfile.txt", true);
                sw.WriteLine(DateTime.Now.ToString() + " :" + Message);
                sw.Flush();
                sw.Close();

            }
            catch

            {

            }

        }
        //delete record from logfile
        public static void Remove()
        {

            File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "\\Logfile.txt", String.Empty);
        }
    }
}
