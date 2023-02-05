using Azure.Data.Tables;

namespace dotnetwithazuretable.Api;

public class TableClientProvider
{
    public static TableClient GetTableClient()
    {
        // Environment variables if INT/PROD
        var tableName = System.Configuration.ConfigurationManager.AppSettings["COSMOS_TABLENAME"];
        var connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["COSMOS_CONNECTION_STRING"].ConnectionString;

        // Environment variables if App.config not exists
        if (tableName == null || connectionString == null)  LoadLocalEnvironmentVariablesIfExists();
        if (tableName == null)
            tableName = Environment.GetEnvironmentVariable("COSMOS_TABLENAME");
        if (connectionString == null)
            connectionString = Environment.GetEnvironmentVariable("COSMOS_CONNECTION_STRING");

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
        EnvironmentVariableManager.Load(dotenv);
    }
}
