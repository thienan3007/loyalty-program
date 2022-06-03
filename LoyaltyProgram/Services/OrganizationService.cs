using LoyaltyProgram.Models;

namespace LoyaltyProgram.Services
{
    public interface OrganizationService
    {
        public List<Organization> GetOrganizations();
        public Organization GetOrganizationById(int organizationId);
        public bool AddOrganization(Organization organization);
        public bool UpdateOrganization(Organization organization, int id);
        public bool DeleteOrganization(int organizationId);
        public int GetCount();
    }
}
