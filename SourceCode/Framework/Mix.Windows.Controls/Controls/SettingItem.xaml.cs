using System.Windows;
using System.Windows.Input;
using MaterialDesignThemes.Wpf;

namespace Mix.Windows.Controls
{
    /// <summary>
    /// SettingItem.xaml 的交互逻辑
    /// </summary>
    public partial class SettingItem : ICommandSource
    {
        /// <summary>
        /// The label property
        /// </summary>
        public static readonly DependencyProperty LabelProperty = DependencyProperty.Register("Label", typeof(string), typeof(SettingItem), new PropertyMetadata(null));

        /// <summary>
        /// The icon kind property
        /// </summary>
        public static readonly DependencyProperty IconKindProperty = DependencyProperty.Register("IconKind", typeof(PackIconKind), typeof(SettingItem), new PropertyMetadata(default(PackIconKind)));

        /// <summary>
        /// The command property
        /// </summary>
        public static readonly DependencyProperty CommandProperty = DependencyProperty.Register("Command", typeof(ICommand), typeof(SettingItem), new PropertyMetadata(default(ICommand)));

        /// <summary>
        /// The command parameter property
        /// </summary>
        public static readonly DependencyProperty CommandParameterProperty = DependencyProperty.Register("CommandParameter", typeof(object), typeof(SettingItem), new PropertyMetadata(default(object)));

        /// <summary>
        /// The command target property
        /// </summary>
        public static readonly DependencyProperty CommandTargetProperty = DependencyProperty.Register("CommandTarget", typeof(IInputElement), typeof(SettingItem), new PropertyMetadata(default(IInputElement)));

        /// <summary>
        /// Initializes a new instance of the <see cref="SettingItem"/> class.
        /// </summary>
        public SettingItem()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Gets or sets the label.
        /// </summary>
        /// <value>
        /// The label.
        /// </value>
        public string Label
        {
            get => (string)GetValue(LabelProperty);
            set => SetValue(LabelProperty, value);
        }

        /// <summary>
        /// Gets or sets the kind of the icon.
        /// </summary>
        /// <value>
        /// The kind of the icon.
        /// </value>
        public PackIconKind IconKind
        {
            get => (PackIconKind)GetValue(IconKindProperty);
            set => SetValue(IconKindProperty, value);
        }

        /// <summary>
        /// Gets the command that will be executed when the command source is invoked.
        /// </summary>
        public ICommand Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        /// <summary>
        /// Represents a user defined data value that can be passed to the command when it is executed.
        /// </summary>
        public object CommandParameter
        {
            get => (object)GetValue(CommandParameterProperty);
            set => SetValue(CommandParameterProperty, value);
        }

        /// <summary>
        /// The object that the command is being executed on.
        /// </summary>
        public IInputElement CommandTarget
        {
            get => (IInputElement)GetValue(CommandTargetProperty);
            set => SetValue(CommandTargetProperty, value);
        }

        private void SettingItem_OnMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (Command?.CanExecute(CommandParameter) ?? false) Command.Execute(CommandParameter);
        }
    }
}