using CM_Server.BgServices;
using CM_Server.ConnectDB;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//
builder.Services.AddSingleton<ConnectMongo>();
//
builder.Services.AddSingleton<BgSyncDataCm>();
builder.Services.AddSingleton<IHostedService, BgSyncDataCm>(
serviceProvider => serviceProvider.GetService<BgSyncDataCm>());
//
builder.Services.AddSingleton<BgSyncDataPM>();
builder.Services.AddSingleton<IHostedService, BgSyncDataPM>(
serviceProvider => serviceProvider.GetService<BgSyncDataPM>());

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
