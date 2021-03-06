﻿using Mix.Data.Entities;
using System.Collections.Generic;

namespace Mix.Library.Entities.Databases
{
    /// <summary>
    /// 公司
    /// </summary>
    /// <seealso cref="Mix.Data.Entities.AduitEntity" />
    public class Company : AduitEntity
    {
        /// <summary>Gets or sets the name.</summary>
        /// <value>The name.</value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the introduction.
        /// </summary>
        /// <value>
        /// The introduction.
        /// </value>
        public string Introduction { get; set; }

        /// <summary>
        /// Gets or sets the country.
        /// </summary>
        /// <value>
        /// The country.
        /// </value>
        public string Country { get; set; }

        /// <summary>
        /// Gets or sets the industry.
        /// </summary>
        /// <value>
        /// The industry.
        /// </value>
        public string Industry { get; set; }

        /// <summary>
        /// Gets or sets the product.
        /// </summary>
        /// <value>
        /// The product.
        /// </value>
        public string Product { get; set; }

        /// <summary>
        /// Gets or sets the employees.
        /// </summary>
        /// <value>
        /// The employees.
        /// </value>
        public ICollection<Employee> Employees { get; set; }
    }
}