using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Models.Menu
{
    public class ActionLink
    {
        public string Controller { get; set; }

        public string Action { get; set; }

        public override string ToString()
        {
            var stringBuilder = new StringBuilder("/");

            if (!string.IsNullOrWhiteSpace(Controller))
            {
                stringBuilder.Append(Controller + "/");
            }
            if (!string.IsNullOrWhiteSpace(Action))
            {
                stringBuilder.Append(Action + "/");
            }

            return stringBuilder.ToString();
        }
    }
}
