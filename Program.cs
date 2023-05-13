using Bugsnag;
using XmpManager.Clients;
using XmpManager.Contracts;
using XmpManager.Middlewares;
using XmpManager.RabbitMQ;
using XmpManager.RabbitMQ.Connection;
using XmpManager.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Bugsnag
builder.Services.AddSingleton<IClient>(_ => new Client(builder.Configuration["Bugsnag:ApiKey"]));

// Clients
builder.Services.AddSingleton(_ => new EjabberdClient(builder.Configuration["Ejabberd:BaseUrl"], builder.Configuration["Ejabberd:Host"], builder.Configuration["Ejabberd:Service"], builder.Configuration["Ejabberd:Username"], builder.Configuration["Ejabberd:Password"]));

// RabbitMQ
var connection = new RabbitMQConnection(builder.Configuration["RabbitMQ:Host"], builder.Configuration["RabbitMQ:Username"], builder.Configuration["RabbitMQ:Password"]);
var registrations = new RabbitMQListener<User>(connection, "xmp.users.account", builder.Configuration["RabbitMQ:UserRegistrations:Exchange"], builder.Configuration["RabbitMQ:UserRegistrations:RegistrationsRoutingKey"]);
var unregistrations = new RabbitMQListener<User>(connection, "xmp.users.account", builder.Configuration["RabbitMQ:UserRegistrations:Exchange"], builder.Configuration["RabbitMQ:UserRegistrations:UnregistrationsKeysRoutingKey"]);

// Services 
builder.Services.AddTransient(i => new MucService(i.GetRequiredService<EjabberdClient>()));
builder.Services.AddTransient(i => new UserService(i.GetRequiredService<EjabberdClient>(), registrations, unregistrations));

var app = builder.Build();

// Singleton instantiations
app.Services.GetService<UserService>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<HttpExceptionMiddleware>();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
