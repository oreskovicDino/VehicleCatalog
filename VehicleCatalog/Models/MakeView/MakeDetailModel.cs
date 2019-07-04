using VehicleCatalog.Service.Models;
using X.PagedList;

namespace VehicleCatalog.Models.MakeView
{
    public class MakeDetailModel
    {
        public VehicleMakeVM MakeDetail { get; set; }
        public IPagedList<Model> ModelList { get; set; }
        public string Name { get; set; }
    }
}
