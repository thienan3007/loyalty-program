using System.Reflection;
using System.Text;
using LoyaltyProgram.Models;
using LoyaltyProgram.Utils;


namespace LoyaltyProgram.Services
{
    public interface BrandService 
    {
        public PagedList<Brand> GetBrands(PagingParameters pagingParameters);
        public Brand GetBrandById(int id);
        public bool AddBrand(Brand brand);
        public bool UpdateBrand(Brand brand, int id);
        public bool DeleteBrand(int id);
        public int GetBrandCount();
    }
}
