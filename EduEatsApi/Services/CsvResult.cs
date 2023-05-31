using System.Text;
using Microsoft.AspNetCore.Mvc;

namespace EduEatsApi.Services;

public class CsvResult : IActionResult
{
    private readonly string _csvData;
    private readonly string _fileName;

    public CsvResult(string csvData, string fileName)
    {
        _csvData = csvData;
        _fileName = fileName;
    }
    
    public async Task ExecuteResultAsync(ActionContext context)
    {
        var response = context.HttpContext.Response;
        response.ContentType = "text/csv";
        response.Headers.Add("Content-Disposition", "attachment; filename=" + _fileName);
        
        await context.HttpContext.Response.WriteAsync(_csvData);
    }
}