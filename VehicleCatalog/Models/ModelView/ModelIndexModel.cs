using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VehicleCatalog.Models.MakeDtos;
using VehicleCatalog.Models.ModelDto;

namespace VehicleCatalog.Models.ModelView
{
    public class ModelIndexModel
    {
        public IEnumerable<ModelForListDto> ModelList { get; set; }
        public IEnumerable<MakeForListDto> MakeList { get; set; }
        public int MakeId { get; set; }
        public string Name { get; set; }
        public string SortStatus { get; set; }
        public string SearchString { get; set; }
        public int PageNum { get; set; }
    }
}
