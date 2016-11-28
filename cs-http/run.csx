 #load "./MatrixMultiply.csx"

using System.Net;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.ApplicationInsights.Extensibility;

private static TelemetryClient telemetry = new TelemetryClient();
private static string key = TelemetryConfiguration.Active.InstrumentationKey = System.Environment.GetEnvironmentVariable("APPINSIGHTS_INSTRUMENTATIONKEY", EnvironmentVariableTarget.Process);

public static async Task<HttpResponseMessage> Run(HttpRequestMessage req, TraceWriter log)
{
    log.Info($"C# HTTP trigger function processed a request. RequestUri={req.RequestUri}");
    
    telemetry.TrackEvent("Function Started");
    // Establish an operation context and associated telemetry item:
    using (var operation = telemetry.StartOperation<RequestTelemetry>("CS-MatrixMultiply"))
    {
        // parse query parameter
        string val = req.GetQueryNameValuePairs()
            .FirstOrDefault(q => string.Compare(q.Key, "size", true) == 0)
            .Value;

        // Set size to query string or body data
        int size = Int32.Parse(val ?? "5");
        int seed = 124;
        int valueMin = 0;
        int valueMax = 101;

        telemetry.TrackEvent("MatrixMultiplyStarted", null, new Dictionary<string, double>{{"size", size}});

        int[][] matrix = CreateRandomMatrix(size, seed, valueMin, valueMax);
        seed = 2 * seed;
        int[][] matrix2 = CreateRandomMatrix(size, seed, valueMin, valueMax);
        int[][] result = MultiplyMatrix(matrix, matrix2);

        // Set properties of containing telemetry item - for example:
        operation.Telemetry.ResponseCode = "200";

        //optional
        telemetry.StopOperation(operation);

    } // When operation is disposed, telemetry item is sent.
    return req.CreateResponse(HttpStatusCode.OK);
}

