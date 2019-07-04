using AutoMapper;
using VehicleCatalog.Service.Models;

namespace VehicleCatalog.Models
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Make, VehicleMakeVM>().ReverseMap();
            CreateMap<Model, VehicleModelVM>().ReverseMap();
          
        }
    }
}
