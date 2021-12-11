using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Dto.NavMenu
{
    public class CreateNavMenuItemDto
    {
        public string Name { get; set; } = "default";
        public string LinkController { get; set; } = "NotFound";
        public string LinkAction { get; set; } = "";
        public int Priority { get; set; }
        public int AccessLevel { get; set; }
    }
}
