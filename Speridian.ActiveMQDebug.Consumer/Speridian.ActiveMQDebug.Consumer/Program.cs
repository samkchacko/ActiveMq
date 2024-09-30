using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apache.NMS;
using Apache.NMS.ActiveMQ;

namespace Speridian.ActiveMQDebug.Consumer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string brokerUri = "tcp://127.0.0.1:61621";//?jms.prefetchPolicy.queuePrefetch=1
            //"tcp://127.0.0.1:61621";//External
            
            string queueName = "OfficerSched.Hearings.PD200";
                //"OfficerSched.Response.PD200.APD";
                //"OfficerSched.Request";
            try {
                IConnectionFactory factory = new ConnectionFactory(brokerUri);
                Console.WriteLine("Factory Set");
                using (IConnection connection = factory.CreateConnection())
                {
                    Console.WriteLine("Connection going to start");
                    connection.Start();
                    Console.WriteLine("Connection is started");
                    using(ISession session = connection.CreateSession(AcknowledgementMode.AutoAcknowledge))//AcknowledgementMode.AutoAcknowledge
                    {
                        Console.WriteLine("Session created");
                        IDestination destination = session.GetQueue(queueName);
                        Console.WriteLine("Queue set");
                        using (IMessageConsumer consumer = session.CreateConsumer(destination))
                        {
                            Console.WriteLine("Consumer created");
                            IMessage message=consumer.Receive(TimeSpan.FromSeconds(100));
                            Console.WriteLine("message recived");
                            if(message is ITextMessage textMessage)
                            {
                                Console.WriteLine("Received message with text: " + textMessage.Text);
                            }
                            else
                            {
                                Console.WriteLine("No message received within the time limit.");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
