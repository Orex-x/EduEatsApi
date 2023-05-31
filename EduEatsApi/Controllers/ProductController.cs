using System.Collections;
using System.Net;
using System.Text;
using System.Web;
using EduEatsApi.Models;
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

    public async Task<CsvResult> GetData()
    {
        var stringBuilder = new StringBuilder();

        var list = await _context.Products.ToListAsync();
        
        list.ForEach(x =>
        {
            stringBuilder.AppendLine($"{x.Name},{x.Description},{x.Amount},{x.Price},{x.PathToImage}");
        });

        var result = new CsvResult(stringBuilder.ToString(), "Products.cvs");
        
        return result;
    }

    public IActionResult UploadData() => View(new UploadDataViewModel());
    
    
    [HttpPost]
    public async Task<IActionResult> UploadData(UploadDataViewModel model)
    {
        if (model.FileUpload?.FormFile != null && model.FileUpload.FormFile.Length > 0)
        {
            if (!ModelState.IsValid) return View(model);

            var list = new List<string>();
            using (var sr = new StreamReader(model.FileUpload.FormFile.OpenReadStream(), Encoding.UTF8))
            {
                string line;
                while ((line = sr.ReadLine()) != null) list.Add(line);
            }
            
            list.ForEach(async x =>
            {
                try
                {
                    var array = x.Split(",");
                    var product = new Product
                    {
                        Name = array[0],
                        Description = array[1],
                        Amount = Convert.ToInt32(array[2]),
                        Price = Convert.ToInt32(array[3]),
                        PathToImage = array[4]
                    };
                    await _context.Products.AddAsync(product);
                }
                catch (Exception)
                {
                }
            });
            await _context.SaveChangesAsync();
        }
        
        return View(model);
    }
}