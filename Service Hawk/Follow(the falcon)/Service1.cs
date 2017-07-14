using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Follow_the_falcon_
{
    public partial class Service1 : ServiceBase
    {
        Timer tmr1 = null;
        public Service1()
        {
            InitializeComponent();


        }

        protected override void OnStart(string[] args)
        {
            tmr1 = new Timer();
            this.tmr1.Interval = 3000;
            this.tmr1.Elapsed += new System.Timers.ElapsedEventHandler(this.timer1_Tick);
            tmr1.Enabled = true;

        }
        //public string[] readme()
        //{
        //    List<string> list = new List<string>();
        //    string line;
        //    string[] service;


        //    // Read the file and display it line by line.
        //    System.IO.StreamReader file =
        //           new System.IO.StreamReader(System.IO.Directory.GetCurrentDirectory() + @"\backup.txt");

        //    while ((line = file.ReadLine()) != null)
        //    {
        //        list.Add(line);
        //    }
        //    service = list.ToArray();
        //    file.Close();

        //    return service;
        //}


        protected override void OnStop()
        {
            tmr1.Enabled = true;
        }
        private void timer1_Tick(object sender, EventArgs e)
        {

            ServiceController sc = new System.ServiceProcess.ServiceController("Falcon");
            sc.Refresh();
            if (!sc.Status.Equals(ServiceControllerStatus.Running))
            {
                if (new FileInfo(System.IO.Path.GetDirectoryName(new System.Uri(System.Reflection.Assembly.GetExecutingAssembly().CodeBase).LocalPath) + @"\backup.txt").Length == 0)
                {
                    // empty
                }
                else
                {
                    List<string> list = new List<string>();

                    string[] service;


                    //// Read the file and display it line by line.
                    string line = "";
                    using (StreamReader sr = new StreamReader(System.IO.Path.GetDirectoryName(new System.Uri(System.Reflection.Assembly.GetExecutingAssembly().CodeBase).LocalPath)+ @"\backup.txt"))
                    {
                        while ((line = sr.ReadLine()) != null)
                        {
                            list.Add(line);
                        }
                    }


                    service = list.ToArray();

                    try
                    {

                        sc.Start(service);
                    }
                    catch (Exception ee)
                    {
                        StreamWriter sw = null;
                        sw = new StreamWriter("Log.txt", true);
                        sw.WriteLine(DateTime.Now.ToString() + " :" + ee.Source.ToString().Trim() + " ;" + ee.Message.ToString().Trim());
                        sw.Flush();
                        sw.Close();
                    }
                }






            }

        }
    }
}