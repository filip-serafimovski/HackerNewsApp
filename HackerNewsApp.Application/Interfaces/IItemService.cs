using System.Collections.Generic;
using System.Threading.Tasks;
using HackerNewsApp.Domain.Entities;

namespace HackerNewsApp.Application.Interfaces
{
    public interface IItemService
    {
        Task<IEnumerable<Item>> GetItemsAsync(Func<Item, IComparable> orderBy = null, Func<Item, bool> filter = null);
        Task<Item> GetItemByIdAsync(int itemId);
        Task<IEnumerable<Item>> GetItemsByIdsAsync(IEnumerable<int> ids);
        // Add other methods for item filtering, searching, and details
    }
}