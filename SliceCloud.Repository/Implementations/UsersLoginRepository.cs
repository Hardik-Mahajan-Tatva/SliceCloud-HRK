using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using SliceCloud.Repository.Interfaces;
using SliceCloud.Repository.Models;

namespace SliceCloud.Repository.Implementations;

public class UsersLoginRepository(SliceCloudContext context) : IUsersLoginRepository
{
    private readonly SliceCloudContext _context = context;

    public async Task<UsersLogin?> GetUserLoginByEmailAsync(string userEmail)
    {
        return await _context.UsersLogins.FirstOrDefaultAsync(u => u.Email!.ToLower() == userEmail.ToLower());
    }
    
    public async Task<UsersLogin?> GetUserLoginAsync(string userEmail, string userPassword)
    {
        UsersLogin? user = await _context.UsersLogins.Include(u => u.User).FirstOrDefaultAsync(
            u => u.Email == userEmail
            && u.PasswordHash == userPassword
            && u.IsFirstLogin == false
            && u.User!.IsDeleted == false
            && u.User.Status == 1
            );
        return user;
    }

    public async Task SavePasswordResetToken(int userId, string passwordResetToken, DateTime expiration, bool isUsed)
    {
        UsersLogin? user = await _context.UsersLogins.FindAsync(userId);
        if (user != null)
        {
            user.ResetToken = passwordResetToken;
            user.ResetTokenExpiration = expiration;
            user.IsResetTokenUsed = isUsed;
            await _context.SaveChangesAsync();
        }
    }

    public async Task<UsersLogin?> GetUserByResetToken(string resetToken)
    {
        return await _context.UsersLogins.FirstOrDefaultAsync(u => u.ResetToken == resetToken);
    }

    public async Task<bool> SetUserPassword(int userLoginId, string newPassword)
    {
        UsersLogin? user = await _context.UsersLogins.FindAsync(userLoginId);
        if (user == null)
        {
            return false;
        }
        User? userTable = await _context.Users.FindAsync(user.UserId);
        if (userTable == null)
        {
            return false;
        }
        user.PasswordHash = newPassword;
        userTable.PasswordHash = newPassword;

        _context.UsersLogins.Update(user);
        _context.Users.Update(userTable);
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task InvalidateResetToken(UsersLogin usersLogin)
    {
        usersLogin.IsResetTokenUsed = true;
        _context.UsersLogins.Update(usersLogin);
        await _context.SaveChangesAsync();
    }

}
