namespace Vegastar.Domain.Exceptions;

public class AdminAlreadyExistsException : BusinessException
{
    public AdminAlreadyExistsException(string message) : base(message) { }
}