namespace EduEatsApi.Models;

public class Category
{
    public int Id { get; set; }
    
    public string Name { get; set; }
    
    public string PathToImage { get; set; }
    
    public ICollection<Product> Products { get; set; }
    
}