using System;
using System.Collections.Generic;
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
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        System.Windows.Threading.DispatcherTimer timer = new System.Windows.Threading.DispatcherTimer();
        public Login()
        {
            InitializeComponent();
            timer.Tick += new EventHandler(Timer_click);
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Start();

            textBox.Focus();
        }
        private void Timer_click(object sender, EventArgs e)
        {
            label4.Content = DateTime.Now.ToString("HH:mm:ss") + " " + DateTime.Now.ToLongDateString();
        }
        private void login(object sender, RoutedEventArgs e)
        {

            string admin = textBox.Text.ToString();
            string password = passwordBox.Password.ToString();
            if (admin.Equals(ConfigUpdate.File.GetSetting("Admin")) && password.Equals(ConfigUpdate.File.GetSetting("Password")))
            {


                Home h = new Home();
                this.Hide();
                h.Show();
                this.Close();
            }
            else
                MessageBox.Show("admin or passsword is wrong!!");
            passwordBox.Clear();

        }

        private void lostpassword(object sender, RoutedEventArgs e)
        {
            string Password = "Your Password is  " + ConfigUpdate.File.GetSetting("Password");
            Mail.func.sendNotification(Password, ConfigUpdate.File.GetSetting("SystemMail"), ConfigUpdate.File.GetSetting("AdminEMail"));
            MessageBox.Show("Password details are sent to your Email address");
        }
    


    }
}
