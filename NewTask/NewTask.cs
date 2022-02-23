using System;
using RabbitMQ.Client;
using System.Text;

class NewTask
{
    public static void Main()
    {
        // Create the connection to server
        var factory = new ConnectionFactory() {HostName = "localhost"}; //connect to a RabbitMQ node on the local machine i.e "localhost" If we wanted to connect to a node on a different machine we specify its hostname or IP address here.
        using (var connection = factory.CreateConnection())
        using (var channel = connection.CreateModel())
        {
            // Create channel for API
            channel.QueueDeclare(queue: "task_queue",
                                durable: false,
                                exclusive: false,
                                autoDelete: false,
                                arguments: null);

            // Get Amount of messages to be sent
            Console.WriteLine(" Enter number of messages to send > ");
            string numOfMessages = Console.ReadLine();
            int tMessages = 1;

            //HERES SOME BULLSHIT ---------------------------------
            if (numOfMessages == "0") tMessages = 0;
            if (numOfMessages == "1") tMessages = 1;
            if (numOfMessages == "2") tMessages = 2;
            if (numOfMessages == "3") tMessages = 3;
            if (numOfMessages == "4") tMessages = 4;
            if (numOfMessages == "5") tMessages = 5;
            if (numOfMessages == "6") tMessages = 6;
            if (numOfMessages == "7") tMessages = 7;
            if (numOfMessages == "8") tMessages = 8;
            if (numOfMessages == "9") tMessages = 9;
            // ---------------------------------------------------

            // loop through as many messages to be sent
            for (int i = 1; i <= tMessages; i++)
            {
            // set up to allow arbitrary messages to be sent from the command line
            Console.WriteLine(" Enter Message " + i + ": ");
            var messageString = Console.ReadLine();
            var message = GetMessage(messageString);
            var body = Encoding.UTF8.GetBytes(message);

            var properties = channel.CreateBasicProperties();
            properties.Persistent = true;

            // declare a queue to send to, then publish a message to the queue
            channel.BasicPublish(exchange: "",
                                routingKey: "task_queue",
                                basicProperties: properties,
                                body: body);
            }
            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();
        }
    }

    private static string GetMessage(string args)
    {
        return ((args.Length > 0) ? string.Join(" ", args) : args);
    }
}