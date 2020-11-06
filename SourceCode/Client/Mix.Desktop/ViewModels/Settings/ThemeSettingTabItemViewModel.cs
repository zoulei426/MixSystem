using MaterialDesignColors;
using MaterialDesignThemes.Wpf;
using Mix.Windows.WPF;
using Prism.Commands;
using Prism.Ioc;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Mix.Desktop.ViewModels.Settings
{
    [AddINotifyPropertyChangedInterface]
    public class ThemeSettingTabItemViewModel : ViewModelBase, IViewLoadedAndUnloadedAware
    {
        #region Properties

        public bool IsDarkTheme
        {
            get { return _IsDarkTheme; }
            set
            {
                if (SetProperty(ref _IsDarkTheme, value))
                    ModifyTheme(theme => theme.SetBaseTheme(value ? Theme.Dark : Theme.Light));
            }
        }

        private bool _IsDarkTheme;

        public IList<Swatch> Swatches { get; private set; }

        #endregion Properties

        #region Commands

        public ICommand ApplyPrimaryCommand { get; private set; }

        public ICommand ApplyAccentCommand { get; private set; }

        #endregion Commands

        #region Ctor

        public ThemeSettingTabItemViewModel(IContainerExtension container) : base(container)
        {
        }

        #endregion Ctor

        #region Methods

        protected override void RegisterCommands()
        {
            ApplyPrimaryCommand = new DelegateCommand<Swatch>(ApplyPrimary);
            ApplyAccentCommand = new DelegateCommand<Swatch>(ApplyAccent);
        }

        public void OnLoaded()
        {
            Swatches = new SwatchesProvider().Swatches.ToList();

            //Swatches.Add(new Swatch("default", primaryHues, accentHues);)

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

        public void OnUnloaded()
        {
        }

        private static void ApplyPrimary(Swatch swatch)
           => ModifyTheme(theme => theme.SetPrimaryColor(swatch.ExemplarHue.Color));

        private static void ApplyAccent(Swatch swatch)
            => ModifyTheme(theme => theme.SetSecondaryColor(swatch.AccentExemplarHue.Color));

        private static void ModifyTheme(Action<ITheme> modificationAction)
        {
            var paletteHelper = new PaletteHelper();
            ITheme theme = paletteHelper.GetTheme();

            modificationAction?.Invoke(theme);

            paletteHelper.SetTheme(theme);
        }

        #endregion Methods
    }
}