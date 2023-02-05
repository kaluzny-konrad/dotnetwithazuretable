using Azure.Data.Tables;
using dotnetwithazuretable.Console;

// Environment variables
var root = Directory.GetCurrentDirectory();
var dotenv = Path.Combine(root, ".env");
DotEnv.Load(dotenv);

// New instance of the TableClient class
var connectionString = Environment.GetEnvironmentVariable("COSMOS_CONNECTION_STRING");
var tableServiceClient = new TableServiceClient(connectionString);

// New instance of TableClient class referencing the server-side table
var tableName = Environment.GetEnvironmentVariable("COSMOS_TABLENAME");
var tableClient = tableServiceClient.GetTableClient(
    tableName: tableName
);
await tableClient.CreateIfNotExistsAsync();

// Create new item using composite key constructor
var sharedPartitionKey = "gear-surf-surfboards";
var productToAdd1 = new Product()
{
    RowKey = "68719518388",
    PartitionKey = sharedPartitionKey,
    Name = "Ocean Surfboard",
    Quantity = 8,
    Sale = true,
};

// Add new item to server-side table
Console.WriteLine($"1. Adding single product: {productToAdd1.Name}");
try
{
    await tableClient.AddEntityAsync(productToAdd1);
}
catch (Azure.RequestFailedException)
{
    Console.WriteLine($" - Product already exists: {productToAdd1.Name}");
}


// Read a single item from container
var productAdded1 = await tableClient.GetEntityAsync<Product>(
    rowKey: productToAdd1.RowKey,
    partitionKey: productToAdd1.PartitionKey
);
Console.WriteLine($"2. Single product added: {productAdded1.Value.Name}");

// Add new item to server-side table
var productToAdd2 = new Product()
{
    RowKey = "68719518390",
    PartitionKey = sharedPartitionKey,
    Name = "Sand Surfboard",
    Quantity = 5,
    Sale = false
};

Console.WriteLine($"3. Adding second product: {productToAdd2.Name}");
try
{
    await tableClient.AddEntityAsync(productToAdd2);
}
catch (Azure.RequestFailedException)
{
    Console.WriteLine($" - Product already exists: {productToAdd2.Name}");
}

// Read multiple items from container
var productsAdded = tableClient.Query<Product>(x => x.PartitionKey == sharedPartitionKey);

Console.WriteLine("4. Multiple products added:");
foreach (var productAdded in productsAdded)
    Console.WriteLine(productAdded.Name);