using XmpManager.Contracts;
using XmpManager.EventBus.RabbitMQ;
using XmpManager.EventBus.RabbitMQ.Connection;
using XmpManager.EventBus.Subscriptions;
using XmpManager.Service.Users;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Services 
builder.Services.AddTransient<IUserService>(i => new UserService());

// RabbitMQ
var rabbitMQConnection = new RabbitMQConnection("localhost").TryConnect();

// RabbitMQ - Subscriptions
builder.Services.AddSingleton(i => new UserRegistrationsListener(i.GetRequiredService<IUserService>(), new RabbitMQListener<User>(rabbitMQConnection, "registrations", "registrations", "users.new")));

var app = builder.Build();

// Singleton instantiations
app.Services.GetService<UserRegistrationsListener>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
