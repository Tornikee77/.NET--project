using _NET_PROJECT.Dtos;

const string GetGameEndpointName = "GetGame";


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
app.MapGet("/games/{id}", (int id) => games.Find(game => game.Id == id)).WithName(GetGameEndpointName);

// POST /Games
app.MapPost("/games",(CreateGameDto newGame) =>
{
    GameDto game = new (
        games.Count + 1,
        newGame.Name,
        newGame.Genre,
        newGame.Price,
        newGame.ReleaseDate
    );
    games.Add(game);
    return Results.CreatedAtRoute(GetGameEndpointName, new { id = game.Id }, game);
});

// PUT /Games/{id}
app.MapPut("/games/{id}",(int id,UpdateGameDto updateGame) =>
{
   var index = games.FindIndex(game => game.Id==id);

    games[index] = new GameDto(
        id,
        updateGame.Name,
        updateGame.Genre,
        updateGame.Price,
        updateGame.ReleaseDate
    );
    return Results.NoContent();
});
app.Run();