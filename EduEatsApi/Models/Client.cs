namespace EduEatsApi.Models;

public class Client
{
    public int Id { get; set; } 
    
    
    public int UserId { get; set; }
    public User User { get; set; }
    
    public int Balance { get; set; }

    public ICollection<FavoriteProduct> FavoriteProducts { get; set; }
    public ICollection<ProductBasket> Basket { get; set; }
    public ICollection<Order> Orders { get; set; }
    public ICollection<BankCard> BankCards { get; set; }
}