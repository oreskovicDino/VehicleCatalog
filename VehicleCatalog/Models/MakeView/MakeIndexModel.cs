using VehicleCatalog.Service.Models;
using X.PagedList;

namespace VehicleCatalog.Models.MakeView
{
    public class MakeIndexModel
    {
        public IPagedList<Make> MakeList { get; set; }

        public string Name { get; set; }

        public string Abrv { get; set; }
        public string SortStatus { get; set; }
        public string SearchString { get; set; }
        public IPagination Pagination { get; set; }

    }
}
