using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using VehicleCatalog.Models;
using VehicleCatalog.Models.ModelView;
using VehicleCatalog.Service;
using VehicleCatalog.Service.Models;
using X.PagedList;

namespace VehicleCatalog.Controllers
{
    //Model administration view (CRUD with Sorting, Filtering & Paging)
    public class ModelController : Controller
    {
        private readonly IUnitOfWork unit;

        //private readonly IModelService service;
        //private readonly IMakeService makeService;


        private readonly IMapper mapper;

        public ModelController(IUnitOfWork unit, IMapper mapper)
        {
            //this.service = service;
            //this.makeService = makeService;
            this.unit = unit;
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

            IPagedList<Model> modelPage = await unit.Models.GetAll(paging, sorting, filter);

            var model = new ModelIndexModel
            {
                ModelList = modelPage,
                SortStatus = sort,
                SearchString = search,
                Pagination = paging
            };
            Dis();
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
                    Model model = await unit.Models.GetById(id);
                    //Make make = await unit.Makes.GetById(model.MakeId);
                    //var modelDetail = model.ModelByID;

                    DetailModel detailModel = new DetailModel
                    {
                        ModelDetail = mapper.Map<VehicleModelVM>(model),
                        //MakeDetail = mapper.Map<MakeForDetailDto>(make),
                        MakeDetail = await unit.Makes.GetById(model.MakeId),
                        Id = model.Id,
                        Abrv = model.Abrv,
                        MakeId = model.MakeId
                    };
                    Dis();
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

            IPagedList<Make> makePage = await unit.Makes.GetAll(paging, sorting, filter);
            var make = new ModelIndexModel
            {
                MakeList = makePage,
                Pagination = paging,
                SearchString = search,
                SortStatus = sort
            };
            Dis();
            return View(make);
        }


        // (Create Page) selects a single record from the Makes table. 
        // GET: /Model/Create?makeId
        public async Task<IActionResult> Create(int? makeId)
        {
            ViewData["Title"] = "Models | Create | ";

            Make make = await unit.Makes.GetById(makeId);
            var makeForModel = mapper.Map<VehicleMakeVM>(make);

            var createModel = new CreateModel
            {
                Abrv = makeForModel.Abrv,
                MakeId = makeForModel.Id,
                DetailMakeName = makeForModel.Name
            };

            Dis();
            return View(createModel);
        }

        // Adds a record to table Models.
        //

        [HttpPost]
        public async Task<IActionResult> Create(VehicleModelVM model)
        {
            try
            {
                var modelForCreation = mapper.Map<Model>(model);
                if (ModelState.IsValid)
                {

                    await unit.Models.Add(modelForCreation);
                    Dis();
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
        public async Task<IActionResult> Update(VehicleModelVM model)
        {
            try
            {
                if (await unit.Models.Update(mapper.Map<Model>(model)))
                {
                    Dis();
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
                    Model modelForDeletion = await unit.Models.GetById(id);

                    if (await unit.Models.Remove(modelForDeletion))
                    {
                        Dis();
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

        private void Dis() { unit.Dispose(); }
    }
}