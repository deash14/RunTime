using System;
using System.Windows;
using System.Windows.Controls;

namespace Wpf
{
    /// <summary>
    /// Interaction logic for TextBoxTime.xaml
    /// </summary>
    public partial class RequiredTextBox : UserControl
    {
        public RequiredTextBox()
        {
            InitializeComponent();
        }

        public bool IsRequired
        {
            get { return (bool)GetValue(IsRequiredProperty); }
            set { SetValue(IsRequiredProperty, value); }
        }

        public static readonly DependencyProperty IsRequiredProperty = DependencyProperty.Register(nameof(IsRequired),
                                                                                                   typeof(bool),
                                                                                                   typeof(RequiredTextBox),
                                                                                                   new PropertyMetadata(false));

        public String TextInControl
        {
            get { return (String)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public static readonly DependencyProperty TextProperty = DependencyProperty.Register(nameof(TextInControl),
                                                                                             typeof(String),
                                                                                             typeof(RequiredTextBox),
                                                                                             new PropertyMetadata(""));
    }
}
