using _NET_PROJECT.Dtos;

var buiilder = WebApplication.CreateBuilder(args);  
var app = buiilder.Build();


List<GameDto> games = [
    new (
        1,
        "mlbb",
        "FARMING",
        19.91M,
        new DateOnly(1992,7,15)),
          new (
        2,
        "Mortal Kombat",
        "Fighting",
        19.92M,
        new DateOnly(1992,7,15)),
          new (
        3,
        "FiFA",
        "Football",
        19.93M,
        new DateOnly(1992,7,15)),
];

// GET /Games
app.MapGet("/games", () => games);

// GET /Games/{id}
app.MapGet("/games/{id}", (int id) => games.Find(game => game.Id == id));
app.Run();