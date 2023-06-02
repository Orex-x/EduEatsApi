namespace EduEatsApi.Models;

public class Order
{
    public int Id { get; set; }
    
    public OrderStatus OrderStatus { get; set; }
    public ICollection<ProductBasket> Products { get; set; }
    
    public DateTime DateTime { get; set; }
    
    public int Price { get; set; }
}