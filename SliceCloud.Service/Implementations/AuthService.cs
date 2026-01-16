using SliceCloud.Repository.Interfaces;
using SliceCloud.Repository.Models;
using SliceCloud.Service.Interfaces;

namespace SliceCloud.Service.Implementations;

public class AuthService(IUsersLoginRepository usersLoginRepository) : IAuthService
{
    private readonly IUsersLoginRepository _usersLoginRepository = usersLoginRepository;

    public async Task<UsersLogin?> AuthenticateUser(string email, string password)
    {
        UsersLogin? user = await _usersLoginRepository.GetUserLoginAsync(email, password);

        if (user == null) return null;

        return user;
    }

    public async Task<UsersLogin?> GetUserLoginByEmailAsync(string email)
    {
        UsersLogin? usersLogin = await _usersLoginRepository.GetUserLoginByEmailAsync(email);
        if (usersLogin is not null)
            return usersLogin;
        return null;
    }
    
    public async Task<bool> CheckIfUserExists(string email)
    {
        UsersLogin? usersLogin = await _usersLoginRepository.GetUserLoginByEmailAsync(email);
        if (usersLogin is not null)
            return true;
        return false;
    }

    public async Task<string> GeneratePasswordResetToken(string email)
    {
        UsersLogin? user = await _usersLoginRepository.GetUserLoginByEmailAsync(email);
        if (user == null) return string.Empty;

        string token = Convert.ToBase64String(Guid.NewGuid().ToByteArray());

        await _usersLoginRepository.SavePasswordResetToken(user.UserLoginId,
          token,
          DateTime.UtcNow.AddHours(24),
          false);

        return token;
    }

    public async Task<bool> ValidatePasswordResetToken(string token)
    {
        UsersLogin? tokenEntry = await _usersLoginRepository.GetUserByResetToken(token);
        if (tokenEntry == null || tokenEntry.ResetTokenExpiration.GetValueOrDefault() < DateTime.UtcNow || tokenEntry.IsResetTokenUsed == true)
        {
            return false;
        }
        return true;
    }

    public async Task<bool> UpdateUserPassword(string token, string newPassword)
    {
        UsersLogin? user = await _usersLoginRepository.GetUserByResetToken(token);
        if (user == null || user.ResetTokenExpiration.GetValueOrDefault() < DateTime.UtcNow || user.IsResetTokenUsed == true)
        {
            return false;
        }

        bool passwordUpdated = await _usersLoginRepository.SetUserPassword(user.UserLoginId, newPassword);
        if (!passwordUpdated)
        {
            return false;
        }

        user.IsResetTokenUsed = true;
        user.IsFirstLogin = false;
        await _usersLoginRepository.InvalidateResetToken(user);

        return true;
    }
}
