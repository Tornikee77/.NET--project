using _NET_PROJECT.Data;
using _NET_PROJECT.Dtos;
using _NET_PROJECT.Models;

namespace _NET_PROJECT.Endpoints;

public static class Gameendpoints
{
    const string GetGameEndpointName = "GetGame";

    private static readonly List<GameDto> games =
    [
        new(1, "mlbb", "FARMING", 19.91M, new DateOnly(1992, 7, 15)),
        new(2, "Mortal Kombat", "Fighting", 19.92M, new DateOnly(1992, 7, 15)),
        new(3, "FiFA", "Football", 19.93M, new DateOnly(1992, 7, 15)),
    ];

    public static void MapGameEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/games");
        // GET /Games
        group.MapGet("/", () => games);

        // GET /Games/{id}
        group
            .MapGet("/{id}", (int id) => games.Find(game => game.Id == id))
            .WithName(GetGameEndpointName);

        // POST /Games
        group.MapPost(
            "/",
            (CreateGameDto newGame, GameStoreContext dbContext) =>
            {
                Game game = new()
                {
                    Name = newGame.Name,

                    GenreId = newGame.GenreId,

                    Price = newGame.Price,

                    ReleaseDate = newGame.ReleaseDate,
                };

                dbContext.Games.Add(game);

                dbContext.SaveChanges();

                GameDetailsDto gameDto = new(
                    game.Id,
                    game.Name,
                    game.GenreId,
                    game.Price,
                    game.ReleaseDate
                );

                return Results.CreatedAtRoute(
                    GetGameEndpointName,
                    new { id = gameDto.Id },
                    gameDto
                );
            }
        );

        // PUT /Games/{id}
        group.MapPut(
            "/{id}",
            (int id, UpdateGameDto updateGame) =>
            {
                var index = games.FindIndex(game => game.Id == id);

                games[index] = new GameDto(
                    id,
                    updateGame.Name,
                    updateGame.Genre,
                    updateGame.Price,
                    updateGame.ReleaseDate
                );
                return Results.NoContent();
            }
        );

        // DELETE /Games/{id}
        group.MapDelete(
            "/{id}",
            (int id) =>
            {
                var index = games.FindIndex(game => game.Id == id);

                _ = games.RemoveAll(game => game.Id == id);
                return Results.NoContent();
            }
        );
    }
}
