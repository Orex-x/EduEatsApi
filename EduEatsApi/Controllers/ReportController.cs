using System.ComponentModel;
using EduEatsApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace EduEatsApi.Controllers;

public class ReportController : Controller
{
    private readonly DatabaseContext _context;
    public ReportController(DatabaseContext context)
    {
        _context = context;
    }


    public IActionResult ImportToExcel()
    {
        return null;
    }
    
    public IActionResult ImportToPdf()
    {
        return null;
    }
    
    public IActionResult Print()
    {
        return null;
    }
}