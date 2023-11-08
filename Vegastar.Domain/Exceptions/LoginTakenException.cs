namespace Vegastar.Domain.Exceptions;

public class LoginTakenException : BusinessException
{
    public LoginTakenException(string message) : base(message) { }
}