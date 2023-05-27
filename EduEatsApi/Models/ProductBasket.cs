namespace EduEatsApi.Models;

public class ProductBasket
{
    public int Id { get; set; }
    
    
    public int ProductId { get; set; }
    public Product? Product { get; set; }
    
    public int Amount { get; set; }
    
    public int Discount { get; set; } //для хранения скидки
}