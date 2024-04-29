using Microsoft.EntityFrameworkCore;
using SSS_StoreStockSystem.DAL.Data;
using SSS_StoreStockSystem.DAL.Models;
using SSS_TStockSystem.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSS_StoreStockSystem.BLL.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseModel
    {
        private protected readonly AppDBContext _dbContext;
        public GenericRepository(AppDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IEnumerable<T> GetAll()
        {
            //We use this method to show all items only, hence, AsNoTracking is used here
            return _dbContext.Set<T>().Where(t => t.IsDeleted == false).AsNoTracking();
        }

        public T GetById(int id)
        {
            // Using Find function to search for the object in the local memory first, if not found, hit the database
            //this could potientialy save us a trip to the database
            return _dbContext.Find<T>(id);

        }
        public void Add(T entity) 
            => _dbContext.Set<T>().Add(entity);

        public void Update(T entity)
            =>_dbContext.Set<T>().Update(entity);

        public void Delete(T entity)
           => _dbContext.Set<T>().Remove(entity);
        


    }
}
