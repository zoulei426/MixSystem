﻿namespace Mix.Windows.WPF
{
    public interface ILocalizable
    {
        I18nManager I18nManager { get; set; }

        void OnCurrentUICultureChanged();
    }
}