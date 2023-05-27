
namespace EduEatsApi.Models
{
    public class FavoriteProduct
    {
        public int Id { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
