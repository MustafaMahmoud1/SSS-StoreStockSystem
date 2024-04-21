using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SSS_StoreStockSystem.BLL.UnitOfWork;
using SSS_StoreStockSystem.DAL.Models;
using SSS_StoreStockSystem.ViewModels;

namespace SSS_ItemStockSystem.Controllers
{
    public class ItemController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;


        public ItemController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            var items = _unitOfWork.ItemRepository.GetAll().ToArray();
            var mappedItems = _mapper.Map<Item[], ItemViewModel[]>(items);
            return View(mappedItems);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ItemViewModel itemVM)
        {
            var item = _mapper.Map<Item>(itemVM);
            _unitOfWork.ItemRepository.Add(item);
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
            var item = _unitOfWork.ItemRepository.GetById(id.Value);
            if (item == null)
            {
                return NotFound();
            }
            var mappedItem = _mapper.Map<ItemViewModel>(item);
            return View(mappedItem);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int id, ItemViewModel itemVM)
        {
            if (id == itemVM.Id)
                return BadRequest();
            var mappedItem = _mapper.Map<Item>(itemVM);
            try
            {
                _unitOfWork.ItemRepository.Update(mappedItem);
                var count = _unitOfWork.Complete();
                if (count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return View("Index");
            }

            return View(mappedItem);
        }
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (!id.HasValue)
            {
                return BadRequest();
            }
            var item = _unitOfWork.ItemRepository.GetById(id.Value);
            if (item == null)
            {
                return NotFound();
            }
            try
            {
                _unitOfWork.ItemRepository.Delete(item);
                var count = _unitOfWork.Complete();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {

                TempData["Error"] = ex.Message;
                return View("Index");
            }

        }
    }
}
