namespace MatMate.Api.Models;

public class RegistrationDto
{
    public required string Username { get; init; }
    public required string Password { get; init; }
    public string? Email { get; init; }
}