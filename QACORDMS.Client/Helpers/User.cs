﻿namespace QACORDMS.Client.Helpers
{
    public class User
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public List<string> RoleNames { get; set; } = new List<string>();
   
    }
}