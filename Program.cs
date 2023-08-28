using ELK.Example;
using ELK.Example.Domain.Ports.Input;
using ELK.Example.Domain.Ports.Output;
using ELK.Example.Domain.Ports.UseCases;
using ELK.Example.ELK.Example.Adapter.Logs.Driven.Logs.ELK;
using ELK.Example.ELK.Example.Adapter.Logs.Facades;
using ELK.Example.Infrastructure.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IELKRegisterLog, ELKRegisterLog>();
builder.Services.AddSingleton<ILogFacade, LogFacade>();
builder.Services.AddSingleton<IWeatherForecastsInputPort, WeatherForecastsUseCase>();
builder.Services.AddSingleton<IWeatherForecastsOutputPort, ELKRegisterLog>();
builder.Services.AddSingleton<IELKRegisterLog, ELKRegisterLog>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseMiddleware<RequestResponseLoggingMiddleware>();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
