using System;
using System.Collections.Generic;

namespace testSessionUsage.Models
{
    public partial class UserDetail
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}
