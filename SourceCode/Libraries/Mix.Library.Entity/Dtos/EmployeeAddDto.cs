using Mix.Library.Entities.Databases;
using System;

namespace Mix.Library.Entities.Dtos
{
    /// <summary>
    /// EmployeeAddDto
    /// </summary>
    public class EmployeeAddDto
    {
        /// <summary>
        /// Gets or sets the employee no.
        /// </summary>
        /// <value>
        /// The employee no.
        /// </value>
        public string EmployeeNo { get; set; }

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>
        /// The first name.
        /// </value>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>
        /// The last name.
        /// </value>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the gender.
        /// </summary>
        /// <value>
        /// The gender.
        /// </value>
        public Gender Gender { get; set; }

        /// <summary>
        /// Gets or sets the date of birth.
        /// </summary>
        /// <value>
        /// The date of birth.
        /// </value>
        public DateTime DateOfBirth { get; set; }
    }
}