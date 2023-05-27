namespace EduEatsApi.Models;

public class ClientProduct
{
    public int Id { get; set; } 
    
    public int PostsId { get; set; }
    public int TagsId { get; set; }
    public Client Client { get; set; } = null!;
    public Product Product { get; set; } = null!;
}