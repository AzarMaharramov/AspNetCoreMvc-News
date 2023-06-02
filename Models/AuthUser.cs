using System;
using System.Collections.Generic;

namespace MyFirstProject.Models
{
    public partial class AuthUser
    {
        public int Id { get; set; }
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
