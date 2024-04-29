using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using SSS_StoreStockSystem.BLL.Interfaces;
using SSS_StoreStockSystem.BLL.Repositories;
using SSS_StoreStockSystem.BLL.UnitOfWork;
using SSS_StoreStockSystem.DAL.Data;
using SSS_StoreStockSystem.DAL.Models;
using SSS_StoreStockSystem.Helpers;
using SSS_StoreStockSystem.ViewModels;
using System.Reflection.Metadata;

namespace SSS_StoreStockSystem.Controllers
{
    public class StoreController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;


        public StoreController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            var stores = _unitOfWork.StoreRepository.GetAll().ToArray();
            var mappedStores = _mapper.Map<Store[], StoreViewModel[]>(stores);
            return View(mappedStores);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(StoreViewModel storeVM)
        {
            storeVM.ImageName = DocumentSettings.UploadFile(storeVM.Image, "images");
            var store = _mapper.Map<Store>(storeVM);
            _unitOfWork.StoreRepository.Add(store);
            var count = _unitOfWork.Complete();
            if (count > 0)
            {
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (!id.HasValue)
            {
                return BadRequest();
            }
            var store = _unitOfWork.StoreRepository.GetById(id.Value);
            if (store == null)
            {
                return NotFound();
            }
            var mappedStore = _mapper.Map<StoreViewModel>(store);
            return View(mappedStore);

        }
        [HttpPost]
        public IActionResult Edit(StoreViewModel storeVM) 
        {
            if (storeVM.Image != null)
            {
                DocumentSettings.DeleteFile(storeVM.ImageName, "images");
                storeVM.ImageName = DocumentSettings.UploadFile(storeVM.Image, "images");
            }
            var mappedStore = _mapper.Map<Store>(storeVM);
            try
            {
                _unitOfWork.StoreRepository.Update(mappedStore);
                var count = _unitOfWork.Complete();
                if (count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction(nameof(Index));
            }

            return View(mappedStore);
        }
        [HttpGet]
        public IActionResult Delete(int? id) 
        {
            if (!id.HasValue)
            {
                return BadRequest();
            }
            var store = _unitOfWork.StoreRepository.GetById(id.Value);
            if (store == null)
            {
                return NotFound();
            }
            try
            {
                store.IsDeleted = true;
                _unitOfWork.StoreRepository.Update(store);
                var count = _unitOfWork.Complete();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {

                TempData["Error"] = ex.Message;
                return RedirectToAction(nameof(Index));
            }

          
        }
    }
}
