using LoyaltyProgram.Models;

namespace LoyaltyProgram.Services
{
    public class BrandServiceImpl : BrandService
    {
        private readonly DatabaseContext _databaseContext;
        public BrandServiceImpl(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }
        public List<Brand> GetBrands()
        {
            return _databaseContext.Brands.ToList();
        }
    }
}
