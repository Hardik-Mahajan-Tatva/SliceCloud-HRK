using SliceCloud.Repository.Models;

namespace SliceCloud.Repository.Interfaces;

public interface IRolesRepository
{
    /// <summary>
    /// Retrieves a role by its ID asynchronously.
    /// </summary>
    /// <param name="id">The ID of the role to retrieve.</param>
    /// <returns>A task that returns the role if found, otherwise null.</returns>
    Task<Role?> GetRoleByIdAsync(int id);
}
