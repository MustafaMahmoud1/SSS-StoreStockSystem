using SSS_StoreStockSystem.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SSS_StoreStockSystem.DAL.Data.Seeding
{
    public static class ContextSeed
    {
        public async static void SeedAsync(AppDBContext context)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));
            if (context.Stores.Count() == 0)
            {
                var storesData = File.ReadAllText("../SSS-StoreStockSystem.DAL/Data/Seeding/StoresData.json");
                var stores = JsonSerializer.Deserialize<List<Store>>(storesData);
                foreach (var store in stores)
                {
                    context.Set<Store>().Add(store);
                }
                await context.SaveChangesAsync();
            }
            if (context.Items.Count() == 0)
            {
                var itemsData = File.ReadAllText("../SSS-StoreStockSystem.DAL/Data/Seeding/ItemsData.json");
                var items = JsonSerializer.Deserialize<List<Item>>(itemsData);
                foreach (var item in items)
                {
                    context.Set<Item>().Add(item);
                }
                await context.SaveChangesAsync();
            }

        }

    }
}
