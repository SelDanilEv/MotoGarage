using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Dto.NavMenu
{
    public class CreateNavMenuItemDto
    {
        public string Name { get; set; }
        public string LinkController { get; set; }
        public string LinkAction { get; set; }
        public int Priority { get; set; }
        public int AccessLevel { get; set; }
    }
}
