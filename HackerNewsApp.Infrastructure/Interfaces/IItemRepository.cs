using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HackerNewsApp.Domain.Entities;

namespace HackerNewsApp.Infrastructure.Interfaces
{
    public interface IItemRepository
    {
        Task<IEnumerable<Item>> GetItemsAsync();
        Task<Item> GetItemByIdAsync(int itemId);
        Task<IEnumerable<Item>> GetItemsByIdsAsync(IEnumerable<int> ids);
    }
}
