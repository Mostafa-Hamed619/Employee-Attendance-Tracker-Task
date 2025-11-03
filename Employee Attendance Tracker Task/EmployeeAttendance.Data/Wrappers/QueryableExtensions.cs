using Microsoft.EntityFrameworkCore;

namespace EmployeeAttendance.Data.Wrappers
{
    public static class QueryableExtensions
    {
        public static async Task<PageResult<T>> ToPaginatedListAsync<T>(this IQueryable<T> source, int Page, int PageSize)
            where T : class
        {
            if (source == null)
                throw new Exception("Empty");

            Page = Page == 0 ? 1 : Page;

            PageSize = PageSize == 0 ? 10 : PageSize;

            int totalCount = await source.CountAsync();

            var items = await source.Skip((Page - 1) * PageSize)
                .Take(PageSize)
                .ToListAsync();

            return new PageResult<T>
            {
                items = items,
                TotalCount = totalCount,
                Page = Page,
                PageSize = PageSize
            };
        }
    }
}
