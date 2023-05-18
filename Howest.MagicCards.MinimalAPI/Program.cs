using Howest.MagicCards.DAL.Models;
using Howest.MagicCards.DAL.Repositories;
using Howest.MagicCards.MinimalAPI;
using Howest.MagicCards.MinimalAPI.Mappings;

var (builder, services, config) = WebApplication.CreateBuilder(args);

const string commonPrefix = "/api";

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<MongoDBSettings>(builder.Configuration.GetSection("MongoDB"));
builder.Services.AddSingleton<MongoDBDeckRepository>();

WebApplication app = builder.Build();

string urlPrefix = config.GetSection("ApiPrefix").Value ?? commonPrefix;

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapDeckEndpoints(urlPrefix);

app.Run();
