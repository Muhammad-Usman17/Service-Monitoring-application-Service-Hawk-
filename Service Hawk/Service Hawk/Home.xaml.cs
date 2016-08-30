using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Service_Hawk
{
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : Window
    {
        ServiceController sc;


        System.Windows.Threading.DispatcherTimer timer = new System.Windows.Threading.DispatcherTimer();

        public Home()
        {
            InitializeComponent();

            timer.Tick += new EventHandler(Timer_click);
            timer.Interval = new TimeSpan(0, 0, 5);
            timer.Start();


            sc = new System.ServiceProcess.ServiceController("Falcon");
            if (sc.Status.Equals(ServiceControllerStatus.Stopped))
            {
                button2.IsEnabled = true;
                start.IsEnabled = true;
                install.IsEnabled = true;
                uninstall.IsEnabled = true;
                ConfigSetting.IsEnabled = true;
               
                button1.IsEnabled = true;
                button.IsEnabled = true;

                button3.IsEnabled = false;
                button4.IsEnabled = true;
                stop.IsEnabled = false;



            }
            else if (sc.Status.Equals(ServiceControllerStatus.Running))
            {
                button2.IsEnabled = false;
                start.IsEnabled = false;
                install.IsEnabled = false;
                uninstall.IsEnabled = false;
                ConfigSetting.IsEnabled = false;

                button1.IsEnabled = false;
                button.IsEnabled = false;

                button3.IsEnabled = true;
                button4.IsEnabled = false;
                stop.IsEnabled =true;
                String[] ser = Log.func.readfile(ConfigUpdate.File.GetSetting("MoniterList"));
                for (int i = 0; i<ser.Length; i++)
                {
                    listView.Items.Add((String)ser[i]);
                }

            }

        }



        private void StartMonitering(object sender, RoutedEventArgs e)
{
    Start(); 
}


        private void Timer_click(object sender, EventArgs e)
        {
            textBox.Text = Log.func.readlastError();
        }


        private void StopMonitoring(object sender, RoutedEventArgs e)
{
    Stop();
}

private void installUnistallpage(object sender, RoutedEventArgs e)
{
    this.Hide();
    InstallUnistallWindow W5 = new InstallUnistallWindow();
    W5.Show();
    this.Close();

}

private void ShowList(object sender, RoutedEventArgs e)
{
    System.ServiceProcess.ServiceController[] services;
    services = System.ServiceProcess.ServiceController.GetServices();
    listBox.Items.Clear();
    for (int i = 0; i < services.Length; i++)
    {
        if (services[i].ServiceName == "Falcon")
        {

        }
        if (services[i].ServiceName == "follow_falcon")
        {

        }
        else
            listBox.Items.Add(services[i].ServiceName);
    }
}



private void AddtoMonitor(object sender, RoutedEventArgs e)
{
    if ((string)listBox.SelectedItem != ("") && (string)listBox.SelectedItem != null)
    {
        if (listView.Items.Contains((string)listBox.SelectedItem))
        {
            MessageBox.Show("Caution!! you already add " + (string)listBox.SelectedItem + "   to moniter");
        }

        else
            listView.Items.Add((string)listBox.SelectedItem);
    }
    else
        MessageBox.Show(" You have no selected any service");
}



private void RemovefromMonitor(object sender, RoutedEventArgs e)
{
    listView.Items.Remove((string)listView.SelectedItem);
}

private void ConfigurationSettingpage(object sender, RoutedEventArgs e)
{
    this.Hide();
     SettingWindow W2 = new SettingWindow();
    W2.Show();
    this.Close();

}


private void LogPage(object sender, RoutedEventArgs e)
{
    this.Hide();
   LogWindow W3 = new LogWindow();
    W3.Show();
    this.Close();
}


private void Signout(object sender, RoutedEventArgs e)
{
    MessageBoxResult result = MessageBox.Show("Are you sure to Sign out?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
    if (result == MessageBoxResult.Yes)
    {

        this.Hide();
        Login W = new Login();
        W.Show();
        this.Close();

    }
}


public void Start()
{
    if (listView.Items.Count != 0)
    {
        try
        {

            if (sc.Status.Equals(ServiceControllerStatus.Paused))
            {
                sc.Continue();
                sc.WaitForStatus(ServiceControllerStatus.Running);
                        button2.IsEnabled = false;
                        start.IsEnabled = false;
                        install.IsEnabled = false;
                        uninstall.IsEnabled = false;
                        ConfigSetting.IsEnabled = false;

                        button1.IsEnabled = false;
                        button.IsEnabled = false;

                        button3.IsEnabled = true;
                        button4.IsEnabled = false;
                        stop.IsEnabled = true;


                    }
            else
            {
                List<string> list = new List<string>();
                List<string> listtxt = new List<string>();
                list.Add(ConfigUpdate.File.GetSetting("mailhost"));
                list.Add(ConfigUpdate.File.GetSetting("port"));
                list.Add(ConfigUpdate.File.GetSetting("SystemMail"));
                list.Add(ConfigUpdate.File.GetSetting("Systempassword"));
                list.Add(ConfigUpdate.File.GetSetting("AdminEMail"));
                list.Add(ConfigUpdate.File.GetSetting("subject"));
                list.Add(ConfigUpdate.File.GetSetting("body"));
                string item = string.Empty;
                for (int i = 0; i < listView.Items.Count; i++)
                {
                    list.Add(listView.Items[i].ToString());
                    listtxt.Add(listView.Items[i].ToString());

                }


                string[] service = list.ToArray();
                string[] ser = listtxt.ToArray();
                        Log.func.writefile(service);
                Log.func.writefile(ser, ConfigUpdate.File.GetSetting("MoniterList"));
                sc.Start(service);
                sc.WaitForStatus(ServiceControllerStatus.Running);
                if (sc.Status.Equals(ServiceControllerStatus.Running))
                {
                            button2.IsEnabled = false;
                            start.IsEnabled = false;
                            install.IsEnabled = false;
                            uninstall.IsEnabled = false;
                            ConfigSetting.IsEnabled = false;

                            button1.IsEnabled = false;
                            button.IsEnabled = false;

                            button3.IsEnabled = true;
                            button4.IsEnabled = false;
                            stop.IsEnabled = true;

                        }

            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("Could not start " + " Service.\n Error : " + ex.Message.ToString());
        }
        finally
        {
            sc.Close();
        }
    }

    else
        MessageBox.Show("You Must have to select atleast one Service before Start");
}


public void Stop()

{
    try
    {
        sc.Stop();
                Log.func.remove();
        sc.WaitForStatus(ServiceControllerStatus.Stopped);


        if (sc.Status.Equals(ServiceControllerStatus.Stopped))
        {
                    button2.IsEnabled = true;
                    start.IsEnabled = true;
                    install.IsEnabled = true;
                    uninstall.IsEnabled = true;
                    ConfigSetting.IsEnabled = true;

                    button1.IsEnabled = true;
                    button.IsEnabled = true;

                    button3.IsEnabled = false;
                    button4.IsEnabled = true;
                    stop.IsEnabled = false;

                }
    }
    catch (Exception ex)
    {
        MessageBox.Show("Could not stop " + " Service.\n Error : " + ex.Message.ToString());
    }
    finally
    {
        sc.Close();
    }

}



private void exit(object sender, RoutedEventArgs e)
{
    Application.Current.Shutdown();

}






    



        private void button2_Click(object sender, RoutedEventArgs e)
        {

        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {

        }

        private void listView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
