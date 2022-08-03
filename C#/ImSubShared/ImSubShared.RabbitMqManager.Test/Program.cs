using ImSubShared.RabbitMqManager.QueueConfigurations;
using ImSubShared.RabbitMqManagers.QueueManager.QueueManagers;

var configuration = new BasicQueueConfiguration
{
    Username = "guest",
    Password = "guest",
    Hostname = "localhost",
    QueueName = "test",
    Exchange = "test_exch",
    RoutingKey = "test_route"
};
var sender = new BasicSenderQueueManager<string>(configuration);
var receiver = new BasicReceiverQueueManager<string>(configuration);

while (true)
{
    try
    {
        try
        {
            receiver.CheckState();
        }
        catch (Exception) 
        {
            Console.WriteLine("Restarting receiver ...");
        }
        sender.Send("Hello world!");
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
    }

    var input = Console.ReadLine();
    if (input == "EXIT")
        break;
}