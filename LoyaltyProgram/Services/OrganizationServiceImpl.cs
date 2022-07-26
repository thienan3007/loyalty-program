using LoyaltyProgram.Helpers;
using LoyaltyProgram.Models;
using LoyaltyProgram.Utils;

namespace LoyaltyProgram.Services
{
    public class OrganizationServiceImpl : OrganizationService
    {
        private DatabaseContext databaseContext;
        private ISortHelper<Organization> _sortHelper;
        public OrganizationServiceImpl (DatabaseContext databaseContext, ISortHelper<Organization> sortHelper)
        {
            this.databaseContext = databaseContext;
            this._sortHelper = sortHelper;
        }
        public bool AddOrganization(Organization organization)
        {
            if (organization != null)
            {
                databaseContext.Organizations.Add(organization);
                return databaseContext.SaveChanges() > 0;
            }
            return false;
        }

        public bool DeleteOrganization(int organizationId)
        {
            var organization = databaseContext.Organizations.FirstOrDefault(o => o.Id == organizationId);
            if (organization != null)
            {
                if (organization.Status == 1)
                {
                    organization.Status = 0;
                    return databaseContext.SaveChanges() > 0;
                }
            }
            return false;
        
        }

        public int GetCount()
        {
            return databaseContext.Organizations.Where(o => o.Status == 1).Count();
        }

        public Organization GetOrganizationById(int organizationId)
        {
            var organization = databaseContext.Organizations.FirstOrDefault(o => o.Id == organizationId);
            if (organization != null)
            {
                if (organization.Status == 1)
                {
                    return organization;
                }
            }

            return null;
        }

        public PagedList<Organization> GetOrganizations(PagingParameters pagingParameters)
        {
            var filterString = pagingParameters.FilterString;
            IQueryable<Organization> organizations;
            if (filterString != null)
            {
                organizations = databaseContext.Organizations.Where(o => o.Status == 1 && o.Name.Contains(filterString));
            }
            else
            {
                organizations = databaseContext.Organizations.Where(o => o.Status == 1);
            }

            var sortedOrganizations = _sortHelper.ApplySort(organizations, pagingParameters.OrderBy);
            if (organizations != null)
            {
                return PagedList<Organization>.ToPagedList(sortedOrganizations, pagingParameters.PageNumber, pagingParameters.PageSize);
            }
            return null;
        }

        public bool UpdateOrganization(Organization organization, int id)
        {
            if (organization != null)
            {
                var organizationDb = databaseContext.Organizations.FirstOrDefault(o => o.Id == id);

                if (organizationDb != null)
                {
                    if (organizationDb.Status == 1)
                    {
                        if (organization.Name != null)
                            organizationDb.Name = organization.Name;
                        if (organization.Status != null)
                            organizationDb.Status = organization.Status;
                        if (organization.Description != null)
                            organizationDb.Description = organization.Description;

                        return databaseContext.SaveChanges() > 0;
                    }
                }
            }

            return false;
        }
    }
}
