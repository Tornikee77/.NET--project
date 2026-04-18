using System.ComponentModel.DataAnnotations;

namespace _NET_PROJECT.Dtos;

public record class UpdateGameDto(string Name, int GenreId, decimal Price, DateOnly ReleaseDate);
