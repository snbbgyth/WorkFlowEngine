using System;
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
            using (var host = new ServiceHost(typeof (WorkFlowWCFService.WorkFlowService)))
            {
                //注册启动时事件
                host.Opened += delegate
                                   {
                                       Console.WriteLine("[Server] Begins to listen request on " +
                                                         host.BaseAddresses[0]);
                                   };
                host.Open();
                Console.Read();
            }
        }
    }
}
