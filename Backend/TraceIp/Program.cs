using StackExchange.Redis;
using TraceIp.Services.Implementation;
using TraceIp.Services.Interface;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

ConnectionMultiplexer multiplexer = ConnectionMultiplexer.Connect("192.168.0.2:6379, password=eYVX7EwVmmxKPCDmwMtyKVge8oLd2t81, ConnectTimeout = 10000");
builder.Services.AddSingleton<IConnectionMultiplexer>(multiplexer);
builder.Services.AddScoped<ITraceIpService, TraceIpService>();
builder.Services.AddScoped<IApiCountryService, ApiCountryService>();
builder.Services.AddScoped<ICurrencyService, CurrencyService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
