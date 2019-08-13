using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using VehicleCatalog.Models;
using VehicleCatalog.Models.MakeView;
using VehicleCatalog.Service;
using VehicleCatalog.Service.Models;
using X.PagedList;

namespace VehicleCatalog.Controllers
{
    // Make administration view (CRUD with Sorting, Filtering & Paging)
    public class MakeController : Controller
    {
        #region Fields

        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;

        #endregion

        public MakeController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        #region Index

        // (Index Page) Selects all records from the Makes table. 
        // GET: /Make/Index
        public async Task<IActionResult> Index(int? page, string search, string sort = null)
        {
            ViewData["Title"] = "Manufacturers";

            IPagination paging = new Pagination() { CurrentPage = page ?? 1 };
            ISort sorting = new Sort() { Sorting = sort };
            IFilter filter = new Filter() { FilterString = search };

            //IPagedList<VehicleMakeVM> makePage = mapper.Map<IPagedList<VehicleMakeVM>>(pp);
            var makePage = await unitOfWork.MakeService.GetMakesAsync(paging, sorting, filter);

            var make = new MakeIndexModel
            {
                MakeList = makePage,
                SortStatus = sort,
                SearchString = search,
                Pagination = paging
            };

            return View(make);
        }

        #endregion

        #region Detail

        // (Detail Page) Selects a single record from the Makes table. 
        // GET: /Make/Detail/id
        public async Task<IActionResult> Detail(int? page, int? id)
        {
            ViewData["Title"] = "Manufacturer | Detail | ";

            IPagination paging = new Pagination { CurrentPage = page };

            try
            {
                if (id.HasValue)
                {
                    Make make = await unitOfWork.MakeService.GetMakeAsync(id);
                    IPagedList<Model> models = await unitOfWork.MakeService.GetModelsByMake(make, paging);


                    var makeDetail = new MakeDetailModel
                    {
                        MakeDetail = mapper.Map<VehicleMakeVM>(make),
                        ModelList = models
                    };
                    return View(makeDetail);
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

        #region Create

        // (Create Page)
        // GET: /Make/Create
        public IActionResult Create()
        {
            return View();
        }

        // Adds a record to table Makes.
        //
        [HttpPost]
        public async Task<IActionResult> Create(VehicleMakeVM make)
        {
            try
            {
                var makeForCreation = mapper.Map<Make>(make);
                if (ModelState.IsValid)
                {
                    unitOfWork.MakeService.Create(makeForCreation);
                    if (await unitOfWork.Commit())
                    {
                        return RedirectToAction("Detail", "Make", new { id = makeForCreation.Id });
                    }
                    else
                    {
                        return BadRequest("Something went wrong");
                    }
                }

                return View();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        #endregion

        #region Update

        // (Update Page) selects a single record from the Makes table.
        // GET: /Make/Update/id
        public async Task<IActionResult> Update(int? id)
        {
            ViewData["Title"] = "Manufacturer | Edit ";

            try
            {
                if (id.HasValue)
                {
                    var make = await unitOfWork.MakeService.GetMakeAsync(id);

                    var makeDetail = new MakeUpdateModel
                    {
                        Id = make.Id,
                        Name = make.Name,
                        Abrv = make.Abrv,
                    };
                    return View(makeDetail);

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

        // Updates a record from table Makes.
        //
        [HttpPost]
        public async Task<IActionResult> Update(VehicleMakeVM make)
        {
            try
            {
                Make makeForupdate = mapper.Map<Make>(make);
                unitOfWork.MakeService.Update(makeForupdate);
                unitOfWork.ModelService.UpdateModels(makeForupdate);

                if (await unitOfWork.Commit())
                {
                    return RedirectToAction("Detail", "Make", new { id = make.Id });
                }

                return BadRequest("Something went wrong");

            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        #endregion

        #region Delete

        // Removes a record from table Makes.
        public async Task<IActionResult> Delete(int? id)
        {
            try
            {
                if (id.HasValue)
                {
                    unitOfWork.MakeService.Delete(id);

                    if (await unitOfWork.Commit())
                    {
                        return RedirectToAction("Index", "Make");
                    }
                    else
                    {
                        return BadRequest("Something went wrong, we couldn't delete this manufacturer");
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

        #endregion

    }
}