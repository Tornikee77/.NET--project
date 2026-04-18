using _NET_PROJECT.Data;
using _NET_PROJECT.Endpoints;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddValidation();
builder.AddGameStoreDb();

var app = builder.Build();

app.MapGameEndpoints();

app.MigrateDb();
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<GameStoreContext>();

    if (!db.Genres.Any())
    {
        var genres = new List<Genre>();

        for (int i = 1; i <= 100; i++)
        {
            genres.Add(new Genre { Name = $"Genre {i}" });
        }

        db.Genres.AddRange(genres);
        db.SaveChanges();
    }
}

app.Run();
