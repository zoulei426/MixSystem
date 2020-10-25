using System;

namespace Mix.Data
{
    /// <summary>
    /// ICurrentUser
    /// </summary>
    public interface ICurrentUser
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public Guid? ID { get; set; }
    }
}