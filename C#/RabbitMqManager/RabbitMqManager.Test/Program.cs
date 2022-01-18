using QueueManager.QueueConfigurations;
using QueueManager.QueueManagers;

try
{
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

    sender.Send("Hello world!");
    receiver.Consume();

    Console.ReadKey();
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}
