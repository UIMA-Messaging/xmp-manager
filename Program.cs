using XmpManager.EventBus.Connection;
using XmpManager.Service.Users;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// RabbitMQ
var rabbitMQConnection = new RabbitMQConnction("localhost");

// Services 
builder.Services.AddTransient<IUserService>(i => new UserService());

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
