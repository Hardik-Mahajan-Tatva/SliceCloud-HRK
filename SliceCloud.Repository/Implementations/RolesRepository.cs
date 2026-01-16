using Microsoft.EntityFrameworkCore;
using SliceCloud.Repository.Interfaces;
using SliceCloud.Repository.Models;

namespace SliceCloud.Repository.Implementations;

public class RolesRepository(SliceCloudContext context) : IRolesRepository
{
    private readonly SliceCloudContext _context = context;
    public async Task<Role?> GetRoleByIdAsync(int roleId)
    {
        return await _context.Roles.FirstOrDefaultAsync(r => r.RoleId == roleId);
    }
}
