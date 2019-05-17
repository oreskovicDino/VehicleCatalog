using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VehicleCatalog.Models.MakeDtos;
using VehicleCatalog.Models.ModelDto;
using VehicleCatalog.Models.ModelView;
using VehicleCatalog.Service;
using VehicleCatalog.Service.Models;
using X.PagedList;

namespace VehicleCatalog.Controllers
{
    //Model administration view (CRUD with Sorting, Filtering & Paging)
    public class ModelController : Controller
    {
        private readonly IVehicleService service;
        private readonly IMapper mapper;

        public ModelController(IVehicleService service, IMapper mapper)
        {
            this.service = service;
            this.mapper = mapper;
        }

        // (Index Page) Selects all records from the Models table.
        // GET: /Model/Index
        public async Task<IActionResult> Index(int? page, string search, string sort)
        {
            ViewData["Title"] = "Models";

            IPagination paging = new Pagination() { CurrentPage = page };
            ISort sorting = new Sort() { Sorting = sort };
            IFilter filter = new Filter() { FilterString = search };

            IPagedList<Model> modelPage = await service.GetAllModels(paging, sorting, filter);

            var model = new ModelIndexModel
            {
                ModelList = modelPage,
                SortStatus = sort,
                SearchString = search,
                Pagination = paging
            };

            return View(model);
        }


        // (Detail Page) Selects a single record from the Models table. 
        // GET: /Model/Detail/id
        public async Task<IActionResult> Detail(int? id)
        {
            ViewData["Title"] = "Models | Detail | ";

            try
            {
                if (id.HasValue)
                {
                    IModelToReturn model = await service.GetModelById(id);
                    var modelDetail = model.ModelByID;

                    DetailModel detailModel = new DetailModel
                    {
                        ModelDetail = mapper.Map<ModelForDetailDto>(model.ModelByID),
                        MakeDetail = mapper.Map<MakeForDetailDto>(model.MakeByID),
                        Id = modelDetail.Id,
                        Abrv = modelDetail.Abrv,
                        MakeId = modelDetail.MakeId
                    };
                    return View(detailModel);
                }
                else
                {
                    return BadRequest("The ID isn't valid");
                }
            }
            catch (Exception)
            {
                return BadRequest("Something went wrong");
            }
        }


        // (SelectMake Page) Selects all records record from the Makes table. 
        // GET: /Model/SelectMake/id
        public async Task<IActionResult> SelectMake(int? page, string search, string sort)
        {
            IPagination paging = new Pagination() { CurrentPage = page };
            ISort sorting = new Sort() { Sorting = sort };
            IFilter filter = new Filter() { FilterString = search };

            IPagedList<Make> makePage = await service.GetAllMakes(paging, sorting, filter);
            var make = new ModelIndexModel
            {
                MakeList = makePage,
                Pagination = paging,
                SearchString = search,
                SortStatus = sort
            };
            return View(make);
        }


        // (Create Page) selects a single record from the Makes table. 
        // GET: /Model/Create?makeId
        public async Task<IActionResult> Create(int? makeId)
        {
            ViewData["Title"] = "Models | Create | ";

            IMakeToReturn make = await service.GetMakeById(makeId);
            var makeForModel = mapper.Map<MakeForModelDto>(make.MakeByID);

            var createModel = new CreateModel
            {
                Abrv = makeForModel.Abrv,
                MakeId = makeForModel.Id,
                DetailMakeName = makeForModel.Name
            };

            return View(createModel);
        }

        // Adds a record to table Models.
        //

        [HttpPost]
        public async Task<IActionResult> Create(ModelForCreationDto model)
        {
            try
            {
                var modelForCreation = mapper.Map<Model>(model);
                if (ModelState.IsValid)
                {

                    await service.Create(modelForCreation);
                    return RedirectToAction("Detail", "Model", new { id = modelForCreation.Id });
                }
                else
                {
                    var createModel = new CreateModel
                    {
                        Abrv = model.Abrv,
                        MakeId = model.MakeId,
                        DetailMakeName = model.DetailMakeName
                    };
                    return View(createModel);
                }
            }
            catch (Exception)
            {

                return BadRequest();
            }

        }


        // Updates a record from table Models.
        //
        [HttpPost]
        public async Task<IActionResult> Update(ModelForUpdateDto model)
        {
            try
            {
                if (await service.UpdateModel(mapper.Map<Model>(model)))
                {
                    return RedirectToAction("Detail", "Model", new { id = model.Id });
                }
                return BadRequest("Something went wrong");
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }


        // Removes a record from table Models.
        //
        public async Task<IActionResult> Delete(int? id)
        {
            try
            {
                if (id.HasValue)
                {
                    if (await service.DeleteModel((int)id))
                    {
                        return RedirectToAction("Index", "Model");
                    }
                    else
                    {
                        return BadRequest("Something went wrong, we couldn't delete this model");
                    }
                }
                else
                {
                    return BadRequest("The ID isn't valid");
                }
            }
            catch (Exception e)
            {

                return BadRequest(e);
            }
        }


    }
}