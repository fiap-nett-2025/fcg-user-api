﻿using FCG.User.Application.DTO;
using FCG.User.Application.Services;

namespace FCG.User.Application.Services.Interfaces;

public interface IUserService
{
    Task<IEnumerable<UserDto>> GetAllUsersAsync();
    Task<UserDto> GetUserByIdAsync(string id);
    Task<UserDto> CreateUserAsync(CreateUserModel model);
    Task UpdateUserAsync(string id, UpdateUserModel model);
    Task DeleteUserAsync(string id);
}
