using Mix.Data.Entities;

namespace Mix.Library.Entity.Databases.Accounts
{
    public class User : AduitEntity
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }
    }
}