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
}
