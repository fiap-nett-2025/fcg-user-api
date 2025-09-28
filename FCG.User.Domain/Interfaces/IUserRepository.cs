
using FCG.User.Domain.Entities;

namespace FCG.User.Domain.Interfaces;

public interface IUserRepository
{
    Task<Entities.User> GetByIdAsync(string id);
    Task<Entities.User> GetByEmailAsync(string email);
    Task AddAsync(Entities.User user);
    Task UpdateAsync(Entities.User user);
    Task DeleteAsync(string id);
    Task<bool> ExistsByEmailAsync(string email);
}
