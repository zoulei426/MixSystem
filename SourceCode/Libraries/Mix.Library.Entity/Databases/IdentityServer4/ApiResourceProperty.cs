﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mix.Library.Entities.Databases.IdentityServer4
{
    /// <summary>
    /// ApiResourceProperty
    /// </summary>
    /// <seealso cref="Mix.Library.Entities.Databases.IdentityServer4.Property" />
    public class ApiResourceProperty : Property
    {
        /// <summary>
        /// Gets or sets the API resource identifier.
        /// </summary>
        /// <value>
        /// The API resource identifier.
        /// </value>
        public int ApiResourceId { get; set; }

        /// <summary>
        /// Gets or sets the API resource.
        /// </summary>
        /// <value>
        /// The API resource.
        /// </value>
        public ApiResource ApiResource { get; set; }
    }
}