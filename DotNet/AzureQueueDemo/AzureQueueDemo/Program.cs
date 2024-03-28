using Azure.Storage.Queues;
using AzureQueueDemo;
using System.Text.Json;

Console.WriteLine("Starting to send an item to the Azure queue...");
string connectionString = "DefaultEndpointsProtocol=https;AccountName=devacademy2024;AccountKey=XXXXXXX;EndpointSuffix=core.windows.net";
string queueName = "jani-order-queue";

QueueClient queue = new QueueClient(connectionString, queueName);
queue.CreateIfNotExists();

int orderCounter = 1;
while (true)
{
    Console.WriteLine("Press Enter to send a queue item.");
    Console.ReadLine();

    WebShopOrder order = new()
    {
        OrderId = orderCounter,
        ProductName = "Surface Laptop 4",
        Quantity = Random.Shared.Next(1, 10),
        OrderPrice = 1500
    };
    string json = JsonSerializer.Serialize(order);

    queue.SendMessage(json);
    Console.WriteLine("Message sent to the queue!");

    orderCounter++;
}
