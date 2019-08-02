namespace VehicleCatalog.Service.Models
{
    // Interface for paging content
    public interface IPagination
    {
        // Current page number
        int? CurrentPage { get; set; }
        // Size of paged list
        int? Size { get; set; }
    }
}
