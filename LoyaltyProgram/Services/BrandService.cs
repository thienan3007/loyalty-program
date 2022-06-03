using LoyaltyProgram.Models;

namespace LoyaltyProgram.Services
{
    public interface BrandService
    {
        public List<Brand> GetBrands();
        public Brand GetBrandById(int id);
        public bool AddBrand(Brand brand);
        public bool UpdateBrand(Brand brand, int id);
        public bool DeleteBrand(int id);
        public int GetBrandCount();
    }
}
