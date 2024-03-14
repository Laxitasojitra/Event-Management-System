using EventManagementSystem;
using EventSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace EventManagementServiceHost
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Type t = typeof(AdminService);
            Uri tcp = new Uri("net.tcp://localhost:8010/AdminService");
            Uri http = new Uri("http://localhost:8180/AdminService");
            ServiceHost host  = new ServiceHost(t, tcp, http);
            try
            {
                host.Open();
                Console.WriteLine("Published");
                Console.ReadLine();
            }
            finally
            {
                host.Close();
            }
        }
    }
}
