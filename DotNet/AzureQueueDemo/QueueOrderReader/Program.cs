using Azure;
using Azure.Storage.Queues;
using Azure.Storage.Queues.Models;

Console.WriteLine("Starting to receive items from the Azure queue...");
string connectionString = "DefaultEndpointsProtocol=https;AccountName=devacademy2024;AccountKey=XXXXXXX;EndpointSuffix=core.windows.net";
string queueName = "jani-order-queue";

QueueClient queue = new QueueClient(connectionString, queueName);
queue.CreateIfNotExists();

while (true)
{
    Console.WriteLine("Press Enter to process the queue.");
    Console.ReadLine();

    Response<QueueMessage[]> message = queue.ReceiveMessages();
    foreach (QueueMessage msg in message.Value)
    {
        Console.WriteLine($"Webshop Order: {msg.MessageText}");
        queue.DeleteMessage(msg.MessageId, msg.PopReceipt);
    }
}
