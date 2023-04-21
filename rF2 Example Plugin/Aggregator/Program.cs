// See https://aka.ms/new-console-template for more information
using Azure.Messaging.EventHubs;
using Azure.Messaging.EventHubs.Producer;
using Microsoft.Extensions.Configuration;
using System.Text;

Console.WriteLine("Hello, World!");
// Connection string to the Event Hub namespace
string eventHubConnectionString = "Endpoint=sb://racing-telemetry-events.servicebus.windows.net/;SharedAccessKeyName=Token;SharedAccessKey=kaB5fe+iOkWnOoCGrdE+V5hivs0QtecvK+AEhMQ9y60=;EntityPath=telemetry";
// Name of the Event Hub
string eventHubName = "telemetry";
// Create an EventHubProducerClient
var producerClient = new EventHubProducerClient(eventHubConnectionString, eventHubName);
var configurationBuilder = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .Build();


var folderPath = configurationBuilder.GetSection("RaceResultsFolder").Value;
if (folderPath == null)
{
    throw new InvalidOperationException($"{nameof(folderPath)} is null.");
}
var processedFilesFolderPath = configurationBuilder.GetSection("ProcessedFilesFolder").Value;
if (processedFilesFolderPath == null)
{
    throw new InvalidOperationException($"{nameof(processedFilesFolderPath)} is null.");
}

var directory = new DirectoryInfo(folderPath);

while (true)
{
    var files = directory.EnumerateFiles();
    foreach (var file in files.OrderBy(f => f.LastWriteTime))
    {
        var contentString = File.ReadAllText(file.FullName);
        // Create an EventData object
        EventData eventData = new EventData(Encoding.UTF8.GetBytes(contentString));
        var events = new List<EventData> { eventData };
        await producerClient.SendAsync(events);
        File.Move(file.FullName, $"{processedFilesFolderPath}/{DateTime.Now:yyyyMMddHHmmssfff}-{file.Name}");
    }
    Thread.Sleep(1000);
}
