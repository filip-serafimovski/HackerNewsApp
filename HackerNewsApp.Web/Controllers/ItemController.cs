using System.Threading.Tasks;
using HackerNewsApp.Application.Interfaces;
using HackerNewsApp.Application.Services;
using HackerNewsApp.Domain.Entities;
using HackerNewsApp.Web.Models;
using Microsoft.AspNetCore.Mvc;

public class ItemController : Controller
{
    private readonly IItemService _itemService;

    public ItemController(IItemService itemService)
    {
        _itemService = itemService;
    }

    public async Task<IActionResult> Index(string filter, string search)
    {
        IEnumerable<Item> items;

        switch (filter?.ToLower())
        {
            case "all":
                items = await _itemService.GetItemsAsync(orderBy: x => x.Time);
                break;
            case "hot":
                items = await _itemService.GetItemsAsync(orderBy: x => x.Score);
                break;
            case "show hn":
                items = await _itemService.GetItemsAsync(filter: x => x.Title.Contains("show hn", StringComparison.OrdinalIgnoreCase));
                break;
            case "ask hn":
                items = await _itemService.GetItemsAsync(filter: x => x.Title.Contains("ask hn", StringComparison.OrdinalIgnoreCase));
                break;
            default:
                items = await _itemService.GetItemsAsync();
                break;
        }

        if (!string.IsNullOrEmpty(search))
        {
            items = items.Where(x => x.Title.Contains(search, StringComparison.OrdinalIgnoreCase));
        }

        return View(items);
    }

    public async Task<IActionResult> Details(int id)
    {
        var item = await _itemService.GetItemByIdAsync(id);

        if (item == null)
        {
            return NotFound();
        }

        // Get additional details for Kids
        var childItems = await _itemService.GetItemsByIdsAsync(item.Kids ?? Array.Empty<int>());

        var viewModel = new ItemDetailsViewModel
        {
            Item = item,
            ChildItems = childItems
        };

        return View(viewModel);
    }
}
