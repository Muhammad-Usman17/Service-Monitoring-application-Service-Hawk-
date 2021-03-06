﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.ServiceProcess;
using System.Windows;
using System.Windows.Controls;

namespace Service_Hawk
{
    /// <summary>
    /// Interaction logic for InstallUnistallWindow.xaml
    /// </summary>
    public partial class InstallUnistallWindow : Window
    {
        List<string> allservices;
        public InstallUnistallWindow()
        {
            InitializeComponent();
            allservices = new List<string>();
            System.ServiceProcess.ServiceController[] services;
            services = System.ServiceProcess.ServiceController.GetServices();
            allservices.Clear();
            for (int i = 0; i < services.Length; i++)
            {
                //if (services[i].ServiceName != "Falcon" && services[i].ServiceName != "follow_falcon")
                //{
                    allservices.Add(services[i].ServiceName);
                //}
            }
            listBox.Visibility = Visibility.Collapsed;

        }
        private void installer(object sender, RoutedEventArgs e)
        {
            if (textBox.Text == "")
            {
                MessageBox.Show("Service Path is empty");

            }
            else
            {
                try
                {

                    InstallService(textBox.Text);
                }
                catch (FileNotFoundException ex)
                {
                    MessageBox.Show("Error!! invalid path");
                }
                catch (Exception t)
                {
                    MessageBox.Show(t.Message);
                }

            }

        } 


        public void InstallService(string pathToAssembly)
        {
            System.Configuration.Install.ManagedInstallerClass.InstallHelper(new string[] { pathToAssembly });
            MessageBox.Show("Installed Sucessfully...");

        }
        private void uninstaller(object sender, RoutedEventArgs e)
        {
            Boolean check = false;
            foreach (string itm in allservices)
            {
                if (textBox1.Text != itm)
                    check = false;
                else
                {
                    check = true;
                    break;
                }
            }

            if (check == true)
            {

                ServiceController sc = new ServiceController(textBox1.Text);
                try
                {
                    if (sc.Status.Equals(ServiceControllerStatus.Running))
                    {

                        sc.Stop();
                    }
                }
                catch (Exception ee)
                {
                    MessageBox.Show(ee.Message);
                }

                try
                {
                    Log.func.writefilebat(textBox1.Text, System.IO.Directory.GetCurrentDirectory() + @"\delete.bat");
                    System.Diagnostics.Process Proc = new System.Diagnostics.Process();
                    Proc.StartInfo.FileName = System.IO.Directory.GetCurrentDirectory() + @"\delete.bat";
                    Proc.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                    Proc.Start();
                    MessageBox.Show("Uninstalled Sucessfully...");
                }
                catch (Exception ee)
                {
                    MessageBox.Show(ee.Message);
                }
            }
            else
                MessageBox.Show("ERR0R ! You entered invalid Service ...");

        }


        private void selectpath(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();


            dlg.DefaultExt = ".exe";
            dlg.Filter = "Service Exe Files (*.exe)|*.exe";



            Nullable<bool> result = dlg.ShowDialog();



            if (result == true)
            {

                string filename = dlg.FileName;
                textBox.Text = filename;
            }
        }


        private void textBox1_TextChanged(object sender, TextChangedEventArgs e)
        {
            String typed = textBox1.Text;
            List<String> auto = new List<string>();
            auto.Clear();
            foreach (String item in allservices)
            {
                if (!String.IsNullOrEmpty(textBox1.Text))
                {
                    if (item.StartsWith(typed))
                    {
                        auto.Add(item);
                    }
                }

            }
            if (auto.Count > 0)
            {
                listBox.ItemsSource = auto;
                listBox.Visibility = Visibility.Visible;
            }
            else if (textBox1.Text.Equals(""))
            {

                listBox.Visibility = Visibility.Collapsed;
                listBox.ItemsSource = null;
            }
            else
            {
                listBox.Visibility = Visibility.Collapsed;
                listBox.ItemsSource = null;

            }


        }
        private void listBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listBox.ItemsSource != null)
            {
                listBox.Visibility = Visibility.Collapsed;
                textBox1.TextChanged -= new TextChangedEventHandler(textBox1_TextChanged);
                if (listBox.SelectedIndex != -1)
                {
                    textBox1.Text = listBox.SelectedItem.ToString();
                }
                textBox1.TextChanged += new TextChangedEventHandler(textBox1_TextChanged);
            }

        }


        private void homescreen(object sender, RoutedEventArgs e)
        {
            this.Hide();
            Home W = new Home();
            W.Show();
            this.Close();
        }






        private void button3_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
