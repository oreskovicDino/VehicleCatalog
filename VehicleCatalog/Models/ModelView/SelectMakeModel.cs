using VehicleCatalog.Service.Models;
using X.PagedList;

namespace VehicleCatalog.Models.ModelView
{
    public class SelectMakeModel
    {
        public IPagedList<Make> MakeList { get; set; }
        public string SortStatus { get; set; }
        public string SearchString { get; set; }
        public IPagination Pagination { get; set; }
    }
}
