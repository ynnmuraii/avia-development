using Aspire.Hosting;

var builder = DistributedApplication.CreateBuilder(args);

// База данных
var mysql = builder.AddMySql("mysql")
    .AddDatabase("airline-db");

// RabbitMQ с management плагином
var rabbitmq = builder.AddRabbitMQ("messaging")
    .WithManagementPlugin();

// API зависит от MySQL и RabbitMQ
var api = builder.AddProject<Projects.Airline_API>("airline-api")
    .WithReference(mysql)
    .WithReference(rabbitmq)
    .WaitFor(mysql)
    .WaitFor(rabbitmq);

// Генератор зависит только от RabbitMQ, запускается после API
builder.AddProject<Projects.Airline_Generator>("airline-generator")
    .WithReference(rabbitmq)
    .WaitFor(rabbitmq)
    .WaitFor(api);

builder.Build().Run();