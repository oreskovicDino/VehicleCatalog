using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using VehicleCatalog.Models.MakeDtos;
using VehicleCatalog.Models.MakeView;
using VehicleCatalog.Models.ModelDto;
using VehicleCatalog.Service;
using VehicleCatalog.Service.Models;

namespace VehicleCatalog.Controllers
{
    public class MakeController : Controller
    {
        private readonly IVehicleService service;
        private readonly IMapper mapper;

        public MakeController(IVehicleService service, IMapper mapper)
        {
            this.service = service;
            this.mapper = mapper;
        }
        public IActionResult Index()
        {
            var makeForList = mapper.Map<IEnumerable<MakeForListDto>>(service.GetAllMakes());

            var make = new MakeIndexModel
            {
                MakeList = makeForList
            };

            return View(make);
        }

        public IActionResult Detail(int id)
        {
            var make = service.GetMakeById(id);

            var modelListView = mapper.Map<IEnumerable<ModelListForMakeDto>>(make.Models);

            var makeDetail = new MakeDetailModel
            {
                MakeDetail = mapper.Map<MakeForDetailDto>(make),
                ModelList = modelListView
            };

            return View(makeDetail);
        }
    }
}