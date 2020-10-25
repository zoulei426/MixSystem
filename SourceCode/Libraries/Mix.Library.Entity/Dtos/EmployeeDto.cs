using System;

namespace Mix.Library.Entities.Dtos
{
    /// <summary>
    /// EmployeeDto
    /// </summary>
    public class EmployeeDto
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the company identifier.
        /// </summary>
        /// <value>
        /// The company identifier.
        /// </value>
        public Guid CompanyId { get; set; }

        /// <summary>
        /// Gets or sets the employee no.
        /// </summary>
        /// <value>
        /// The employee no.
        /// </value>
        public string EmployeeNo { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the gender display.
        /// </summary>
        /// <value>
        /// The gender display.
        /// </value>
        public string GenderDisplay { get; set; }

        /// <summary>
        /// Gets or sets the age.
        /// </summary>
        /// <value>
        /// The age.
        /// </value>
        public int Age { get; set; }
    }
}