using Microsoft.EntityFrameworkCore;
using SSS_StoreStockSystem.BLL.Interfaces;
using SSS_StoreStockSystem.DAL.Data;
using SSS_StoreStockSystem.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace SSS_StoreStockSystem.BLL.Repositories
{
    public class StockRepository : IStockRepository
    {
        private protected readonly AppDBContext _dbContext;
        public StockRepository(AppDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IEnumerable<StoreItem> GetAll()
        {
            return _dbContext.StoreItems.Include(x => x.Item).Include(x => x.Store).ToList();
        }
        public void Alter(StoreItem storeItem)
            => _dbContext.StoreItems.Update(storeItem);
        public void Insert(StoreItem storeItem)
            => _dbContext.StoreItems.Add(storeItem);
        public void CleanUp(StoreItem storeItem)
           => _dbContext.StoreItems.Remove(storeItem);
        public StoreItem Fetch(int itemId, int storeId)
        => _dbContext.StoreItems.FirstOrDefault(x => x.ItemId == itemId && x.StoreId == storeId);
        
        public IEnumerable<Store> GetStores()
            => _dbContext.Stores.Where(s => s.IsDeleted == false).ToList();
        public IEnumerable<Item> GetItems()
            => _dbContext.Items.Where(s => s.IsDeleted == false).ToList();
        public void Log(string process, int newQuantity, int oldQuantity, int storeId, int itemId)
        {
            var macAddr =
                NetworkInterface.GetAllNetworkInterfaces()
                .Where(nic => nic.OperationalStatus == OperationalStatus.Up)
                .Select(nic => nic.GetPhysicalAddress().ToString())
                .FirstOrDefault();
            var currentDate = DateTime.Now;
            var log = new StockLog
            {
                StoreId = storeId,
                ItemId = itemId,
                Machine = macAddr,
                DateTime = currentDate,
                Process = process == "buy" ? Proccess.Purchase : Proccess.Sell,
                OldQuantity = oldQuantity,
                NewQuantity = newQuantity
            };
            _dbContext.StockLogs.Add(log);
        }
        public void Save() 
        { 
            _dbContext.SaveChanges(); 
        }
        public void Dispose()
        {
            _dbContext.Dispose();
        }



    }
}
