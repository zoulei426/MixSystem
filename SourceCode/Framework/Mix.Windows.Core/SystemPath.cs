using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mix.Windows.Core
{
    public static class SystemPath
    {
        /// <summary>
        /// It represents the path where the "Mix.Desktop" is located.
        /// </summary>
        public static readonly string Application = AppDomain.CurrentDomain.BaseDirectory;

        /// <summary>
        /// Data
        /// </summary>
        public static readonly string Data = Path.Combine(Application, nameof(Data));

        /// <summary>
        /// Logs
        /// </summary>
        public static readonly string Logs = Path.Combine(Application, nameof(Logs));

        /// <summary>
        /// Configs
        /// </summary>
        public static readonly string Configs = Path.Combine(Application, nameof(Configs));

        /// <summary>
        /// %AppData%\MixSystem
        /// </summary>
        public static readonly string AppData = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "MixSystem");

        /// <summary>
        /// %AppData%\MixSystem\Apps
        /// </summary>
        public static readonly string Apps = Path.Combine(AppData, nameof(Apps));

        /// <summary>
        /// %AppData%\MixSystem\Users
        /// </summary>
        public static readonly string Users = Path.Combine(AppData, nameof(Users));

        static SystemPath()
        {
            Directory.CreateDirectory(Data);
            Directory.CreateDirectory(Logs);
            Directory.CreateDirectory(Configs);
            Directory.CreateDirectory(Apps);
            Directory.CreateDirectory(Users);
        }
    }
}
