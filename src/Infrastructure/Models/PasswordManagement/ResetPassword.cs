﻿namespace Infrastructure.Models.ResetPassword
{
    public class ResetPassword
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public string Code { get; set; }
    }
}
