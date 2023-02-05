using Azure.Data.Tables;

namespace dotnetwithazuretable.Api;

public class TableClientProvider
{
    public static TableClient GetTableClient()
    {
        // Environment variables
        LoadLocalEnvironmentVariablesIfExists();
        var connectionString = Environment.GetEnvironmentVariable("COSMOS_CONNECTION_STRING");
        var tableName = Environment.GetEnvironmentVariable("COSMOS_TABLENAME");

        // New instance of TableClient class referencing the server-side table
        var tableServiceClient = new TableServiceClient(connectionString);
        TableClient tableClient = tableServiceClient.GetTableClient(
            tableName: tableName
        );

        return tableClient;
    }

    private static void LoadLocalEnvironmentVariablesIfExists()
    {
        var root = Directory.GetCurrentDirectory();
        var dotenv = Path.Combine(root, ".env");
        DotEnv.Load(dotenv);
    }
}
