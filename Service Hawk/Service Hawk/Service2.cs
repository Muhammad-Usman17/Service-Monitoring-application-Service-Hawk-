using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Service_Falcon
{
    public partial class Service2 : ServiceBase
    {
        Timer tmr1 = null;
        public Service2()
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
        public string[] readme()
        {
            List<string> list = new List<string>();
            string line;
            string[] service;


            // Read the file and display it line by line.
            System.IO.StreamReader file =
                   new System.IO.StreamReader(@"C:\Users\Muhammad_Usman\Documents\Visual Studio 2015\Service-Monitoring-application-Service-Hawk-\Service Hawk\Service Hawk\bin\Debug\backup.txt");
            while ((line = file.ReadLine()) != null)
            {
                list.Add(line);
            }
            service = list.ToArray();
            file.Close();

            return service;
        }


        protected override void OnStop()
        {
            tmr1.Enabled = true;
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            ServiceController sc = new System.ServiceProcess.ServiceController("Falcon");
            if (sc.Status.Equals(ServiceControllerStatus.Stopped))
            {
                if (readme() != null)



                    sc.Start(readme());

            }
        }

    }
}