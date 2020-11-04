using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Mix.Windows.Controls.Helpers
{
    /// <summary>
    /// PaletteHelper
    /// </summary>
    public class PaletteHelper
    {
        /// <summary>
        /// Gets the theme.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException">Cannot get theme outside of a WPF application. Use ResourceDictionaryExtensions.GetTheme on the appropriate resource dictionary instead.</exception>
        public virtual ITheme GetTheme()
        {
            if (Application.Current is null)
                throw new InvalidOperationException("Cannot get theme outside of a WPF application. Use ResourceDictionaryExtensions.GetTheme on the appropriate resource dictionary instead.");
            return Application.Current.Resources.GetTheme();
        }

        /// <summary>
        /// Sets the theme.
        /// </summary>
        /// <param name="theme">The theme.</param>
        /// <exception cref="ArgumentNullException">theme</exception>
        /// <exception cref="InvalidOperationException">Cannot set theme outside of a WPF application. Use ResourceDictionaryExtensions.SetTheme on the appropriate resource dictionary instead.</exception>
        public virtual void SetTheme(ITheme theme)
        {
            if (theme is null) throw new ArgumentNullException(nameof(theme));
            if (Application.Current is null)
                throw new InvalidOperationException("Cannot set theme outside of a WPF application. Use ResourceDictionaryExtensions.SetTheme on the appropriate resource dictionary instead.");
            Application.Current.Resources.SetTheme(theme);
        }

        /// <summary>
        /// Gets the theme manager.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException">Cannot get ThemeManager. Use ResourceDictionaryExtensions.GetThemeManager on the appropriate resource dictionary instead.</exception>
        public virtual IThemeManager GetThemeManager()
        {
            if (Application.Current is null)
                throw new InvalidOperationException("Cannot get ThemeManager. Use ResourceDictionaryExtensions.GetThemeManager on the appropriate resource dictionary instead.");
            return Application.Current.Resources.GetThemeManager();
        }
    }
}