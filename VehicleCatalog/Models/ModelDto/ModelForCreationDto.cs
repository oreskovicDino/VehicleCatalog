using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using VehicleCatalog.Models.MakeDtos;
using VehicleCatalog.Service.Models;

namespace VehicleCatalog.Models.ModelDto
{
    public class ModelForCreationDto
    {
        [Required(ErrorMessage = "Please enter model name.")]
        public string Name { get; set; }
        public string Abrv { get; set; }
        public int MakeId { get; set; }
        public string DetailMakeName { get; set; }
    }
}
