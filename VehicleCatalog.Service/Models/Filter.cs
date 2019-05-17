using System;
using System.Collections.Generic;
using System.Text;

namespace VehicleCatalog.Service.Models
{
    // Class for searching and filtering 
    public class Filter : IFilter
    {
        // Keyword for searching tables
        public string FilterString { get; set; }
    }
}
