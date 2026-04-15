using _NET_PROJECT.Data;
using _NET_PROJECT.Endpoints;

var buiilder = WebApplication.CreateBuilder(args);
buiilder.Services.AddValidation();
var connString = " Data Source = GameStore.db";
buiilder.Services.AddSqlite<GameStoreContext>(connString);
var app = buiilder.Build();

app.MapGameEndpoints();
app.Run();
