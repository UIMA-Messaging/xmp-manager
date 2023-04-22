using XmpManager.Clients.Ejabberd;
using XmpManager.Contracts;
using XmpManager.RabbitMQ;
using XmpManager.RabbitMQ.Connection;
using XmpManager.Service.Users;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Clients
builder.Services.AddSingleton(_ => new EjabberdClient());

// Services 
builder.Services.AddTransient<IUserService>(i => new UserService(i.GetRequiredService<IRabbitMQListener<User>>(), i.GetRequiredService<EjabberdClient>()));

// RabbitMQ
//var rabbitMQConnection = new RabbitMQConnection(builder.Configuration["RabbitMQ:Uri"]);
var rabbitMQConnection = new RabbitMQConnection("localhost");
builder.Services.AddSingleton<IRabbitMQListener<User>>(i => new RabbitMQListener<User>(
    rabbitMQConnection, 
    "xmp.users.account", 
    builder.Configuration["RabbitMQ:UserRegistrations:Exchange"], 
    builder.Configuration["RabbitMQ:UserRegistrations:RoutingKey"]));

var app = builder.Build();

// Singleton instantiations
app.Services.GetService<IUserService>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
