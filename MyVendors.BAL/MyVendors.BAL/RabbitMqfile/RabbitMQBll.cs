//using RabbitMQ.Client;
//using RabbitMQ.Client.Events;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace MyVendors.BAL.RabbitMqfile
//{
//    public class RabbitMQBll
//    {
//        public IConnection GetConnection()
//        {

//            ConnectionFactory factory = new ConnectionFactory();
//            factory.UserName = "guest";
//            factory.Password = "guest";
//            factory.Port = 5672;
//            factory.HostName = "localhost";
//            factory.VirtualHost = "/";

//            return factory.CreateConnection();

//        }


//        public bool send(IConnection con, string Messagestring, string messagequeue)
//        {
//            try
//            {
//                IModel channel = con.CreateModel();
//                channel.ExchangeDeclare("messageexchange", ExchangeType.Direct);
//                channel.QueueDeclare(messagequeue, true, false, false, null);
//                channel.QueueBind(messagequeue, "messageexchange", messagequeue, null);
//                var message = Encoding.UTF8.GetBytes(Messagestring);
//                channel.BasicPublish("messageexchange", messagequeue, null, message);
//            }
//            catch (Exception ex)
//            {

//                throw;
//            }

//            return true;
//        }

//        public string receive(IConnection con, string notiq)
//        {
//            try
//            {
//                string queue = notiq;
//                IModel channel = con.CreateModel();
//                channel.QueueDeclare(queue: queue, durable: true, exclusive: false, autoDelete: false, arguments: null);
//                var notify = new EventingBasicConsumer(channel);
//                BasicGetResult result = channel.BasicGet(queue: queue, noAck: true);
//                if (result != null)
//                {
//                    return Encoding.UTF8.GetString(result.Body);
//                }
//                else
//                {
//                    return null;
//                }

//            }
//            catch (Exception ex)
//            {

//                return null;
//            }
//        }


//        public string deleteq(IConnection con, string notiq)
//        {
//            try
//            {
//                string queue = notiq;
//                IModel channel = con.CreateModel();
//                channel.QueueDelete(queue: queue);
//                var notify = new EventingBasicConsumer(channel);
//                BasicGetResult result = channel.BasicGet(queue: queue, noAck: true);
//                if (result != null)
//                {
//                    return Encoding.UTF8.GetString(result.Body);
//                }
//                else
//                {
//                    return null;
//                }

//            }
//            catch (Exception ex)
//            {

//                return null;
//            }
//        }

//    }
//}
