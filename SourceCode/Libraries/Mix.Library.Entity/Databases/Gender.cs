using System.ComponentModel;

namespace Mix.Library.Entities.Databases
{
    /// <summary>
    /// 性别
    /// </summary>
    public enum Gender
    {
        /// <summary>
        /// The male
        /// </summary>
        [Description("男")]
        Male,

        /// <summary>
        /// The female
        /// </summary>
        [Description("女")]
        Female
    }
}