using Aspire.Hosting;

var builder = DistributedApplication.CreateBuilder(args);

// База данных
var mysql = builder.AddMySql("mysql")
    .AddDatabase("airline-db");

var api = builder.AddProject<Projects.Airline_API>("airline-api")
    .WithReference(mysql)
    .WaitFor(mysql); 

builder.Build().Run();