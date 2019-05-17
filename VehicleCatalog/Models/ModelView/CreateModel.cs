using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using VehicleCatalog.Models.MakeDtos;

namespace VehicleCatalog.Models.ModelView
{
    public class CreateModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Abrv { get; set; }
        public int MakeId { get; set; }
        public string DetailMakeName { get; set; }
    }
}
