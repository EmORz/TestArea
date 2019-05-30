using System.Collections.Generic;

namespace SIS.MvcFramework.Identity
{
    public class Principal
    {
        public string Id { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public List<string> Roles { get; set; } = new List<string>();

    }
}