using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace Mix.Windows.Controls
{
    /// <summary>
    /// SwitchLoadingViewContainer
    /// </summary>
    /// <seealso cref="System.Windows.Controls.UserControl" />
    [ContentProperty("LoadedContent")]
    public class SwitchLoadingViewContainer : UserControl
    {
        /// <summary>
        /// The is loading property
        /// </summary>
        public static readonly DependencyProperty IsLoadingProperty = DependencyProperty.Register("IsLoading", typeof(bool), typeof(SwitchLoadingViewContainer), new PropertyMetadata(false));

        /// <summary>
        /// The loading content property
        /// </summary>
        public static readonly DependencyProperty LoadingContentProperty = DependencyProperty.Register("LoadingContent", typeof(object), typeof(SwitchLoadingViewContainer), new PropertyMetadata(null));

        /// <summary>
        /// The loaded content property
        /// </summary>
        public static readonly DependencyProperty LoadedContentProperty = DependencyProperty.Register("LoadedContent", typeof(object), typeof(SwitchLoadingViewContainer), new PropertyMetadata(null));

        /// <summary>
        /// Gets or sets a value indicating whether this instance is loading.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is loading; otherwise, <c>false</c>.
        /// </value>
        public bool IsLoading
        {
            get => (bool)this.GetValue(IsLoadingProperty);
            set => this.SetValue(IsLoadingProperty, value);
        }

        /// <summary>
        /// Gets or sets the content of the loading.
        /// </summary>
        /// <value>
        /// The content of the loading.
        /// </value>
        public object LoadingContent
        {
            get => this.GetValue(LoadingContentProperty);
            set => this.SetValue(LoadingContentProperty, value);
        }

        /// <summary>
        /// Gets or sets the content of the loaded.
        /// </summary>
        /// <value>
        /// The content of the loaded.
        /// </value>
        public object LoadedContent
        {
            get => this.GetValue(LoadedContentProperty);
            set => this.SetValue(LoadedContentProperty, value);
        }
    }
}