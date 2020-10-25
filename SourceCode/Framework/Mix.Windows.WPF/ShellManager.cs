using System.Linq;
using System.Windows;

namespace Mix.Windows.WPF
{
    /// <summary>
    /// Shell管理器
    /// </summary>
    public class ShellManager
    {
        /// <summary>
        /// Shows the specified window.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="window">The window.</param>
        public static void Show<T>(T window = null) where T : Window, new()
        {
            var shell = Application.Current.MainWindow = window ?? new T();
            //shell.Loaded += ProcessController.OnWindowLoaded;
            shell.Show();
        }

        /// <summary>
        /// Closes this instance.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public static void Close<T>() where T : Window
        {
            var shell = Application.Current.Windows.OfType<Window>().FirstOrDefault(window => window is T);
            shell?.Close();
        }

        /// <summary>
        /// Switches this instance.
        /// </summary>
        /// <typeparam name="TClose">The type of the close.</typeparam>
        /// <typeparam name="TShow">The type of the show.</typeparam>
        public static void Switch<TClose, TShow>()
            where TShow : Window, new()
            where TClose : Window
        {
            Show<TShow>();
            Close<TClose>();
        }
    }
}