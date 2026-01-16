using SliceCloud.Repository.Models;

namespace SliceCloud.Repository.Interfaces;

public interface IUsersLoginRepository
{
    Task<UsersLogin?> GetUserLoginAsync(string email, string password);

    /// <summary>
    /// Retrieves a user by their email asynchronously.
    /// </summary>
    /// <param name="email">The email of the user to retrieve.</param>
    /// <returns>A task that returns the user if found, otherwise null.</returns>
    Task<UsersLogin?> GetUserLoginByEmailAsync(string userEmail);

    /// <summary>
    /// Saves a password reset token for a user.
    /// </summary>
    /// <param name="userId">The ID of the user.</param>
    /// <param name="token">The reset token to save.</param>
    /// <param name="expiration">The expiration date of the token.</param>
    /// <param name="used">Indicates whether the token has been used.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task SavePasswordResetToken(
        int userId,
        string passwordResetToken,
        DateTime expiration,
        bool isUsed
    );

    Task<UsersLogin?> GetUserByResetToken(string resetToken);

    /// <summary>
    /// Sets a new password for a user.
    /// </summary>
    /// <param name="userLoginId">The ID of the user login to update.</param>
    /// <param name="newPassword">The new password to set.</param>
    /// <returns>A task that returns true if the password was updated successfully, otherwise false.</returns>
    Task<bool> SetUserPassword(int userLoginId, string newPassword);

    /// <summary>
    /// Invalidates a password reset token for a user.
    /// </summary>
    /// <param name="user">The user whose reset token should be invalidated.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task InvalidateResetToken(UsersLogin usersLogin);
}
