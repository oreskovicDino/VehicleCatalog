using System.ComponentModel.DataAnnotations;

namespace VehicleCatalog.Models.MakeDtos
{
    public class MakeForCreationDto
    {
        
        [Required(ErrorMessage = "Please enter manufacturer name.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please enter manufacturer abbreviation.")]
        public string Abrv { get; set; }
    }
}
