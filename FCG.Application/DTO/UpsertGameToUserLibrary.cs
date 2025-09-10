using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCG.Application.DTO
{
    public class UpsertGameToUserLibrary
    {
        public required string UserId { get; set; }
        public required int GameId { get; set; }

    }
}
