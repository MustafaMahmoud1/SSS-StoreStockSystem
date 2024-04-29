using SSS_StoreStockSystem.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSS_StoreStockSystem.BLL.Interfaces
{
    public interface IStockRepository : IDisposable
    {
        public IEnumerable<StoreItem> GetAll();
        public void Alter(StoreItem storeItem);
        public void Insert(StoreItem storeItem);
        public void CleanUp(StoreItem storeItem);
        public StoreItem Fetch(int itemId, int storeId);
        public void Save();
        public IEnumerable<Store> GetStores();
        public IEnumerable<Item> GetItems();
        public void Log(string process, int newQuantity, int oldQuantity, int storeId, int itemId);

    }
}
