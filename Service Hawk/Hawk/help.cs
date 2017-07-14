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
           

            try
            {
                using (StreamWriter sw = new StreamWriter((System.IO.Path.GetDirectoryName(new System.Uri(System.Reflection.Assembly.GetExecutingAssembly().CodeBase).LocalPath)+ @"\Logfile.txt"),true))
                {

                
                    sw.WriteLine(DateTime.Now.ToString() + " :" + ex.Source.ToString().Trim() + " ;" + ex.Message.ToString().Trim());
              
                    sw.Close();
                }

            }
            catch

            {

            }

        }
        //to write error log(custom error message) on log file
        public static void WriteErrorlog(String Message)
        {
     

            try
            {
                using (StreamWriter sw = new StreamWriter((System.IO.Path.GetDirectoryName(new System.Uri(System.Reflection.Assembly.GetExecutingAssembly().CodeBase).LocalPath)+ @"\Logfile.txt"),true))
                {


                    sw.WriteLine(DateTime.Now.ToString() + " :" + Message);
                    
                    sw.Close();
                }
            }
            catch

            {

            }

        }
        //delete record from logfile
        public static void Remove()
        {

            File.WriteAllText(System.IO.Path.GetDirectoryName(new System.Uri(System.Reflection.Assembly.GetExecutingAssembly().CodeBase).LocalPath)+"\\Logfile.txt", String.Empty);
        }
    }
}
