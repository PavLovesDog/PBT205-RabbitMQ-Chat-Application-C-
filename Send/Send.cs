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
                                 durable: true,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);
                                       
            
            // set up to allow arbitrary messages to be sent from the command line
            string message = "Hello Charles!"; // Message here
            var body = Encoding.UTF8.GetBytes(message);

            channel.BasicPublish(exchange: "", 
                                 routingKey: "hello", 
                                 basicProperties: null, 
                                 body: body);
            Console.WriteLine(" [x] Sent {0}", message);
        }

        Console.WriteLine(" Press [enter] to exit.");
        Console.ReadLine();
    }
}