using System;

namespace Mix.Data
{
    /// <summary>
    /// 当前用户
    /// </summary>
    /// <seealso cref="Mix.Data.ICurrentUser" />
    public class CurrentUser : ICurrentUser
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public Guid? ID { get => Guid.Empty; set => SetID(); }

        /// <summary>
        /// Sets the identifier.
        /// </summary>
        private void SetID()
        {
        }
    }
}