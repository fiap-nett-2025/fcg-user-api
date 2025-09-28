using Microsoft.AspNetCore.Http;

namespace FCG.User.Domain.Exceptions
{
    public class TechnicalErrorDetailsException : BaseCustomException
    {
        public TechnicalErrorDetailsException(string message, Exception? innerException = null)
            : base(StatusCodes.Status500InternalServerError, message, innerException) { }
    }
}
