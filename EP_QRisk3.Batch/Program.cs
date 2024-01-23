using ep_models;
using Newtonsoft.Json;

if (args.Length != 1)
{
    Console.WriteLine("Usage: EP_QRisk3.Batch.exe <pathToJSONFiles>");
    return;
}
if (!Directory.Exists(args[0]))
{
    Console.WriteLine("Cannot find directory.");
    return;
}
Console.WriteLine("Batch processing...");

var serializer = new JsonSerializer();
var service = new ep_service.PredictionService();

EPInputModel epInputModel = new EPInputModel();

foreach (var file in Directory.GetFiles(args[0]))
{
    if (!file.Contains(".input."))
    {
        Console.WriteLine("Skipping File: " + file);
        continue;
    }

    Console.WriteLine("Processing file: " + file);
    using (StreamReader sr = File.OpenText(file))
    {
        using (JsonTextReader reader = new JsonTextReader(sr))
        {
            epInputModel = serializer.Deserialize<EPInputModel>(reader);
        }
    }

    // call the ep_service
    var serviceResult = service.GetScore(epInputModel);

    // write the results as JSON
    var outputFile = file.Replace("input", "output");    
    var outputString = JsonConvert.SerializeObject(serviceResult, Formatting.Indented, new Newtonsoft.Json.Converters.StringEnumConverter());    
    File.WriteAllText(outputFile, outputString);
}   