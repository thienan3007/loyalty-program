using LoyaltyProgram.Helpers;
using LoyaltyProgram.Models;
using LoyaltyProgram.Utils;

namespace LoyaltyProgram.Services
{
    public class BrandServiceImpl : BrandService
    {
        private readonly DatabaseContext _databaseContext;
        private ISortHelper<Brand> _sortHelper;
        public BrandServiceImpl(DatabaseContext databaseContext, ISortHelper<Brand> sortHelper)
        {
            _databaseContext = databaseContext;
            _sortHelper = sortHelper;
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
                if (brand.Status == 1)
                {
                    brand.Status = 0;
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
                if (brand.Status == 1)
                {
                    return brand;
                }
            } 
            return null;
        }

        public int GetBrandCount()
        {
            return _databaseContext.Brands.Where(b => b.Status == 1).Count();
        }

        public PagedList<Brand> GetBrands(PagingParameters pagingParameters)
        {
            var filterString = pagingParameters.FilterString;
            //PagedList<Brand> brands;
            
            //if (filterString != null)
            //{
                //brands = PagedList<Brand>.ToPagedList(_databaseContext.Brands.Where(b => b.Status == 1 && b.Name.Contains(filterString)).OrderBy(b => b.Id), pagingParameters.PageNumber, pagingParameters.PageSize);
                var brands = _databaseContext.Brands.Where(b => b.Status == 1 && b.Name.Contains(filterString));
            //}
            //else
            //{
            //    brands = PagedList<Brand>.ToPagedList(_databaseContext.Brands.Where(b => b.Status == 1).OrderBy(b => b.Id), pagingParameters.PageNumber, pagingParameters.PageSize);

            //}
            var sortedBrands = _sortHelper.ApplySort(brands, pagingParameters.OrderBy);
            if (brands != null)
            {
                return PagedList<Brand>.ToPagedList(sortedBrands, pagingParameters.PageNumber, pagingParameters.PageSize); ;
            }
            return null;
        }

        public bool UpdateBrand(Brand brand, int id)
        {
            if (brand != null)
            {
                var brandDb = GetBrandById(id);
                if (brandDb != null)
                {
                    if (brandDb.Status == 1)
                    {
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
            }

            return false;
        }
    }
}
