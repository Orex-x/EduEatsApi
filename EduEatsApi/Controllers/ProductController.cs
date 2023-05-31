using System.Net;
using System.Web;
using EduEatsApi.Services;
using EduEatsApi.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ModernHttpClient;
using Newtonsoft.Json;
using RestSharp;

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
    
    [HttpPost]
    public async Task<IActionResult> GetImages(string productName)
    {
        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

        
        string translatedText = TranslateText(productName);

        var client = new RestClient("https://api.unsplash.com/search");
        var request = new RestRequest();

        request.Method = Method.Get;
        
        request.AddParameter("query", translatedText);
        request.AddParameter("client_id", "las2Eppqp3JEnAxqRYEdStBPwyl5w15RIX60c9HTGz0");
        
        var response = await client.ExecuteAsync(request);

        var imageUrls = ProcessApiResponse(response.Content);

        return Ok(imageUrls);
    }
    
    private List<string> ProcessApiResponse(string apiResponse)
    {
        var data = JsonConvert.DeserializeObject<dynamic>(apiResponse);
        var imageUrls = new List<string>();

        int i = 0;
        foreach (var result in data.photos.results)
        {
            if(i == 10) break;
            
            imageUrls.Add(result.urls.small.ToString());

            i++;
        }

        return imageUrls;
    }
    
    static string TranslateText(string text)
    {
        string url = @$"https://translate.googleapis.com/translate_a/single?client=gtx&sl=ru&tl=en&dt=t&q={text}";

        using (WebClient webClient = new WebClient())
        {
            string response = webClient.DownloadString(url);
            string translatedText = ParseTranslationResponse(response);
            return translatedText;
        }
    }

    static string ParseTranslationResponse(string response)
    {
        // Пример простого разбора ответа от сервиса перевода
        string[] parts = response.Split(new char[] { '\"' }, StringSplitOptions.RemoveEmptyEntries);
        return parts[1];
    }

}