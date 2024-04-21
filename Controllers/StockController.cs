using AutoMapper;
using Microsoft.AspNetCore.Mvc;
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
            return View();
        }
        //[HttpPost]
        //public IActionResult Alter()
        //{
        //    return View();
        //}
    }
}
