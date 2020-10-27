namespace Mix.Data.Dtos
{
    /// <summary>
    /// 实现HETAOAS的LinkDto
    /// </summary>
    public class LinkDto
    {
        /// <summary>
        /// Gets or sets the href.
        /// </summary>
        /// <value>
        /// The href.
        /// </value>
        public string Href { get; }

        /// <summary>
        /// Gets or sets the relative.
        /// </summary>
        /// <value>
        /// The relative.
        /// </value>
        public string Rel { get; }

        /// <summary>
        /// Gets or sets the method.
        /// </summary>
        /// <value>
        /// The method.
        /// </value>
        public string Method { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="LinkDto"/> class.
        /// </summary>
        /// <param name="href">The href.</param>
        /// <param name="rel">The relative.</param>
        /// <param name="method">The method.</param>
        public LinkDto(string href, string rel, string method)
        {
            Href = href;
            Rel = rel;
            Method = method;
        }
    }
}