namespace PhoneCompany.Application.DTOs
{
    public class PaginatedResponse<T>
    {
        public List<T> Items { get; set; } = [];
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int PageCount { get; set; }
    }
}
