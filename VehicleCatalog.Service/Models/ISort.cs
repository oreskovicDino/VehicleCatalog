using System;
using System.Collections.Generic;
using System.Text;

namespace VehicleCatalog.Service.Models
{
    // Interface for sorting tables
    public interface ISort
    {
        // Keyword for sorting tables
        string Sorting { get; set; }
    }
}
