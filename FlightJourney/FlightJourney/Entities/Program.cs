using FlightJourney.Repositories;
using FlightJourney.Repositories.Interfaces;
using FlightJourney.Services;
using FlightJourney.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IFlightRepository, FlightRepository>();
builder.Services.AddScoped<IJourneyService, JourneyService>();
builder.Services.AddCors();


builder.Services.AddHttpClient("NewshoreAPI", httpClient =>
{
    httpClient.BaseAddress = new Uri("https://recruiting-api.newshore.es/api/flights/");
});

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

app.UsePathBase(new PathString("/api"));

// global cors policy
app.UseCors(x => x
    .AllowAnyMethod()
    .AllowAnyHeader()
    .SetIsOriginAllowed(origin => true)
    .WithOrigins("http://localhost:4200")
    .AllowCredentials()); 

app.Run();
