using System.Collections.Generic;
using System.Threading.Tasks;
using HackerNewsApp.Application.Interfaces;
using HackerNewsApp.Domain.Entities;
using HackerNewsApp.Infrastructure.Interfaces;
using HackerNewsApp.Infrastructure.Repositories;

namespace HackerNewsApp.Application.Services
{
    public class ItemService : IItemService
    {
        private readonly IItemRepository _itemRepository;

        public ItemService(IItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }

        public async Task<IEnumerable<Item>> GetItemsAsync(Func<Item, IComparable> orderBy = null, Func<Item, bool> filter = null)
        {
            var items = await _itemRepository.GetItemsAsync();
            // According to filtering requirements, we're only working with stories and comments
            items = items.Where(x => x.Type == "story");

            if (filter != null)
            {
                items = items.Where(filter);
            }

            if (orderBy != null)
            {
                items = items.OrderByDescending(orderBy);
            }

            return items;
        }

        public async Task<Item> GetItemByIdAsync(int itemId)
        {
            return await _itemRepository.GetItemByIdAsync(itemId);
        }

        public async Task<IEnumerable<Item>> GetItemsByIdsAsync(IEnumerable<int> ids)
        {
            var tasks = ids.Select(id => GetItemByIdAsync(id));
            return await Task.WhenAll(tasks);
        }
    }
}