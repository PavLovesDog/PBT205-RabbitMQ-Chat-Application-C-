using System;
using RabbitMQ.Client;
using System.Text;

class Send
{
    public static void Main()
    {
        // Create the connection to server
        var factory = new ConnectionFactory() {HostName = "localhost"}; //connect to a RabbitMQ node on the local machine i.e "localhost" If we wanted to connect to a node on a different machine we specify its hostname or IP address here.
        using (var connection = factory.CreateConnection())
        using (var channel = connection.CreateModel())
        {
            // Create channel for API
            channel.QueueDeclare(queue: "hello",
                                        durable: false,
                                        exclusive: false,
                                        autoDelete: false,
                                        arguments: null);
                                       
            
            string message = "...Hello Charlie!"; // Message to "send"/ Add to queue
            var body = Encoding.UTF8.GetBytes(message);

            // declare a queue to send to, then publish a message to the queue
            channel.BasicPublish(exchange: "",
                                            routingKey: "hello",
                                            basicProperties: null,
                                            body: body);
            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();
        }
    }
}