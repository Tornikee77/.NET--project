using _NET_PROJECT.Data;
using _NET_PROJECT.Endpoints;

var buiilder = WebApplication.CreateBuilder(args);
buiilder.Services.AddValidation();
buiilder.AddGameStoreDb();

var app = buiilder.Build();

app.MapGameEndpoints();

app.MigrateDb();

app.Run();
