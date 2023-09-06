namespace ABI.API.Structure.Application.DTO
{
    public class PaginatedSearchDTO
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public string Search { get; set; }
        public string SortOrder { get; set; }
        public string SortDirection { get; set; }
    }
}
