using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VehicleCatalog.Models.MakeDtos;

namespace VehicleCatalog.Models.MakeView
{
    public class MakeIndexModel
    {        
        public IEnumerable<MakeForListDto> MakeList { get; set; }
        public string Name { get; set; }
        public string Abrv { get; set; }
        public string SortStatus { get; set; }
        public string SearchString { get; set; }
        public int PageNum { get; set; }

    }
}
