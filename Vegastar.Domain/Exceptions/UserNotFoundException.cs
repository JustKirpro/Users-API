namespace Vegastar.Domain.Exceptions;

public class UserNotFoundException : BusinessException
{
    public UserNotFoundException(string message) : base(message) { }
}