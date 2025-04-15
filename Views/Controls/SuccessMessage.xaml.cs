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
    /// Interaction logic for SuccessMessage.xaml
    /// </summary>
    public partial class SuccessMessage : UserControl
    {
        public SuccessMessage()
        {
            InitializeComponent();
        }

        public string SuccessMessageText
        {
            get => (string)GetValue(SuccessMessageTextProperty);
            set => SetValue(SuccessMessageTextProperty, value);
        }

        public static  DependencyProperty SuccessMessageTextProperty =
            DependencyProperty.Register("SuccessMessageText", typeof(string), typeof(SuccessMessage), new PropertyMetadata(string.Empty));

    }
}
