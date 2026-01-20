using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using SliceCloud.Repository.Interfaces;
using SliceCloud.Repository.Models;
using SliceCloud.Repository.ViewModels;

namespace SliceCloud.Repository.Implementations;

public class UsersRepository(SliceCloudContext context) : IUsersRepository
{
    private readonly SliceCloudContext _context = context;

    public async Task<PaginatedList<User>> GetAllUsersAsync(int pageNumber, int pageSize, string query, string sortOrder, string sortColumn, string search)
    {
        IQueryable<User>? usersQuery = _context.Users
       .AsNoTracking()
       .Where(u => u.IsDeleted == false);

        if (!string.IsNullOrEmpty(search))
        {
            usersQuery = usersQuery.Where(u =>
                (u.FirstName != null && (u.FirstName.Contains(search) || u.FirstName.Contains(search, StringComparison.CurrentCultureIgnoreCase))) ||
                (u.Email != null && (u.Email.Contains(search) || u.Email.Contains(search, StringComparison.CurrentCultureIgnoreCase))) ||
                (u.PhoneNumber != null && (u.PhoneNumber.Contains(search) || u.PhoneNumber.Contains(search, StringComparison.CurrentCultureIgnoreCase)))
            );
        }

        if (!string.IsNullOrEmpty(sortColumn))
        {
            ParameterExpression parameter = Expression.Parameter(typeof(User), "u");
            MemberExpression property = Expression.Property(parameter, sortColumn);
            Expression<Func<User, object>> lambda = Expression.Lambda<Func<User,
              object>>(Expression.Convert(property, typeof(object)), parameter);

            usersQuery = sortOrder.Equals("desc", StringComparison.CurrentCultureIgnoreCase) ?
              usersQuery.OrderByDescending(lambda) :
              usersQuery.OrderBy(lambda);
        }

        PaginatedList<User> paginatedUsers = await PaginatedList<User>.CreateAsync(usersQuery, pageNumber, pageSize);
        return paginatedUsers;
    }

}
