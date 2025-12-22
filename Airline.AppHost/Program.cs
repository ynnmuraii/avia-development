using Aspire.Hosting;

var builder = DistributedApplication.CreateBuilder(args);


var mysql = builder.AddMySql("mysql")
    .AddDatabase("airline-db");


var rabbitmq = builder.AddRabbitMQ("messaging")
    .WithManagementPlugin();


var api = builder.AddProject<Projects.Airline_API>("airline-api")
    .WithReference(mysql)
    .WithReference(rabbitmq)
    .WaitFor(mysql)
    .WaitFor(rabbitmq);


builder.AddProject<Projects.Airline_Generator>("airline-generator")
    .WithReference(rabbitmq)
    .WaitFor(rabbitmq)
    .WaitFor(api);

builder.Build().Run();