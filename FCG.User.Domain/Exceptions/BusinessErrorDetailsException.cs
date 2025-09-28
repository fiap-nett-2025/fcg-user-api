using Microsoft.AspNetCore.Http;

namespace FCG.User.Domain.Exceptions
{
    public class BusinessErrorDetailsException : BaseCustomException
    {
        public BusinessErrorDetailsException(string message)
            : base(StatusCodes.Status400BadRequest, message) { }
    }
}