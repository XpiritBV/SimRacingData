// See https://aka.ms/new-console-template for more information
using Classes;
using Microsoft.Extensions.Configuration;
using System.Text.Json;

Console.WriteLine("Hello, World!");


var configurationBuilder = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .Build();


var folderPath = configurationBuilder.GetSection("RaceResultsFolder").Value;
if(folderPath == null)
{
    throw new InvalidOperationException($"{nameof(folderPath)} is null.");
}

var directory = new DirectoryInfo(folderPath);

DateTime oldestTime = DateTime.MaxValue;
FileInfo? oldestFile = null;

var files = directory.EnumerateFiles();
foreach (var file in files)
{
    if (file.LastWriteTime < oldestTime)
    {
        oldestTime = file.LastWriteTime;
        oldestFile = file;
    }
}

if(oldestFile == null)
{
    throw new InvalidOperationException($"{nameof(oldestFile)} is null.");
}

var contentString = File.ReadAllText(oldestFile.FullName);
if(contentString == null)
{
    throw new InvalidOperationException($"{nameof(contentString)} is null.");
}
var element = JsonSerializer.Deserialize<RootObject>(contentString);



