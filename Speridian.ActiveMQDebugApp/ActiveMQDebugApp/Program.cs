using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Apache.NMS;
using Apache.NMS.ActiveMQ;
using Serilog;
using Serilog.Core;


namespace ActiveMQDebugApp
{
    internal class Program
    {
        //private static readonly ILogger logger = new LoggerConfiguration().WriteTo.File("C:/Users/sam.chacko/Desktop/Sooraj/Speridian.ActiveMQDebugApp/Log/Debuglog.txt").CreateLogger();
        static void Main(string[] args)
        {
            string brokerUri = "tcp://127.0.0.1:61621";
               
            try
            {
                //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                IConnectionFactory factory = new ConnectionFactory(brokerUri);
                using (IConnection connection = factory.CreateConnection())
                {
                    connection.Start();
                    using (ISession session = connection.CreateSession(AcknowledgementMode.AutoAcknowledge))
                    {
                        IDestination destination = session.GetQueue("DebugQueue");
                        using (IMessageProducer producer = session.CreateProducer(destination))
                        {
                            ITextMessage textMessage = producer.CreateTextMessage("Helllo Sam");
                            producer.Send(textMessage);
                            Console.WriteLine("Message Sent");
                            //logger.Information("\nMessage Sent");
                            //logger.Information("\n===============================================\n");
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                //logger.Error(e.ToString());
                //logger.Error("\n===============================================\n");
            }
        }
    }
}
