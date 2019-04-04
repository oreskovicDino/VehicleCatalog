using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VehicleCatalog.Models.MakeDtos;
using VehicleCatalog.Models.ModelDto;

namespace VehicleCatalog.Models.ModelView
{
    public class DetailModel
    {
        public ModelForDetailDto ModelDetail { get; set; }
        public MakeForDetailDto MakeDetail { get; set; }
        public string Name { get; set; }
        public int Id { get; set; }
        public int MakeId { get; set; }
    }
}
