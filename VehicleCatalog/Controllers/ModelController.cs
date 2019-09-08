using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using VehicleCatalog.Models;
using VehicleCatalog.Models.ModelView;
using VehicleCatalog.Service;
using VehicleCatalog.Service.Models;
using VehicleCatalog.Service.Services.Common;
using X.PagedList;

namespace VehicleCatalog.Controllers
{
    //Model administration view (CRUD with Sorting, Filtering & Paging)
    public class ModelController : Controller
    {
        #region Fields

        private readonly IModelService modelService;
        private readonly IMapper mapper;

        #endregion

        public ModelController(IModelService modelService, IMapper mapper)
        {
            this.modelService = modelService;
            this.mapper = mapper;
        }

        #region Index

        // (Index Page) Selects all records from the Models table.
        // GET: /Model/Index
        public async Task<IActionResult> Index(int? page, string search, string sort)
        {
            ViewData["Title"] = "Models";

            IPagination paging = new Pagination() { CurrentPage = page };
            ISort sorting = new Sort() { Sorting = sort };
            IFilter filter = new Filter() { FilterString = search };

            IPagedList<Model> modelPage = await modelService.GetModelsAsync(paging, sorting, filter);

            var model = new ModelIndexModel
            {
                ModelList = modelPage,
                SortStatus = sort,
                SearchString = search,
                Pagination = paging
            };
            return View(model);
        }

        #endregion

        #region Detail

        // (Detail Page) Selects a single record from the Models table. 
        // GET: /Model/Detail/id
        public async Task<IActionResult> Detail(int? id)
        {
            ViewData["Title"] = "Models | Detail | ";

            try
            {
                if (id.HasValue)
                {
                    Model model = await modelService.GetModelAsync(id);

                    DetailModel detailModel = new DetailModel
                    {
                        ModelDetail = mapper.Map<VehicleModelVM>(model),
                        MakeDetail = mapper.Map<VehicleMakeVM>(await modelService.GetMakeAsync(model.MakeId)),
                        Id = model.Id,
                        Abrv = model.Abrv,
                        MakeId = model.MakeId
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

        #endregion

        #region SelectMake

        // (SelectMake Page) Selects all records record from the Makes table. 
        // GET: /Model/SelectMake/id
        public async Task<IActionResult> SelectMake(int? page, string search, string sort)
        {
            IPagination paging = new Pagination() { CurrentPage = page };
            ISort sorting = new Sort() { Sorting = sort };
            IFilter filter = new Filter() { FilterString = search };

            IPagedList<Make> makePage = await modelService.GetMakesAsync(paging, sorting, filter);

            var make = new SelectMakeModel
            {
                MakeList = makePage,
                Pagination = paging,
                SearchString = search,
                SortStatus = sort
            };
            return View(make);
        }

        #endregion

        #region Create

        // (Create Page) selects a single record from the Makes table. 
        // GET: /Model/Create?makeId
        public async Task<IActionResult> Create(int? makeId)
        {
            ViewData["Title"] = "Models | Create | ";

            VehicleMakeVM makeForModel = mapper.Map<VehicleMakeVM>(await modelService.GetMakeAsync(makeId));

            var createModel = new CreateModel
            {
                Abrv = makeForModel.Abrv,
                MakeId = makeForModel.Id,
                DetailMakeName = makeForModel.Name
            };
            return View(createModel);
        }

        // Adds a record to table Models.
        [HttpPost]
        public IActionResult Create(VehicleModelVM model)
        {
            try
            {
                var modelForCreation = mapper.Map<Model>(model);
                if (ModelState.IsValid)
                {
                    modelService.Create(modelForCreation);
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

        #endregion

        #region Update

        // Updates a record from table Models.
        [HttpPost]
        public IActionResult Update(VehicleModelVM model)
        {
            try
            {
                modelService.Update(mapper.Map<Model>(model));
                return RedirectToAction("Detail", "Model", new { id = model.Id });

            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        #endregion

        #region Delete

        // Removes a record from table Models.
        public IActionResult Delete(int? id)
        {
            try
            {
                if (id.HasValue)
                {
                    modelService.Delete(id);
                        return RedirectToAction("Index", "Model");
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

        #endregion

    }
}