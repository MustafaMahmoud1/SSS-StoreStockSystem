using Microsoft.AspNetCore.Mvc;

namespace SSS_StoreStockSystem.Controllers
{
    public class StockController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
