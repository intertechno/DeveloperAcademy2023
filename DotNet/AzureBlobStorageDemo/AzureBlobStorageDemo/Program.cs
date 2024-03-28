using Azure.Storage.Blobs;
using AzureBlobStorageDemo;
using System.Text;
using System.Text.Json;

string connectionString = "DefaultEndpointsProtocol=https;AccountName=devacademy2024;AccountKey=XXXXXX;EndpointSuffix=core.windows.net";
string containerName = "jani-demo";
string blobName = "MyFirstBlob.txt";

Customer customer = new()
{
    Id = 1,
    Name = "Jani",
    Country = "Finland"
};

Console.WriteLine("Starting to upload blob to Azure...");
string json = JsonSerializer.Serialize(customer);
MemoryStream stream = new(Encoding.UTF8.GetBytes(json));

BlobServiceClient blobServiceClient = new(connectionString);
BlobContainerClient containerClient = blobServiceClient.CreateBlobContainer(containerName);
BlobClient blobClient = containerClient.GetBlobClient(blobName);

await blobClient.UploadAsync(stream, true);
Console.WriteLine("Completed uploading blob to Azure.");
