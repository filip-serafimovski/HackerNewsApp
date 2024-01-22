using HackerNewsApp.Application.Interfaces;
using HackerNewsApp.Application.Services;
using HackerNewsApp.Infrastructure.Interfaces;
using HackerNewsApp.Infrastructure.Repositories;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddTransient<IItemService, ItemService>();
        services.AddTransient<IItemRepository, ItemRepository>();
        services.AddTransient<ItemController>();
    }
}