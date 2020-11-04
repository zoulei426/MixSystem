using MaterialDesignColors;
using MaterialDesignThemes.Wpf;
using Mix.Windows.WPF;
using Prism.Commands;
using Prism.Ioc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Mix.Desktop.ViewModels.Settings
{
    public class ThemeSettingTabItemViewModel : ViewModelBase
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ThemeSettingTabItemViewModel(IContainerExtension container) : base(container)
        {
            Swatches = new SwatchesProvider().Swatches;

            PaletteHelper paletteHelper = new PaletteHelper();
            ITheme theme = paletteHelper.GetTheme();

            IsDarkTheme = theme.GetBaseTheme() == BaseTheme.Dark;

            if (paletteHelper.GetThemeManager() is { } themeManager)
            {
                themeManager.ThemeChanged += (_, e) =>
                {
                    IsDarkTheme = e.NewTheme?.GetBaseTheme() == BaseTheme.Dark;
                };
            }
        }

        private bool _IsDarkTheme;

        public bool IsDarkTheme
        {
            get { return _IsDarkTheme; }
            set
            {
                if (SetProperty(ref _IsDarkTheme, value))
                    ModifyTheme(theme => theme.SetBaseTheme(value ? Theme.Dark : Theme.Light));
            }
        }

        public IEnumerable<Swatch> Swatches { get; }

        public ICommand ApplyPrimaryCommand { get; } = new DelegateCommand<Swatch>(ApplyPrimary);

        private static void ApplyPrimary(Swatch swatch)
            => ModifyTheme(theme => theme.SetPrimaryColor(swatch.ExemplarHue.Color));

        public ICommand ApplyAccentCommand { get; } = new DelegateCommand<Swatch>(ApplyAccent);

        private static void ApplyAccent(Swatch swatch)
            => ModifyTheme(theme => theme.SetSecondaryColor(swatch.AccentExemplarHue.Color));

        private static void ModifyTheme(Action<ITheme> modificationAction)
        {
            var paletteHelper = new PaletteHelper();
            ITheme theme = paletteHelper.GetTheme();

            modificationAction?.Invoke(theme);

            paletteHelper.SetTheme(theme);
        }
    }
}