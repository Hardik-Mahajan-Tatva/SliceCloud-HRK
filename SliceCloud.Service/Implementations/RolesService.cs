using SliceCloud.Repository.Interfaces;
using SliceCloud.Repository.Models;
using SliceCloud.Service.Interfaces;

namespace SliceCloud.Service.Implementations;

public class RolesService(IRolesRepository rolesRepository) : IRolesService
{
    private readonly IRolesRepository _rolesRepository = rolesRepository;

    public async Task<Role?> GetRoleByIdAsync(int id)
    {
        return await _rolesRepository.GetRoleByIdAsync(id);
    }
}