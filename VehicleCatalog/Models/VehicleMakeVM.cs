using System.ComponentModel.DataAnnotations;

namespace VehicleCatalog.Models
{
    public class VehicleMakeVM
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please enter manufacturer name.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please enter manufacturer abbreviation.")]
        public string Abrv { get; set; }
    }
}
