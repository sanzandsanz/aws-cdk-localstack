using System.Text.Json;
using Amazon.Lambda.Core;
using Amazon.Lambda.SQSEvents;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace WeatherForecast.Service;

public class Function
{
    public async Task FunctionHandler(SQSEvent evnt, ILambdaContext context)
    {
        foreach (var message in evnt.Records)
        {
            await ProcessMessageAsync(message, context);
        }
    }

    private async Task ProcessMessageAsync(SQSEvent.SQSMessage message, ILambdaContext context)
    {
        var weatherForecastAddedEvent = JsonSerializer.Deserialize<WeatherForecastAddedEvent>(message.Body);
        context.Logger.LogInformation($"Processed message for City {weatherForecastAddedEvent.City} on {weatherForecastAddedEvent.DateTime} with Temperature {weatherForecastAddedEvent.TemperatureC}");
        await Task.CompletedTask;
    }
}