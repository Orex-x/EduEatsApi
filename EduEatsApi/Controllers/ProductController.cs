using EduEatsApi.Services;
using EduEatsApi.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EduEatsApi.Controllers;

[Authorize]
public class ProductController : Controller
{
    private readonly DatabaseContext _context;

    public ProductController(DatabaseContext context)
    {
        _context = context;
    }

    public IActionResult Products()
    {
        var list = _context.Products.ToList();
        
        
        var model = new ProductsViewModel
        {
            Products = list
        };

        return View(model);
    }

    public IActionResult AddProduct() => View();
    
    [HttpPost]
    public async Task<IActionResult> AddProduct(AddProductViewModel model)
    {
        if (!ModelState.IsValid) return View(model);
        
        await _context.Products.AddAsync(model.Product);
        await _context.SaveChangesAsync();
        
        return RedirectToAction("Products", "Product");
    }
}