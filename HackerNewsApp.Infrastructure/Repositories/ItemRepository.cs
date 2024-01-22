using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using HackerNewsApp.Infrastructure.Interfaces;
using HackerNewsApp.Domain.Entities;

namespace HackerNewsApp.Infrastructure.Repositories
{
    public class ItemRepository : IItemRepository
    {
        private readonly HttpClient _httpClient;

        public ItemRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<Item>> GetItemsAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<int[]>("https://hacker-news.firebaseio.com/v0/topstories.json");

            if (response == null)
            {
                return Enumerable.Empty<Item>();
            }

            var items = new List<Item>();

            foreach (var itemId in response.Take(100)) // Adjust the number as needed
            {
                var item = await _httpClient.GetFromJsonAsync<Item>($"https://hacker-news.firebaseio.com/v0/item/{itemId}.json");

                if (item != null)
                {
                    // Convert Unix timestamp to DateTimeOffset
                    if (item.Time.HasValue)
                    {
                        item.Time = DateTimeOffset.FromUnixTimeSeconds(item.Time.Value.ToUnixTimeSeconds());
                    }

                    items.Add(item);
                }
            }

            return items;
        }

        public async Task<Item> GetItemByIdAsync(int itemId)
        {
            var response = await _httpClient.GetFromJsonAsync<Item>($"https://hacker-news.firebaseio.com/v0/item/{itemId}.json");

            return response;
        }

        public async Task<IEnumerable<Item>> GetItemsByIdsAsync(IEnumerable<int> ids)
        {
            var tasks = ids.Select(id => GetItemByIdAsync(id));
            return await Task.WhenAll(tasks);
        }
    }
}