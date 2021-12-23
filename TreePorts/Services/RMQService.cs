using RabbitMQ.Client;
using TreePorts.Infrastructure.services;
using System;
using System.Text;

namespace TreePorts.Presentation;
public class RMQService : IRMQService
{
    private string hostname = "http://localhost:5672"; // change to live hosting 

    private IConnection connection; 

    private IConnection getConnection() 
    {
        if (connection == null) {
            var factory = new ConnectionFactory() { HostName = hostname };
            connection = factory.CreateConnection();
        }
        return connection;  
    }

    public bool send(string queue ,string message) 
    {

        try
        {
            //var factory = new ConnectionFactory() { HostName = hostname };
            //using (var connection = factory.CreateConnection())
            var connection = getConnection();
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: queue,
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: "",
                                     routingKey: queue,
                                     basicProperties: null,
                                     body: body);
                channel.Close();
                return true;
            }
        }
        catch (Exception e) 
        {
            return false;
        }

        
        
    }


    public IModel consume(string queue) 
    {
        try
        {
            /*if(connection  == null)
            {
                var factory = new ConnectionFactory() { HostName = hostname };
                connection = factory.CreateConnection();
            }*/

            var connection = getConnection();
            var channel = connection.CreateModel();
            channel.QueueDeclare(queue: queue,
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);
            return channel;
            
        }
        catch (Exception e)
        {
            return null;
        }

    }

    public void dispose() 
    {
        if(connection != null)
            connection.Dispose();
    }

}

