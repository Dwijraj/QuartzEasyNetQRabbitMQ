﻿using EasyNetQ;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuartzEastNetQ
{
    class SendRabbitMQJob : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            Console.Write("Starting Write...");
            SendToQueue();

            return Task.CompletedTask;
        }
        private void SendToQueue()
        {
            for (int i = 0; i < 100; i++)
            {
                Console.Write("Sending Message to Queue");
                Program.bus.Send("MyTestQueue", "TestObject");

            }

        }
    }
}
