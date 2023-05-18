using GraphQL.Server;
using GraphQL.Server.Ui.Playground;
using Howest.MagicCards.DAL.Models;
using Howest.MagicCards.DAL.Repositories;
using Howest.MagicCards.GraphQL.GraphQLTypes;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager config = builder.Configuration;

builder.Services.AddDbContext<MtgV1Context>(
    options => options.UseSqlServer(config.GetConnectionString("CardsDB"))
);
builder.Services.AddScoped<ICardRepository, SqlCardRepository>();

var app = builder.Build();

app.Run();
