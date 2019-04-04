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
        public IActionResult Index(string search = null)
        {
            ViewData["Title"] = "Manufacturers";

            if (!string.IsNullOrWhiteSpace(search))
            {
                var searchMakeForList = mapper.Map<IEnumerable<MakeForListDto>>(service.SearchMakes(search));

                var searchMake = new MakeIndexModel
                {
                    MakeList = searchMakeForList,
                };

                return View(searchMake);
            }

            var makeForList = mapper.Map<IEnumerable<MakeForListDto>>(service.GetAllMakes());

            var make = new MakeIndexModel
            {
                MakeList = makeForList,
            };

            return View(make);
        }

        public IActionResult Detail(int id)
        {
            ViewData["Title"] = "Manufacturer | Detail | ";

            var make = service.GetMakeById(id);

            var modelListView = mapper.Map<IEnumerable<ModelListForMakeDto>>(make.Models);

            var makeDetail = new MakeDetailModel
            {
                MakeDetail = mapper.Map<MakeForDetailDto>(make),
                ModelList = modelListView
            };

            return View(makeDetail);
        }

        

        public async Task<IActionResult> Delete(int id)
        {
            var makeForDeletion = service.GetMakeById(id);
            service.Delete(makeForDeletion);
            if (await service.SaveAll())
            {
                return RedirectToAction(nameof(Index));
            }

            return NoContent();
        }

        public IActionResult Update(int id)
        {
            ViewData["Title"] = "Manufacturer | Edit ";

            var make = service.GetMakeById(id);
            var modelListView = mapper.Map<IEnumerable<ModelListForMakeDto>>(make.Models);

            var makeDetail = new MakeUpdateModel
            {
                Id = make.Id,
                Name = make.Name,
                Abrv = make.Abrv,
                ModelList = modelListView
            };

            return View(makeDetail);
        }


        [HttpPost]
        public async Task<IActionResult> AddMake(MakeForCreationDto make)
        {


            var makeForCreation = mapper.Map<Make>(make);

            await service.CreateMake(makeForCreation);

            return RedirectToAction("Detail", "Make", new { id = makeForCreation.Id });
        }

        [HttpPost]
        public async Task<IActionResult> Update(MakeForUpdateDto make)
        {
            
           
            service.Update(mapper.Map<Make>(make));
            service.UpdateModelAbrv(make.Id, make.Abrv);
            await service.SaveAll();

            return RedirectToAction("Detail", "Make", new { id = make.Id });
        }

    }
}