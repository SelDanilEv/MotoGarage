using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Dto.NavMenu
{
    public class NavMenuItemDto
    {
        public Guid Id { get; set; }
        public string DisplayName { get; set; }
        public string Href { get; set; }
    }
}
