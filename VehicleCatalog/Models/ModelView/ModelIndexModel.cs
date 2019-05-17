using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VehicleCatalog.Models.MakeDtos;
using VehicleCatalog.Models.ModelDto;
using VehicleCatalog.Service.Models;
using X.PagedList;

namespace VehicleCatalog.Models.ModelView
{
    public class ModelIndexModel
    {
        public IPagedList<Model> ModelList { get; set; }
        public IPagedList<Make> MakeList { get; set; }
        public int MakeId { get; set; }
        public string Name { get; set; }
        public string SortStatus { get; set; }
        public string SearchString { get; set; }
        public IPagination Pagination{ get; set; }
    }
}
