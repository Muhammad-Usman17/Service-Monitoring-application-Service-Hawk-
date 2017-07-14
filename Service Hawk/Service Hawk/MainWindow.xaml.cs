using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Principal;
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
            if (!IsRunAsAdministrator())
            {
                var processInfo = new ProcessStartInfo(Assembly.GetExecutingAssembly().CodeBase);

                // The following properties run the new process as administrator
                processInfo.UseShellExecute = true;
                processInfo.Verb = "runas";

                // Start the new process
                try
                {
                    Process.Start(processInfo);
                }
                catch (Exception)
                {
                    // The user did not allow the application to run as administrator
                    MessageBox.Show("Sorry, this application must be run as Administrator.");
                }

                // Shut down the current process
                Application.Current.Shutdown();
            }


            InitializeComponent();
            timer.Tick += new EventHandler(Timer_click);
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Start();

        }
        private bool IsRunAsAdministrator()
        {
            var wi = WindowsIdentity.GetCurrent();
            var wp = new WindowsPrincipal(wi);

            return wp.IsInRole(WindowsBuiltInRole.Administrator);
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
                    ServiceOperation.Func.InstallService(System.IO.Directory.GetCurrentDirectory() + @"\Falcon.exe"); ;
                    progressBar.Value += 10;
                }
                ServiceController ctl2 = ServiceController.GetServices().FirstOrDefault(s => s.ServiceName == "follow_falcon");
                if (ctl2 == null)
                {
                    ServiceOperation.Func.InstallService(System.IO.Directory.GetCurrentDirectory() + @"\follow_falcon.exe");
                    progressBar.Value += 10;
                 
                    
                }
                if (!File.Exists(AppDomain.CurrentDomain.BaseDirectory + "\\delete.bat"))
                {
                    File.Create("delete.bat");
                    File.Create("Logfile.txt");
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

