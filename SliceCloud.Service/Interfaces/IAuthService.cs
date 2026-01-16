using SliceCloud.Repository.Models;

namespace SliceCloud.Service.Interfaces;

public interface IAuthService
{
    Task<UsersLogin?> AuthenticateUser(string email, string password);

    /// <summary>
    /// Checks if a user exists by their email.
    /// </summary>
    /// <param name="email">The email of the user to check.</param>
    /// <returns>A task that returns true if the user exists, otherwise false.</returns>
    Task<UsersLogin?> GetUserLoginByEmailAsync(string email);

    /// <summary>
    /// Checks if a user exists by their email.
    /// </summary>
    /// <param name="email">The email of the user to check.</param>
    /// <returns>A task that returns true if the user exists, otherwise false.</returns>
    Task<bool> CheckIfUserExists(string email);

    /// <summary>
    /// Generates a password reset token for a user.
    /// </summary>
    /// <param name="email">The email of the user to generate the token for.</param>
    /// <returns>A task that returns the generated password reset token.</returns>
    Task<string> GeneratePasswordResetToken(string email);

    /// <summary>
    /// Validates a password reset token.
    /// </summary>
    /// <param name="token">The token to validate.</param>
    /// <returns>A task that returns true if the token is valid, otherwise false.</returns>
    Task<bool> ValidatePasswordResetToken(string token);

    /// <summary>
    /// Updates a user's password using a password reset token.
    /// </summary>
    /// <param name="token">The password reset token.</param>
    /// <param name="newPassword">The new password to set.</param>
    /// <returns>A task that returns true if the password was updated successfully, otherwise false.</returns>
    Task<bool> UpdateUserPassword(string token, string newPassword);
}
