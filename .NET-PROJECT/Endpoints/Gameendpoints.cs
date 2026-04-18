using _NET_PROJECT.Data;
using _NET_PROJECT.Dtos;
using _NET_PROJECT.Models;
using Microsoft.EntityFrameworkCore;

namespace _NET_PROJECT.Endpoints;

public static class Gameendpoints
{
    const string GetGameEndpointName = "GetGame";

    private static readonly List<GameDto> games =
    [
        new GameDto(1, "mlbb", 1, 19.91M, new DateOnly(1992, 7, 15)),
        new GameDto(2, "Mortal Kombat", 2, 19.92M, new DateOnly(1992, 7, 15)),
        new GameDto(3, "FiFA", 3, 19.93M, new DateOnly(1992, 7, 15)),
    ];

    public static void MapGameEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/games");
        // GET /Games
        group.MapGet(
            "/",
            async (GameStoreContext db) =>
            {
                return await db.Games.ToListAsync();
            }
        );

        // GET /Games/{id}
        group
            .MapGet(
                "/{id}",
                async (int id, GameStoreContext db) =>
                {
                    var game = await db.Games.FindAsync(id);

                    return game is null
                        ? Results.NotFound()
                        : Results.Ok(
                            new GameDetailsDto(
                                game.Id,
                                game.Name,
                                game.GenreId,
                                game.Price,
                                game.ReleaseDate
                            )
                        );
                }
            )
            .WithName(GetGameEndpointName);
        // POST /Games
        group.MapPost(
            "/",
            async (CreateGameDto newGame, GameStoreContext dbContext) =>
            {
                Game game = new()
                {
                    Name = newGame.Name,

                    GenreId = newGame.GenreId,

                    Price = newGame.Price,

                    ReleaseDate = newGame.ReleaseDate,
                };

                dbContext.Games.Add(game);

                await dbContext.SaveChangesAsync();

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
                    updateGame.GenreId,
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
