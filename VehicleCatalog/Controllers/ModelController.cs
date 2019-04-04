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
        public IActionResult Index(string search = null)
        {
            ViewData["Title"] = "Models";

            if (!string.IsNullOrWhiteSpace(search))
            {
                var searchModelForlist = mapper.Map<IEnumerable<ModelForListDto>>(service.SearchModels(search));
                var searchMakeForList = mapper.Map<IEnumerable<MakeForListDto>>(service.SearchMakes(search));

                if (!searchMakeForList.Any())
                {
                    searchMakeForList = mapper.Map<IEnumerable<MakeForListDto>>(service.GetAllMakes());
                }
                var searchModel = new ModelIndexModel
                {
                    ModelList = searchModelForlist,
                    MakeList = searchMakeForList
                };

                return View(searchModel);
            }

            var modelForlist = mapper.Map<IEnumerable<ModelForListDto>>(service.GetAllModels());
            var makeForList = mapper.Map<IEnumerable<MakeForListDto>>(service.GetAllMakes());
            var model = new ModelIndexModel
            {
                ModelList = modelForlist,
                MakeList = makeForList
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
                Name = model.Name,
                Id = model.Id,
                MakeId = MakeForDetail.Id
            };

            return View(detailModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddModel(ModelForCreationDto model)
        {


            Model modelForCreation = mapper.Map<Model>(model);

            await service.CreateModel(modelForCreation);

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

            await service.CreateModel(model);

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