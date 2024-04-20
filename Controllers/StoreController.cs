using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using SSS_StoreStockSystem.BLL.Interfaces;
using SSS_StoreStockSystem.BLL.Repositories;
using SSS_StoreStockSystem.BLL.UnitOfWork;
using SSS_StoreStockSystem.DAL.Data;
using SSS_StoreStockSystem.DAL.Models;

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
            return View(stores);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Store store)
        {
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
            return View(store);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int id,Store store) 
        {
            if (id == store.Id)
                return BadRequest();
            try
            {
                _unitOfWork.StoreRepository.Update(store);
                var count = _unitOfWork.Complete();
                if (count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                //exception
            }

            return View(store);
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
                _unitOfWork.StoreRepository.Delete(store);
                var count = _unitOfWork.Complete();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {

                //Exception Handling
                return View("Index");
            }
          
        }
    }
}
