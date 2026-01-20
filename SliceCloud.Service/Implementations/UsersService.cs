using SliceCloud.Repository.Interfaces;
using SliceCloud.Repository.Models;
using SliceCloud.Repository.ViewModels;
using SliceCloud.Service.Interfaces;

namespace SliceCloud.Service.Implementations;

public class UsersService(IUsersRepository usersRepository) : IUsersService
{
    private readonly IUsersRepository _usersRepository = usersRepository;

    public async Task<PaginatedList<User>> GetAllUsersAsync(int pageNumber, int pageSize, string query, string sortOrder, string sortColumn, string search)
    {
        PaginatedList<User>? paginatedUsers = await _usersRepository.GetAllUsersAsync(pageNumber, pageSize, query, sortOrder, sortColumn, search);

        return paginatedUsers;
    }

}
