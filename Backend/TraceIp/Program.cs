using StackExchange.Redis;
using TraceIp.Services.Implementation;
using TraceIp.Services.Interface;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

string hostRedis = "cache"; 
string portRedis = "6379";

ConfigurationOptions option = new ConfigurationOptions
{
    AbortOnConnectFail = false,
    EndPoints = { hostRedis, portRedis },
    Password = "eYVX7EwVmmxKPCDmwMtyKVge8oLd2t81",
    SyncTimeout = 500000 
};

ConnectionMultiplexer multiplexer = ConnectionMultiplexer.Connect(option);
builder.Services.AddSingleton<IConnectionMultiplexer>(multiplexer);
builder.Services.AddScoped<ITraceIpService, TraceIpService>();
builder.Services.AddScoped<IApiCountryService, ApiCountryService>();
builder.Services.AddScoped<ICurrencyService, CurrencyService>();
builder.Services.AddScoped<IRedisService, RedisService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();
app.UseCors(builder =>
{
    builder.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod();
});
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
