namespace VehicleCatalog.Service.Models
{
    // Class for paging content
    public class Pagination : IPagination
    {
        // Current page number
        public int? CurrentPage { get; set; }
        // Size of paged list
        public int? Size { get; set; }
    }
}
