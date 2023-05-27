namespace EduEatsApi.Models;

public class Combo
{
    public int Id { get; set; }
    
    public string Name { get; set; }
    
    public string PathToImage { get; set; }
    
    public int Price { get; set; }
    
    public ICollection<Product> Products { get; set; }
}