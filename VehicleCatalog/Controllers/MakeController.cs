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
using X.PagedList;

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

        public IActionResult Index(int? page, string search = null, string sort = null)
        {
            ViewData["Title"] = "Manufacturers";

            var pageNumber = page ?? 1;

            if (!string.IsNullOrWhiteSpace(search))
            {
                var searchMakeForList = mapper.Map<IEnumerable<MakeForListDto>>(service.SearchMakes(search));

                var searchMakePage = searchMakeForList;

                switch (sort)
                {
                    case "NameDesc":
                        searchMakePage.OrderByDescending(o => o.Name);
                        break;
                    case "AbrvAsc":
                        searchMakePage.OrderBy(o => o.Abrv);
                        break;
                    case "AbrvDesc":
                        searchMakePage.OrderByDescending(o => o.Abrv);
                        break;
                    default:
                        searchMakePage.OrderBy(o => o.Abrv);
                        break;
                }

                searchMakePage = searchMakePage.ToPagedList(pageNumber, 5);

                var searchMake = new MakeIndexModel
                {
                    MakeList = searchMakePage,
                    SortStatus = sort,
                    SearchString = search,
                    PageNum = pageNumber
                };

                return View(searchMake);
            }

            var makeForList = mapper.Map<IEnumerable<MakeForListDto>>(service.GetAllMakes());

            var makePage = makeForList;



            switch (sort)
            {

                case "NameDesc":
                    makePage = makePage.OrderByDescending(o => o.Name);
                    break;
                case "AbrvAsc":
                    makePage = makePage.OrderBy(o => o.Abrv);
                    break;
                case "AbrvDesc":
                    makePage = makePage.OrderByDescending(o => o.Abrv);
                    break;
                default:
                    makePage = makePage.OrderBy(o => o.Name);
                    break;
            }

            makePage = makePage.ToPagedList(pageNumber, 5);

            var make = new MakeIndexModel
            {
                MakeList = makePage,
                SortStatus = sort,
                SearchString = search,
                PageNum = pageNumber
            };

            return View(make);
        }

        public IActionResult Detail(int? page, int id)
        {
            ViewData["Title"] = "Manufacturer | Detail | ";

            var pageNumber = page ?? 1;

            var make = service.GetMakeById(id);

            var modelListView = mapper.Map<IEnumerable<ModelListForMakeDto>>(make.Models);

            var modelPage = modelListView.ToPagedList(pageNumber, 5);

            var makeDetail = new MakeDetailModel
            {
                MakeDetail = mapper.Map<MakeForDetailDto>(make),
                ModelList = modelPage
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

            service.Create(makeForCreation);
            await service.SaveAll();

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