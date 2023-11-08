using Vegastar.Domain.Contracts;

namespace Vegastar.Domain.Services;

public class PasswordHasherService : IPasswordHasherService
{
    public string HashPassword(string password) => BCrypt.Net.BCrypt.HashPassword(password);
}