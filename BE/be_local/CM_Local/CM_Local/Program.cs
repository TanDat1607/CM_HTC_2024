using CM_Local.AdsTwincat;
using CM_Local.ConnectDB;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// register MonitoringService as IHostedService
builder.Services.AddSingleton<FB_ADS>();
builder.Services.AddSingleton<FB_ADS1>();
builder.Services.AddSingleton<ConnectMongo>();
//CM
builder.Services.AddSingleton<BgADSCM>();
builder.Services.AddSingleton<IHostedService, BgADSCM>(
 serviceProvider => serviceProvider.GetService<BgADSCM>());
//PM
builder.Services.AddSingleton<BgADSPM>();
builder.Services.AddSingleton<IHostedService, BgADSPM>(
 serviceProvider => serviceProvider.GetService<BgADSPM>());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
