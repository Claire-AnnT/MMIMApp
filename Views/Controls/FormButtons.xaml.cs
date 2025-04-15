using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MMIMApp.Views.Controls
{
    /// <summary>
    /// Interaction logic for FormButtons.xaml
    /// </summary>
    public partial class FormButtons : UserControl
    {
        public FormButtons()
        {
            InitializeComponent();
        }

        public static DependencyProperty ButtonTextProperty = DependencyProperty.Register("ButtonText", typeof(string), typeof(FormButtons), new PropertyMetadata("Button"));
        public string ButtonText
        {
            get => (string)GetValue(ButtonTextProperty);
            set => SetValue(ButtonTextProperty, value);
        }

        public static DependencyProperty CancelCommandProperty = DependencyProperty.Register("CancelCommand", typeof(ICommand), typeof(FormButtons), new PropertyMetadata(null));
        public ICommand CancelCommand
        {
            get => (ICommand)GetValue(CancelCommandProperty);
            set => SetValue(CancelCommandProperty, value);
        }

        public static DependencyProperty SubmitCommandProperty = DependencyProperty.Register("SubmitCommand", typeof(ICommand), typeof(FormButtons), new PropertyMetadata(null));
        public ICommand SubmitCommand
        {
            get => (ICommand)GetValue(SubmitCommandProperty);
            set => SetValue(SubmitCommandProperty, value);
        }
    }
}
