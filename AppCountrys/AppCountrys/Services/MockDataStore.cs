using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppCountrys.Models;
using AppCountrys.Models.ModelsDB;

namespace AppCountrys.Services
{
    public class MockDataStore : IDataStore<Country>
    {
        readonly List<Country> items;

        public MockDataStore()
        {
            items = new List<Country>()
            {
                
                new Country { alpha = "MX", independent = true, name = "Mexico", numeric_code = 052 },
                new Country { alpha = "AF", independent = true, name = "Afghanistan", numeric_code = 052 },
                new Country { alpha = "AL", independent = true, name = "Albania", numeric_code = 052 },
            };
        }

        public async Task<bool> AddItemAsync(Country item)
        {
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Country item)
        {
            var oldItem = items.Where((Country arg) => arg.alpha == item.alpha).FirstOrDefault();
            items.Remove(oldItem);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldItem = items.Where((Country arg) => arg.alpha == id).FirstOrDefault();
            items.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<Country> GetItemAsync(string id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.alpha == id));
        }

        public async Task<IEnumerable<Country>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }
    }
}