﻿using Microsoft.AspNetCore.Mvc;
using SSS_StoreStockSystem.BLL.Interfaces;
using SSS_StoreStockSystem.BLL.Repositories;
using SSS_StoreStockSystem.DAL.Data;
using SSS_StoreStockSystem.DAL.Models;

namespace SSS_StoreStockSystem.Controllers
{
    public class StoreController : Controller
    {
        private readonly IStoreRepository _storeRepository;
        public StoreController(IStoreRepository storeRepository)
        {
            _storeRepository = storeRepository;
        }
        public IActionResult Index()
        {
            var stores = _storeRepository.GetAll();
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
            var count = _storeRepository.Add(store);
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
            var store = _storeRepository.GetById(id.Value);
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
                var count = _storeRepository.Update(store);
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
            var store = _storeRepository.GetById(id.Value);
            if (store == null)
            {
                return NotFound();
            }
            try
            {
                _storeRepository.Delete(store);
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
