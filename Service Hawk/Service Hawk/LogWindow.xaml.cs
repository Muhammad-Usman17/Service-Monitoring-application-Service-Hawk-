using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
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
    /// Interaction logic for LogWindow.xaml
    /// </summary>
    public partial class LogWindow : Window
    {
        System.Windows.Threading.DispatcherTimer timer = new System.Windows.Threading.DispatcherTimer();
        public LogWindow()
        {
            InitializeComponent();
            timer.Tick += new EventHandler(Timer_click);
            timer.Interval = new TimeSpan(0, 0, 5);
            timer.Start();


        }
        private void Timer_click(object sender, EventArgs e)
        {
            textBox.Text = Log.func.readlastError();
        }

        private void openlogfile(object sender, RoutedEventArgs e)
        {
            try
            {
                Process.Start(System.IO.Directory.GetCurrentDirectory() + @"\Logfile.txt");
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
        }

        private void printlog(object sender, RoutedEventArgs e)
        {
            try
            {
                Log.func.PrintFile(System.IO.Directory.GetCurrentDirectory() + @"\Logfile.txt");
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
        }

        private void deletelog(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure to Clear the logfile?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    File.WriteAllText(System.IO.Directory.GetCurrentDirectory() + @"\Logfile.txt", String.Empty);
                }
                catch (Exception ee)
                {
                    MessageBox.Show(ee.Message);
                }
            }

        }

        private void homescreen(object sender, RoutedEventArgs e)
        {
            this.Hide();
            Home W = new Home();
            W.Show();
            this.Close();
        }
    }
}

