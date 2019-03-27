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
    }
}
