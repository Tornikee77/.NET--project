namespace _NET_PROJECT.Dtos;

public record class UpdateGameDto(
    string Name, 
    string Genre, 
    decimal Price, 
    DateOnly ReleaseDate);

