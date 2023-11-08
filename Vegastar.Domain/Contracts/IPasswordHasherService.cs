namespace Vegastar.Domain.Contracts;

public interface IPasswordHasherService
{
    public string HashPassword(string password);
}