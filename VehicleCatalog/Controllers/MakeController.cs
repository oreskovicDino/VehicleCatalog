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
        private readonly IUnitOfWork unit;
        private readonly IMapper mapper;



        public MakeController(IUnitOfWork unit, IMapper mapper)
        {
            //this.service = service;
            this.unit = unit;
            this.mapper = mapper;
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

            IPagedList<Make> makePage = await unit.Makes.GetAll(paging, sorting, filter);


            var make = new MakeIndexModel
            {
                MakeList = makePage,
                SortStatus = sort,
                SearchString = search,
                Pagination = new Pagination() { CurrentPage = page }
            };

            DisposeOf();
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
                    Make make = await unit.Makes.GetById(id);
                    IPagedList<Model> models = await unit.Models.GetModelsByMake(make, paging);


                    var makeDetail = new MakeDetailModel
                    {
                        MakeDetail = mapper.Map<VehicleMakeVM>(make),
                        ModelList = models
                    };
                    DisposeOf();
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
                if (ModelState.IsValid)
                {
                    var makeForCreation = mapper.Map<Make>(make);
                    unit.Makes.Add(makeForCreation);
                    await unit.Commit();
                    DisposeOf();
                    return RedirectToAction("Detail", "Make", new { id = makeForCreation.Id });
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
                    var make = await unit.Makes.GetById(id);
                    var makeDetail = new MakeUpdateModel
                    {
                        Id = make.Id,
                        Name = make.Name,
                        Abrv = make.Abrv
                    };

                    DisposeOf();
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
                unit.Makes.Update(mapper.Map<Make>(make));
                if (await unit.Commit())
                {
                    DisposeOf();
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
        //
        public async Task<IActionResult> Delete(int? id)
        {
            try
            {
                if (id.HasValue)
                {
                    Make make = await unit.Makes.GetById(id);

                    unit.Makes.Remove(make);

                    if (await unit.Commit())
                    {
                        DisposeOf();
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

        private void DisposeOf() { unit.Dispose(); }
    }
}