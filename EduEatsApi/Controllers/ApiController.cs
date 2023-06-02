using EduEatsApi.Models;
using EduEatsApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EduEatsApi.Controllers;


[Route("api/easydata/models/__default/sources")]
public class ApiController : Controller
{
    private readonly DatabaseContext _context;
    
    public ApiController(DatabaseContext context)
    {
        _context = context;
    }

    [Route("FavoriteProduct/get")]
    public async Task<IEnumerable<FavoriteProduct>> GetFavoriteProducts()
       => await _context.FavoriteProducts
           .Include(x => x.Product)
           .ToArrayAsync();

    [Route("Category/get")]
    public async Task<IEnumerable<Category>> GetCategorys()
        => await _context.Categorys
            .Include(x => x.Products)
            .ToArrayAsync();
    
    [Route("Client/get")]
    public async Task<IEnumerable<Client>> GetClients()
        => await _context.Clients
            .Include(x => x.FavoriteProducts)
            .Include(x => x.Basket)
            .ThenInclude(x => x.Product)
            .Include(x => x.Orders)
            .ThenInclude(x => x.Products)
            .Include(x => x.User)
            .Include(x => x.BankCards)
            .Include(x => x.Orders)
            .ThenInclude(x => x.Products)
            .ThenInclude(x => x.Product)
            .ToArrayAsync();
    
    [Route("Combo/get")]
    public async Task<IEnumerable<Combo>> GetCombos()
        => await _context.Combos
            .Include(x => x.Products)
            .ToArrayAsync();
    
    [Route("Order/get")]
    public async Task<IEnumerable<Order>> GetOrders()
        => await _context.Orders
            .Include(x => x.Products)
            .ThenInclude(x => x.Product)
            .ToArrayAsync();
    
    [Route("Product/get")]
    public async Task<IEnumerable<Product>> GetProducts()
        => await _context.Products
            .ToArrayAsync();
    
    [Route("ProductBasket/get")]
    public async Task<IEnumerable<ProductBasket>> GetProductBaskets()
        => await _context.ProductBaskets
            .Include(x => x.Product)
            .ToArrayAsync();
    
    [Route("User/get")]
    public async Task<IEnumerable<User>> GetUsers()
        => await _context.Users
            .ToArrayAsync();

}