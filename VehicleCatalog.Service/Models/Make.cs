using System.Collections.Generic;

namespace VehicleCatalog.Service.Models
{
    // EF model for Makes table
    public class Make
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Abrv { get; set; }

        public ICollection<Model> Models { get; set; }
    }
}
