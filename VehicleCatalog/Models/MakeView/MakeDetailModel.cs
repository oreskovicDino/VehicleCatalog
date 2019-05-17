using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VehicleCatalog.Models.MakeDtos;
using VehicleCatalog.Models.ModelDto;
using VehicleCatalog.Service.Models;
using X.PagedList;

namespace VehicleCatalog.Models.MakeView
{
    public class MakeDetailModel
    {
        public MakeForDetailDto MakeDetail { get; set; }
        public IPagedList<Model> ModelList { get; set; }
        public string Name { get; set; }
    }
}
