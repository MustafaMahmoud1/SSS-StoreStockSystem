using Microsoft.AspNetCore.Mvc;
using SSS_StoreStockSystem.BLL.Interfaces;
using SSS_StoreStockSystem.DAL.Models;

namespace SSS_ItemStockSystem.Controllers
{
    public class ItemController : Controller
    {
        private readonly IItemRepository _itemRepository;
        public ItemController(IItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }
        public IActionResult Index()
        {
            var items = _itemRepository.GetAll();
            return View(items);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Item item)
        {
            var count = _itemRepository.Add(item);
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
            var item = _itemRepository.GetById(id.Value);
            if (item == null)
            {
                return NotFound();
            }
            return View(item);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int id, Item item)
        {
            if (id == item.Id)
                return BadRequest();
            try
            {
                var count = _itemRepository.Update(item);
                if (count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                //exception
            }

            return View(item);
        }
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (!id.HasValue)
            {
                return BadRequest();
            }
            var item = _itemRepository.GetById(id.Value);
            if (item == null)
            {
                return NotFound();
            }
            try
            {
                _itemRepository.Delete(item);
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
