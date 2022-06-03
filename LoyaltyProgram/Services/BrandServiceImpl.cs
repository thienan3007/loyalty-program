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

        public bool AddBrand(Brand brand)
        {

            _databaseContext.Brands.Add(brand);
            return _databaseContext.SaveChanges() > 0;
        }

        public bool DeleteBrand(int id)
        {
            var brand = _databaseContext.Brands.FirstOrDefault(b => b.Id == id);
            if (brand != null)
            {
                if (brand.Status == true)
                {
                    brand.Status = false;
                    return _databaseContext.SaveChanges() > 0;
                }
            }
            return false;
        }

        public Brand GetBrandById(int id)
        {
            var brand = _databaseContext.Brands.FirstOrDefault(b => b.Id == id);
            if (brand != null)
            {
                if (brand.Status == true)
                {
                    return brand;
                }
            } 
            return null;
        }

        public int GetBrandCount()
        {
            return _databaseContext.Brands.Where(b => b.Status == true).Count();
        }

        public List<Brand> GetBrands()
        {
            return _databaseContext.Brands.Where(b => b.Status == true).ToList();
        }

        public bool UpdateBrand(Brand brand, int id)
        {
            var brandDb = GetBrandById(id);
            if (brandDb != null)
            {
                if (brandDb.Status == true)
                {
                    //brandDb.Status = brand.Status;
                    //brandDb.Name = brand.Name;
                    //brandDb.Description = brand.Description;
                    //brandDb.OrganizationId = brand.OrganizationId;

                    if (brand.Name != null)
                        brandDb.Name = brand.Name;
                    if (brand.Status != null)
                        brandDb.Status = brand.Status;
                    if (brand.Description != null)
                        brandDb.Description = brand.Description;
                    if (brand.OrganizationId != null)
                        brandDb.OrganizationId = brand.OrganizationId;

                    return _databaseContext.SaveChanges() > 0;
                }
            }

            return false;
        }
    }
}
