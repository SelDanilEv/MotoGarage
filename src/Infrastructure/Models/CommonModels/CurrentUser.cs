﻿namespace Infrastructure.Models.CommonModels
{
    public class CurrentUser : BaseModel
    {
        public string Login { get; set; }

        public string Email { get; set; }

        public string Role { get; set; }
    }
}
