using HackerNewsApp.Domain.Entities;

namespace HackerNewsApp.Web.Models
{
    public class ItemDetailsViewModel
    {
        public Item Item { get; set; }
        public IEnumerable<Item> ChildItems { get; set; }
    }
}
