using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VehicleCatalog.Models.MakeDtos;
using VehicleCatalog.Models.ModelDto;

namespace VehicleCatalog.Models.MakeView
{
    public class MakeDetailModel
    {
        public MakeForDetailDto MakeDetail { get; set; }
        public IEnumerable<ModelListForMakeDto> ModelList { get; set; }
    }
}
