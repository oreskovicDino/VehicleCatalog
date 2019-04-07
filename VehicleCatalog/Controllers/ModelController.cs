using AutoMapper;
using Microsoft.AspNetCore.Mvc;
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
    public class ModelController : Controller
    {
        private readonly IVehicleService service;
        private readonly IMapper mapper;

        public ModelController(IVehicleService service, IMapper mapper)
        {
            this.service = service;
            this.mapper = mapper;
        }
        public IActionResult Index(int? page, string search = null, string sort = null)
        {
            ViewData["Title"] = "Models";

            var pageNumber = page ?? 1;

            if (!string.IsNullOrWhiteSpace(search))
            {
                var searchModelForlist = mapper.Map<IEnumerable<ModelForListDto>>(service.SearchModels(search));
                var searchMakeForList = mapper.Map<IEnumerable<MakeForListDto>>(service.SearchMakes(search));

                if (!searchMakeForList.Any())
                {
                    searchMakeForList = mapper.Map<IEnumerable<MakeForListDto>>(service.GetAllMakes());
                }

                var searchModelPage = searchModelForlist;

                switch (sort)
                {

                    case "NameDesc":
                        searchModelPage = searchModelPage.OrderByDescending(o => o.Name);
                        break;
                    case "AbrvAsc":
                        searchModelPage = searchModelPage.OrderBy(o => o.Abrv);
                        break;
                    case "AbrvDesc":
                        searchModelPage = searchModelPage.OrderByDescending(o => o.Abrv);
                        break;
                    default:
                        searchModelPage = searchModelPage.OrderBy(o => o.Name);
                        break;
                }

                searchModelPage = searchModelPage.ToPagedList(pageNumber, 5);

                var searchModel = new ModelIndexModel
                {
                    ModelList = searchModelPage,
                    MakeList = searchMakeForList,
                    SortStatus = sort,
                    SearchString = search,
                    PageNum = pageNumber
                };

                return View(searchModel);
            }

            var modelForlist = mapper.Map<IEnumerable<ModelForListDto>>(service.GetAllModels());
            var makeForList = mapper.Map<IEnumerable<MakeForListDto>>(service.GetAllMakes());

            var modelPage = modelForlist;

            switch (sort)
            {

                case "NameDesc":
                    modelPage = modelPage.OrderByDescending(o => o.Name);
                    break;
                case "AbrvAsc":
                    modelPage = modelPage.OrderBy(o => o.Abrv);
                    break;
                case "AbrvDesc":
                    modelPage = modelPage.OrderByDescending(o => o.Abrv);
                    break;
                default:
                    modelPage = modelPage.OrderBy(o => o.Name);
                    break;
            }

            modelPage = modelPage.ToPagedList(pageNumber, 5);

            var model = new ModelIndexModel
            {
                ModelList = modelPage,
                MakeList = makeForList,
                SortStatus = sort,
                SearchString = search,
                PageNum = pageNumber
            };

            return View(model);
        }

        public IActionResult Create(int id)
        {
            ViewData["Title"] = "Models | Create | ";
            var make = service.GetMakeById(id);
            var makeForModel = mapper.Map<MakeForModelDto>(make);

            var createModel = new CreateModel
            {
                Make = makeForModel,
                Abrv = makeForModel.Abrv,
                MakeId = makeForModel.Id

            };

            return View(createModel);
        }



        public async Task<IActionResult> Delete(int id)
        {
            var modelForDeletion = service.GetModelById(id);
            service.Delete(modelForDeletion);
            if (await service.SaveAll())
            {
                return RedirectToAction(nameof(Index));
            }

            return NoContent();
        }

        public IActionResult Detail(int id)
        {
            ViewData["Title"] = "Models | Detail | ";
            Model model = service.GetModelById(id);
            ModelForDetailDto ModelForDetail = mapper.Map<ModelForDetailDto>(model);
            MakeForDetailDto MakeForDetail = mapper.Map<MakeForDetailDto>(service.GetMakeById(model.MakeId));

            var detailModel = new DetailModel
            {
                ModelDetail = ModelForDetail,
                MakeDetail = MakeForDetail,
                Name = ModelForDetail.Name,
                Id = ModelForDetail.Id,
                MakeId = MakeForDetail.Id,
                Abrv = ModelForDetail.Abrv
            };

            return View(detailModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddModel(ModelForCreationDto model)
        {


            Model modelForCreation = mapper.Map<Model>(model);

            service.Create(modelForCreation);
            await service.SaveAll();
            return RedirectToAction("Detail", "Make", new { id = modelForCreation.MakeId });
        }

        [HttpPost]
        public async Task<IActionResult> AddModel2(int MakeId, string Name)
        {
            var make = service.GetMakeById(MakeId);

            var modelForCreation = new ModelForCreationDto
            {
                Name = Name,
                Abrv = make.Abrv,
                MakeId = make.Id
            };

            Model model = mapper.Map<Model>(modelForCreation);

            service.Create(model);
            await service.SaveAll();

            return RedirectToAction("Detail", "Make", new { id = make.Id });
        }

        [HttpPost]
        public async Task<IActionResult> Update(ModelForUpdateDto model)
        {
            var modelForUpdate = mapper.Map<Model>(model);

            service.Update(modelForUpdate);
            await service.SaveAll();


            return RedirectToAction("Detail", "Model", new { id = model.Id });
        }


    }
}