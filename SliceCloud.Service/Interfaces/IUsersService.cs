using Microsoft.AspNetCore.Http;
using SliceCloud.Repository.Models;
using SliceCloud.Repository.ViewModels;

namespace SliceCloud.Service.Interfaces;

public interface IUsersService
{
    /// <summary>
    /// Retrieves all users as an paginated result.
    /// </summary>
    /// <returns>An paginated result of all users.</returns>
    Task<PaginatedList<User>> GetAllUsersAsync(int pageNumber, int pageSize, string query, string sortOrder, string sortColumn, string search);

    /// <summary>
    /// Validates the uniqueness of fields for a new user asynchronously.
    /// </summary>
    /// <param name="model">The view model containing user details.</param>
    /// <returns>A task that returns a dictionary of validation errors, if any.</returns>
    Task<Dictionary<string, string>> ValidateUniqueFieldsAsync(CreateUserViewModel createUserViewModel);

    /// <summary>
    /// Creates a new user asynchronously.
    /// </summary>
    /// <param name="createUserViewModel">The view model containing user details.</param>
    /// <param name="itemImage">The form file for the image.</param>
    /// <returns>A task that returns true if the creation was successful, otherwise false.</returns>
    Task<bool> CreateUserAsync(CreateUserViewModel createUserViewModel, IFormFile itemImage);

    /// <summary>
    /// Retrieves a user by their ID asynchronously.
    /// </summary>
    /// <param name="userId">The ID of the user to retrieve.</param>
    /// <returns>A task that returns the user view model if found, otherwise null.</returns>
    Task<UpdateUserViewModel?> GetUserByIdAsync(int userId);
}
