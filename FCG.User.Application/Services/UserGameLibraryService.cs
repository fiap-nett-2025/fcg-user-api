using FCG.Application.DTO;
using FCG.Application.Services.Interfaces;
using FCG.Domain.Entities;
using FCG.Domain.Exceptions;
using FCG.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCG.Application.Services
{
    public class UserGameLibraryService : IUserGameLibraryServices
    {
        private readonly IUserGameLibraryRepository _userGameLibraryRepository;
        private readonly IUserRepository _userRepository;

        public UserGameLibraryService(
            IUserGameLibraryRepository userGameLibraryRepository
            , IUserRepository userRepository
            )
        {
            _userGameLibraryRepository = userGameLibraryRepository;
            _userRepository = userRepository;
        }

        public async Task<UserGameLibraryDto> AddGameToUserLibraryAsync(string userId, int gameId)
        {
            //checar se usuario existe
            /*var userExist = _userRepository.GetByIdAsync(model.UserId);
            if (userExist == null) {
                throw new NotFoundException($"Usuario {model.UserId} não encontrado.");
            }*/

            //checar se o jogo ja nao esta adcionado


            var userGameLibrary = new UserGameLibrary(userId, gameId);

            await _userGameLibraryRepository.AddGameToUserLibraryAsync(userGameLibrary);

            return new UserGameLibraryDto
            {
                UserId = userGameLibrary.UserId,
                GameId = userGameLibrary.GameId,
                AddedAt = userGameLibrary.AddedAt,
            };
        }

        public async Task DeleteGameInUserLibraryAsync(string userId, int GameId)
        {
            //checar se usuario existe
            /*var userExist = _userRepository.GetByIdAsync(userId);
            if (userExist == null)
            {
                throw new NotFoundException($"Usuario {userId} não encontrado.");
            }
            */
            //checar se jogo esta la

            await _userGameLibraryRepository.RemoveGameFromUserLibraryAsync(userId, GameId);
        }

        public async Task<IEnumerable<UserGameLibraryDto>> GetAllGamesFromUserLibraryAsync(string userId)
        {
            var list = await _userGameLibraryRepository.GetGamesLibraryByUserIdAsync(userId);
            return list.Select(ToDto);
        }

        public async Task<UserGameLibraryDto> GetOneGameFromUserLibraryAsync(string userId, int gameId)
        {
            var entity = await _userGameLibraryRepository.GetOneGameFromUserLibraryAsync(userId, gameId);

            if (entity is null)
            {
                // Se você tiver NotFoundException no domínio, use-a.
                // Caso não tenha, crie ou use DomainException com uma mensagem adequada.
                throw new NotFoundException($"Jogo {gameId} não encontrado na biblioteca do usuário {userId}.");
            }

            return new UserGameLibraryDto
            {
                UserId = entity.UserId,
                GameId = entity.GameId,
                AddedAt = entity.AddedAt,
            };
        }

        public Task UpdateGameInUserLibraryAsync(UpsertGameToUserLibrary model)
        {
            throw new NotImplementedException();
        }

        private static UserGameLibraryDto ToDto(UserGameLibrary e) => new UserGameLibraryDto
        {
            UserId = e.UserId,
            GameId = e.GameId,
            AddedAt = e.AddedAt
        };

        public async Task DeleteAllUserGameLibraryRecordsAsync(string userId)
        {
            //checar se usuario existe
            /*var userExist = _userRepository.GetByIdAsync(userId);
            if (userExist == null)
            {
                throw new NotFoundException($"Usuario {userId} não encontrado.");
            }*/

            await _userGameLibraryRepository.RemoveAllRecordsAsync(userId);
        }
    }
}
