using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VehicleCatalog.Models
{
    public class VehicleModelVM
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please enter model name.")]
        public string Name { get; set; }
        public string Abrv { get; set; }
        public int MakeId { get; set; }
        public string DetailMakeName { get; set; }
    }
}
