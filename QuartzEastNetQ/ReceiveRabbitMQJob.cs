using System;
using System.Threading.Tasks;
using EasyNetQ;
using Quartz;

namespace QuartzEastNetQ
{
    public class ReceiveRabbitMQJob : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            ReceiveFromQueue();
            return Task.CompletedTask;
        }


        private void ReceiveFromQueue()
        {
            Program.bus.Receive<String>("MyTestQueue", (message) =>
            {

                Console.Write("GotMessage");

            });
        }
    }

}