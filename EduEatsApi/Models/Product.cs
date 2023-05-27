namespace EduEatsApi.Models;

public class Product
{
    public int Id { get; set; }
    
    public string Name { get; set; }
    public string Description { get; set; }
    public int Amount { get; set; }
    public int Price { get; set; }
    public string PathToImage { get; set; }
    
    
}