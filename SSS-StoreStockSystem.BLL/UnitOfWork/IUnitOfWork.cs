using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SSS_StoreStockSystem.BLL.Interfaces;

namespace SSS_StoreStockSystem.BLL.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        public IItemRepository ItemRepository { get; }
        public IStoreRepository StoreRepository { get; }
        int Complete();


    }
}
