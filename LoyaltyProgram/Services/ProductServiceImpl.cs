using LoyaltyProgram.Models;
using Microsoft.EntityFrameworkCore;

namespace LoyaltyProgram.Services
{
    public class ProductServiceImpl : ProductService
    {
        private readonly DatabaseContext _databaseContext;
        public ProductServiceImpl(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public List<Product> FindAll()

        {
            return _databaseContext.Products.ToList();
        }
    }
}
