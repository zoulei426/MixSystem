using Mix.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mix.Library.Entity.Databases.Accounts
{
    public class User : AduitEntity
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }
    }
}