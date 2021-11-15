using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Enums
{
    public static class Roles
    {
        public static string Guest => "";
        public static string Client => "Client";
        public static string Employee => "Employee";
        public static string Admin => "Admin";
        public static string SuperAdmin => "SuperAdmin";
    }
}
