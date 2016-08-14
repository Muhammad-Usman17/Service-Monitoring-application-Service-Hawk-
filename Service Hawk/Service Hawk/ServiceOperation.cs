using System;
using System.Collections.Generic;
using System.IO;
using System.ServiceProcess;
using System.Windows;
using System.Windows.Controls;

namespace Service_Hawk
{
    class ServiceOperation
    {
        private static ServiceOperation obj;

        public static  ServiceOperation Func
        {
            get
            {
                if (obj == null)
                    obj = new ServiceOperation();
                return obj;
            }
        }
             public void InstallService(string pathToAssembly)
        {

            System.Configuration.Install.ManagedInstallerClass.InstallHelper(new string[] { pathToAssembly });

        }
    }
}
