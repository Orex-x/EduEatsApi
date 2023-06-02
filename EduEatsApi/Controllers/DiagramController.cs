using EduEatsApi.Services;
using EduEatsApi.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EduEatsApi.Controllers;

public class DiagramController : Controller
{
    private readonly DatabaseContext _context;
    public DiagramController(DatabaseContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> ProductDiagram()
    {
        var labels = await _context.Products.Select(x => x.Name).ToListAsync();
        
        var records = await _context.Orders
            .Include(x => x.Products)
            .ThenInclude(x => x.Product)
            .ToListAsync();


        var data = new List<int>();
        
        labels.ForEach(label =>
        {
            var count = records.Sum(x => x.Products.Where(y
                => y.Product.Name == label).Sum(z => z.Amount));
            data.Add(count);
        });
        
        var model = new ProductDiagramViewModel
        {
            BarLabels = labels,
            BarData = data
        };

        return View(model);
    }
}