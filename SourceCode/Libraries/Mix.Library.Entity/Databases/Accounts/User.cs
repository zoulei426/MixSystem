using Mix.Data.Entities;

namespace Mix.Library.Entities.Databases.Accounts
{
    /// <summary>
    /// 用户
    /// </summary>
    /// <seealso cref="Mix.Data.Entities.AduitEntity" />
    public class User : AduitEntity
    {
        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        /// <value>
        /// The username.
        /// </value>
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        public string Email { get; set; }
    }
}