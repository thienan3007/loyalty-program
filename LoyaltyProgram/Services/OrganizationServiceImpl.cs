using LoyaltyProgram.Models;

namespace LoyaltyProgram.Services
{
    public class OrganizationServiceImpl : OrganizationService
    {
        private DatabaseContext databaseContext;
        public OrganizationServiceImpl (DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
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
                if (organization.Status == true)
                {
                    organization.Status = false;
                    return databaseContext.SaveChanges() > 0;
                }
            }
            return false;
        
        }

        public int GetCount()
        {
            return databaseContext.Organizations.Count();
        }

        public Organization GetOrganizationById(int organizationId)
        {
            var organization = databaseContext.Organizations.FirstOrDefault(o => o.Id == organizationId);
            if (organization != null)
            {
                if (organization.Status == true)
                {
                    return organization;
                }
            }

            return null;
        }

        public List<Organization> GetOrganizations()
        {
            return databaseContext.Organizations.Where(o => o.Status == true).ToList();
        }

        public bool UpdateOrganization(Organization organization, int id)
        {
            var organizationDb = databaseContext.Organizations.FirstOrDefault(o => o.Id == id);

            if (organizationDb != null)
            {
                if (organizationDb.Status == true)
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

            return false;
        }
    }
}
