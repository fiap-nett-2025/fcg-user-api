using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCG.User.Domain.Exceptions
{
    public class NotFoundException : BaseCustomException
    {
        public NotFoundException(string message = "Recurso não encontrado.")
            : base(StatusCodes.Status404NotFound, message) { }
    }
}
