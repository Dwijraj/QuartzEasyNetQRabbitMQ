using EasyNetQ;
using EasyNetQ.Scheduling;
using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using IScheduler = EasyNetQ.Scheduling.IScheduler;

namespace QuartzEastNetQ
{
    class Program
    {
        public static IBus bus;
        private static Quartz.IScheduler _scheduler { get; set; }
        static void Main(string[] args)
        {

            bus = RabbitHutch.CreateBus($"host=localhost");

            StartJobs();
            Console.ReadKey();
        }
        public async static void StartJobs()
        {
            _scheduler =await StdSchedulerFactory.GetDefaultScheduler();

            await _scheduler.Start();

         //   SendRabbitMQMessages();
            
            ReceiveRabbitMQMessages();
            SendRabbitMQMessages();
        }

        private static void ReceiveRabbitMQMessages()
        {
            IJobDetail neoloadtestexecutorJob = JobBuilder.Create<ReceiveRabbitMQJob>()
               .WithIdentity("Receive", "Receive")
               .Build();

            ITrigger neoloadtestexecutorjobTrigger = TriggerBuilder.Create()
                .WithIdentity("Trigger", "Trigger which kicks start the IJob")
                .StartNow()
                //.WithSimpleSchedule(x => x
                //    .WithIntervalInSeconds(20)
                //    .RepeatForever())
                .Build();

            _scheduler.ScheduleJob(neoloadtestexecutorJob, neoloadtestexecutorjobTrigger);

        }

        private static void SendRabbitMQMessages()
        {
           
                IJobDetail neoloadtestexecutorJob1 = JobBuilder.Create<SendRabbitMQJob>()
                .WithIdentity("Send", "Send")
                .Build();

                ITrigger neoloadtestexecutorjobTrigger1 = TriggerBuilder.Create()
                    .WithIdentity("Trigger2", "Trigger which kicks start the IJob")
                    .StartNow()
                    .WithSimpleSchedule(x => x
                        .WithIntervalInSeconds(10)
                        .RepeatForever())
                    .Build();

            _scheduler.ScheduleJob(neoloadtestexecutorJob1, neoloadtestexecutorjobTrigger1);
        }
    }
}
