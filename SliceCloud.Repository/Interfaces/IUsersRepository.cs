using SliceCloud.Repository.Models;
using SliceCloud.Repository.ViewModels;

namespace SliceCloud.Repository.Interfaces;

public interface IUsersRepository
{
    /// <summary>
    /// Retrieves all paginated users for further filtering or querying.
    /// </summary>
    /// <returns>An paginated result of all users.</returns>
    Task<PaginatedList<User>> GetAllUsersAsync(int pageNumber, int pageSize, string query, string sortOrder, string sortColumn, string search);
}
