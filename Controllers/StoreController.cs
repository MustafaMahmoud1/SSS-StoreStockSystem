using Microsoft.AspNetCore.Mvc;
using SSS_StoreStockSystem.BLL.Interfaces;
using SSS_StoreStockSystem.BLL.Repositories;
using SSS_StoreStockSystem.DAL.Data;

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
    }
}
