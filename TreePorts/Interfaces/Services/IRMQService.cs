using RabbitMQ.Client;

namespace TreePorts.Interfaces.Services;
public interface IRMQService
{
    bool send(string queue, string message);
    IModel consume(string queue);

    void dispose();
}

