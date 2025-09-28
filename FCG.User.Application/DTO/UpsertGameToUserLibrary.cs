using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCG.User.Application.DTO
{
    public class UpsertGameToUserLibrary
    {
        public required string UserId { get; set; }
        public required string GameId { get; set; }

    }
}
