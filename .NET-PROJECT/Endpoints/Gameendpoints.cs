using _NET_PROJECT.Data;
using _NET_PROJECT.Dtos;
using _NET_PROJECT.Models;
using Microsoft.EntityFrameworkCore;

namespace _NET_PROJECT.Endpoints;

public static class Gameendpoints
{
    const string GetGameEndpointName = "GetGame";

    public static void MapGameEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/games");
        // GET /Games
        group.MapGet(
            "/",
            async (GameStoreContext db) =>
                await db
                    .Games.Include(game => game.Genre)
                    .Select(game => new GameDto(
                        game.Id,
                        game.Name,
                        game.GenreId,
                        game.Price,
                        game.ReleaseDate
                    ))
                    .AsNoTracking()
                    .ToListAsync()
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
            async (int id, UpdateGameDto dto, GameStoreContext db) =>
            {
                var game = await db.Games.FindAsync(id);

                if (game is null)
                    return Results.NotFound();

                game.Name = dto.Name;
                game.GenreId = dto.GenreId;
                game.Price = dto.Price;
                game.ReleaseDate = dto.ReleaseDate;

                await db.SaveChangesAsync();

                return Results.NoContent();
            }
        );

        // DELETE /Games/{id}
        group.MapDelete(
            "/{id}",
            async (int id, GameStoreContext db) =>
            {
                var game = await db.Games.FindAsync(id);

                if (game is null)
                    return Results.NotFound();

                db.Games.Remove(game);
                await db.SaveChangesAsync();

                return Results.NoContent();
            }
        );
    }
}
