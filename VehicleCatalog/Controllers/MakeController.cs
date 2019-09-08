using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using VehicleCatalog.Models;
using VehicleCatalog.Models.MakeView;
using VehicleCatalog.Service;
using VehicleCatalog.Service.Models;
using VehicleCatalog.Service.Services.Common;
using X.PagedList;

namespace VehicleCatalog.Controllers
{
    // Make administration view (CRUD with Sorting, Filtering & Paging)
    public class MakeController : Controller
    {
        #region Fields

        private readonly IMapper mapper;
        private readonly IMakeService makeService;

        #endregion

        public MakeController(IMapper mapper, IMakeService makeService)
        {
            this.mapper = mapper;
            this.makeService = makeService;
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

            var makePage = await makeService.GetPagedMakesAsync(paging, sorting, filter);

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

            //IPagination paging = new Pagination { CurrentPage = page };

            try
            {
                if (id.HasValue)
                {
                    Make make = await makeService.GetMakeAsync(id);

                    var makeDetail = new MakeDetailModel
                    {
                        MakeDetail = mapper.Map<VehicleMakeVM>(make),
                        ModelList = await make.Models.ToPagedListAsync((page ?? 1), (7))
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
        public  IActionResult Create(VehicleMakeVM make)
        {
            try
            {
                var makeForCreation = mapper.Map<Make>(make);
                if (ModelState.IsValid)
                {
                    makeService.Create(makeForCreation);

                    return RedirectToAction("Detail", "Make", new { id = makeForCreation.Id });
                    
                }

                return View();
            }
            catch (Exception e)
            {
                return BadRequest("Something went wrong     " + e);
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
                    var make = await makeService.GetMakeAsync(id);

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
        public IActionResult Update(VehicleMakeVM make)
        {
            try
            {
                Make makeForupdate = mapper.Map<Make>(make);
                makeService.Update(makeForupdate);
                makeService.UpdateModels(makeForupdate);

                return RedirectToAction("Detail", "Make", new { id = make.Id });
            }
            catch (Exception e)
            {
                return BadRequest("Something went wrong" + e);
            }
        }

        #endregion

        #region Delete

        // Removes a record from table Makes.
        public IActionResult Delete(int? id)
        {
            try
            {
                if (id.HasValue)
                {
                    makeService.Delete(id);
                    return RedirectToAction("Index", "Make");
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