using System.Collections.Generic;
using System.Net;
using Azure.Storage.Blobs;
using System.Text;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace AzureFunctionAppDemo
{
    public class Function1
    {
        private readonly ILogger _logger;

        public Function1(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<Function1>();
        }

        [Function("Function1")]
        public HttpResponseData Run([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequestData req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            string connectionString = "DefaultEndpointsProtocol=https;AccountName=devacademy2024;AccountKey=XXXXXXX;EndpointSuffix=core.windows.net";
            string containerName = "jani-demo";
            string blobName = "HelloFromAzureFunction.txt";

            string message = "Hello from Azure Function!";
            MemoryStream stream = new(Encoding.UTF8.GetBytes(message));
            BlobServiceClient blobServiceClient = new(connectionString);
            BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(containerName);
            BlobClient blobClient = containerClient.GetBlobClient(blobName);
            var result = blobClient.UploadAsync(stream, true).Result;

            var response = req.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("Content-Type", "text/plain; charset=utf-8");

            response.WriteString("Succesfully uploaded a blob to Azure blob storage.");

            return response;
        }
    }
}
