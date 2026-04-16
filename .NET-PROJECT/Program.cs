using _NET_PROJECT.Data;
using _NET_PROJECT.Endpoints;

var buiilder = WebApplication.CreateBuilder(args);
buiilder.Services.AddValidation();
var connString = " Data Source = GameStore.db";
buiilder.Services.AddSqlite<GameStoreContext>(
    connString,
    optionsAction: options =>
        options.UseSeeding(
            (context, _) =>
            {
                if (!context.Set<Genre>().Any())
                {
                    context
                        .Set<Genre>()
                        .AddRange(
                            new Genre { Name = "Action" },
                            new Genre { Name = "Adventure" },
                            new Genre { Name = "RPG" },
                            new Genre { Name = "Strategy" },
                            new Genre { Name = "Sports" }
                        );
                    context.SaveChanges();
                }
            }
        )
);
var app = buiilder.Build();

app.MapGameEndpoints();

app.MigrateDb();

app.Run();
