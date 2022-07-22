using LoyaltyProgram.Models;
using LoyaltyProgram.Utils;

namespace LoyaltyProgram.Services
{
    public interface OrganizationService
    {
        public PagedList<Organization> GetOrganizations(PagingParameters pagingParameters);
        public Organization GetOrganizationById(int organizationId);
        public bool AddOrganization(Organization organization);
        public bool UpdateOrganization(Organization organization, int id);
        public bool DeleteOrganization(int organizationId);
        public int GetCount();
    }
}
