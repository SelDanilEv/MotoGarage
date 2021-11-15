using Microsoft.AspNetCore.Authorization;

namespace Infrastructure.Attributes
{
    public class AuthorizeAdmin : AuthorizeAttribute
    {
        public AuthorizeAdmin()
        {
            Roles = Enums.Roles.Admin;
        }
    }

    public class AuthorizeEmployee : AuthorizeAttribute
    {
        public AuthorizeEmployee()
        {
            Roles = $"{Enums.Roles.Admin},{Enums.Roles.Employee}";
        }
    }

    public class AuthorizeClient : AuthorizeAttribute
    {
        public AuthorizeClient()
        {
            Roles = $"{Enums.Roles.Admin},{Enums.Roles.Employee},{Enums.Roles.Client}";
        }
    }
}
