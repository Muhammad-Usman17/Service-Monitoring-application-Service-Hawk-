using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Service_Hawk
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        System.Windows.Threading.DispatcherTimer timer = new System.Windows.Threading.DispatcherTimer();
        public MainWindow()
        {
            InitializeComponent();
            timer.Tick += new EventHandler(Timer_click);
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Start();

        }

        private void Timer_click(object sender, EventArgs e)
        {

            if (progressBar.Value == 100)
            {
                timer.Stop();
                Login h = new Login();
                this.Hide();
                h.Show();
                this.Close();

            }

            else {
                ServiceController ctl = ServiceController.GetServices().FirstOrDefault(s => s.ServiceName == "Falcon");

                if (ctl == null)
                {
                    ServiceOperation.Func.InstallService(@"C:\Users\Muhammad_Usman\Documents\Visual Studio 2015\Service-Monitoring-application-Service-Hawk-\Service Hawk\Hawk\bin\Debug\Hawk.exe    ");

                }
                ServiceController ctl1 = ServiceController.GetServices().FirstOrDefault(s => s.ServiceName == "follow_falcon");
                if (ctl1 == null)
                {
                    ServiceOperation.Func.InstallService(@"C:\Users\Muhammad_Usman\Documents\Visual Studio 2015\Service-Monitoring-application-Service-Hawk-\Service Hawk\Follow(the falcon)\obj\Debug\Follow(the falcon).exe");
                    progressBar.Value += 10;
                 
                    progressBar.Value += 10;
                }
                ServiceController sc = new System.ServiceProcess.ServiceController("follow_falcon");
                if (sc.Status.Equals(ServiceControllerStatus.Stopped))
                {
                    sc.Start();
                }
                else
                    progressBar.Value += 30;


            }
            }

    }






     
    }

