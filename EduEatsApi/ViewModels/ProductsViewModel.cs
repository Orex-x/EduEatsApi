using EduEatsApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace EduEatsApi.ViewModels;

public class ProductsViewModel
{
    public List<Product> Products { get; set; }
}