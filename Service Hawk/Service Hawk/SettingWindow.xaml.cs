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
    /// Interaction logic for SettingWindow.xaml
    /// </summary>
    public partial class SettingWindow : Window
    {
        public SettingWindow()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(MainWindow_Loaded);
        }


        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            textBox.Text = ConfigUpdate.File.GetSetting("Admin");
            textBox1.Text = ConfigUpdate.File.GetSetting("Password");
            textBox2.Text = ConfigUpdate.File.GetSetting("mailhost");
            textBox3.Text = ConfigUpdate.File.GetSetting("port");
            textBox4.Text = ConfigUpdate.File.GetSetting("SystemMail");
            textBox5.Text = ConfigUpdate.File.GetSetting("Systempassword");
            textBox6.Text = ConfigUpdate.File.GetSetting("AdminEMail");
            textBox7.Text = ConfigUpdate.File.GetSetting("subject");
            textBox8.Text = ConfigUpdate.File.GetSetting("body");

        }



    private void updatesetting(object sender, RoutedEventArgs e)
    {

        if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "" && textBox5.Text != "" && textBox.Text != "" && Mail.func.emailIsValid(textBox4.Text) && Mail.func.emailIsValid(textBox6.Text))
        {
            ConfigUpdate.File.SetSetting("Admin", textBox.Text.ToString());
            ConfigUpdate.File.SetSetting("Password", textBox1.Text.ToString());
            ConfigUpdate.File.SetSetting("mailhost", textBox2.Text.ToString());
            ConfigUpdate.File.SetSetting("port", textBox3.Text.ToString());
            ConfigUpdate.File.SetSetting("Systempassword", textBox5.Text.ToString());
               
                    ConfigUpdate.File.SetSetting("SystemMail", textBox4.Text);

                    ConfigUpdate.File.SetSetting("AdminEMail", textBox6.Text);
                    ConfigUpdate.File.SetSetting("subject", textBox7.Text);
                    ConfigUpdate.File.SetSetting("body", textBox8.Text);
                  
                
              
                MessageBox.Show("Sucessfully Saved");

        }
              else
                {
                    if (!Mail.func.emailIsValid(textBox1.Text) && !Mail.func.emailIsValid(textBox3.Text))
                    {
                        textBox1.Focus();
                        MessageBox.Show("you entered Invalid SystemMail Address and AdminEMail");

                    }
                    else if (!Mail.func.emailIsValid(textBox1.Text))
                    {
                        textBox1.Focus();
                        MessageBox.Show("you entered Invalid SystemMail Address ");
                    }

                    else if (!Mail.func.emailIsValid(textBox3.Text))
                    {
                        textBox3.Focus();

                        MessageBox.Show("you entered Invalid AdminEMail Address ");
                    }
                    else
                    MessageBox.Show("Error!!one or more field is empty");


            }
           

    }
       

        private void test(object sender, RoutedEventArgs e)
        {
            Mail.func.sendNotification(ConfigUpdate.File.GetSetting("SystemMail"), ConfigUpdate.File.GetSetting("AdminEMail"));
            MessageBox.Show("Email sent sucessfully!! ");
        }


        private void homescreen(object sender, RoutedEventArgs e)
        {
            this.Hide();
           Home W = new Home();
            W.Show();
            this.Close();
        }

        private void textBox3_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(textBox4.Text, "[^0-9]"))
            {
                MessageBox.Show("Please enter only numbers input like (8080,587)etc.");
                textBox4.Clear();
            }

        }
    }
}
