using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VehicleCatalog.Models.ModelDto
{
    public class ModelForCreationDto
    {
        public string Name { get; set; }
        public string Abrv { get; set; }
        public int MakeId { get; set; }
    }
}
