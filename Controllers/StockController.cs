using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata;
using SSS_StoreStockSystem.BLL.Interfaces;
using SSS_StoreStockSystem.BLL.UnitOfWork;
using SSS_StoreStockSystem.DAL.Models;
using SSS_StoreStockSystem.ViewModels;

namespace SSS_StoreStockSystem.Controllers
{
    public class StockController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IStockRepository _stockRepository;


        public StockController(IMapper mapper, IStockRepository stockRepository)
        {
            _mapper = mapper;
            _stockRepository = stockRepository;
        }
        public IActionResult Index()
        {
            var stock = _stockRepository.GetAll().ToArray();
            var mappedStock = _mapper.Map<StockViewModel[]>(stock);
            return View(mappedStock);
        }
        public IActionResult Alter()
        {
            var stock = _stockRepository.GetAll().ToArray();
            var mappedStock = _mapper.Map<StockViewModel[]>(stock);
            return View(mappedStock);
        }
        [HttpPost]
        public IActionResult Alter(string process, int advancedQuantity, int storeId, int itemId)
        {
            if (storeId == 0 && itemId == 0)
                return RedirectToAction(nameof(Index), TempData["Message"] = "Invalid Entry");
            if (advancedQuantity <= 0)
                return RedirectToAction(nameof(Index), TempData["Message"] = "Invalid Quantity");
            var storeItem = _stockRepository.Fetch(itemId, storeId);
            if (process == "buy")
            {
                if (storeItem == null)
                {
                    storeItem = new StoreItem
                    {
                        ItemId = itemId,
                        StoreId = storeId,
                        Quantity = advancedQuantity
                    };
                    _stockRepository.Insert(storeItem);
                    _stockRepository.Log("buy", advancedQuantity, 0, storeId, itemId);
                }
                else
                {
                    storeItem.Quantity = storeItem.Quantity + advancedQuantity;
                    _stockRepository.Alter(storeItem);
                    _stockRepository.Log("buy", storeItem.Quantity, storeItem.Quantity - advancedQuantity, storeId, itemId);
                }
            }
            else if (process == "sell")
            {
                if (storeItem == null)
                    return RedirectToAction(nameof(Index), TempData["Message"] = "Not Enough Quantity");
                
                storeItem.Quantity = storeItem.Quantity - advancedQuantity;
                if (storeItem.Quantity <= 0)
                {
                    _stockRepository.CleanUp(storeItem);
                    _stockRepository.Log("sell", 0, storeItem.Quantity + advancedQuantity, storeId, itemId);
                }
                else
                {
                    _stockRepository.Alter(storeItem);
                    _stockRepository.Log("sell", storeItem.Quantity, storeItem.Quantity + advancedQuantity, storeId, itemId);
                }
            }
            _stockRepository.Save();
            return View();
        }
        public int GetQuantity(int storeId, int itemId)
        {
            if (storeId == 0 )
            {
                return _stockRepository.GetAll().Where(x => x.ItemId == itemId).Sum(x => x.Quantity);
            }
            if (itemId == 0)
            {
                return 0;
            }
            var stock = _stockRepository.Fetch(itemId, storeId);
            return stock == null ? 0 : stock.Quantity;
        }
    }
}
