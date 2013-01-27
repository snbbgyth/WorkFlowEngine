﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using WorkFlowWCFService;

namespace ConsoleWorkFlow
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            StartListener();
        }

        private static void StartListener()
        {
            //WCF配置信息在App.Config中
            using (var host = new ServiceHserviceHost = new ServiceHost(typeof(WorkFlowWCFService.WorkFlowService)))
            {
                serviceHost.Open();
                Console.WriteLine("The server is ready.");
                DisplayServerInfo(serviceHost);
           　
                Console.WriteLine("Press the Enter key to terminate the service");
                Console.ReadLine();
            }
        }
        static void DisplayServerInfo(ServiceHost serviceHost)
        {
            Console.WriteLine();
            Console.WriteLine("****** Host Info ******");
            foreach (var endpoint in serviceHost.Description.Endpoints)
            {
                Console.WriteLine("The address is {0}", endpoint.Address);
                Console.WriteLine("The binding is {0}", endpoint.Binding.Name);
                Console.WriteLine("The contract is {0}", endpoint.Contract.Name);
                Console.WriteLine();
            }
            Console.WriteLine("***********************");
        }
    }
}
