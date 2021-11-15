using Infrastructure.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Helpers
{
    public static class RolesHelper
    {

        public static Dictionary<string, int> AccessLevelByRoleName = new Dictionary<string, int>()
        {
            {Roles.Guest, 0 },
            {Roles.Client, 1 },
            {Roles.Employee, 2 },
            {Roles.Admin, 3 },
            {Roles.SuperAdmin, 4 }
        };

        public static int GetAccessLevel(string roleName)
        {
            if (AccessLevelByRoleName.TryGetValue(roleName, out int value))
            {
                return value;
            }
            else
            {
                return 0;
            }
        }

        public static bool IsResourceAvailableForRole(string roleName, int accessLevel)
        {
            return GetAccessLevel(roleName) >= accessLevel;
        }
    }
}
