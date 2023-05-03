using XmpManager.Clients;
using XmpManager.Contracts;
using XmpManager.RabbitMQ;
using XmpManager.RabbitMQ.Connection;
using XmpManager.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Clients
builder.Services.AddSingleton(_ => new EjabberdClient());

// Services 
builder.Services.AddTransient(i => new UserService(i.GetRequiredService<IRabbitMQListener<User>>(), i.GetRequiredService<EjabberdClient>()));

// RabbitMQ
var rabbitMqConnection = new RabbitMQConnection(builder.Configuration["RabbitMQ:Uri"]);
builder.Services.AddSingleton<IRabbitMQListener<User>>(i => new RabbitMQListener<User>(rabbitMqConnection, "xmp.users.account", builder.Configuration["RabbitMQ:UserRegistrations:Exchange"], builder.Configuration["RabbitMQ:UserRegistrations:RoutingKey"]));

var app = builder.Build();

// Singleton instantiations
app.Services.GetService<UserService>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
