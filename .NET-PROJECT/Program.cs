using _NET_PROJECT.Data;
using _NET_PROJECT.Endpoints;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddValidation();
builder.AddGameStoreDb();

var app = builder.Build();

app.MapGameEndpoints();

app.MigrateDb();

app.Run();
