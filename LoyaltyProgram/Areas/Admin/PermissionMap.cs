using LoyaltyProgram.Auth;

namespace LoyaltyProgram.Areas.Admin
{
    enum Permission
    {
        CreateConditionRule = 1,
        ReadConditionRule = 2,
        UpdateConditionRule = 3,
        DeleteConditionRule = 4
    }
    static class PermissionMap
    {
        public static IEnumerable<Permission> GetPermissions(Role role)
        {
            switch(role)
            {
                //Requirement: user can only read the condition rule
                case Role.User:
                    yield return Permission.ReadConditionRule;
                    break;
                //Requirement: admin can do everthing
                case Role.Admin:
                    yield return Permission.UpdateConditionRule;
                    yield return Permission.CreateConditionRule;
                    yield return Permission.DeleteConditionRule;                     
                    break;
            }
        }
    }
}
