namespace EmployeeAttendance.Data.Wrappers;

public class PageResult<T>
{
    public List<T> items { get; set; } = new();
    public int TotalCount { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }

    public int TotalPage => (int)Math.Ceiling((double)TotalCount / PageSize);
}
