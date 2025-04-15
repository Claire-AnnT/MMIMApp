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
    /// Interaction logic for FormRadioBtn.xaml
    /// </summary>
    public partial class FormRadioBtn : UserControl
    {
        public FormRadioBtn()
        {
            InitializeComponent();
        }

        public static DependencyProperty FieldLabelProperty = DependencyProperty.Register("FieldLabel", typeof(string), typeof(FormRadioBtn), new PropertyMetadata("Field Label"));
        public string FieldLabel
        {
            get => (string)GetValue(FieldLabelProperty);
            set => SetValue(FieldLabelProperty, value);
        }

        public static DependencyProperty IsCheckedProperty = DependencyProperty.Register("IsChecked", typeof(bool), typeof(FormRadioBtn), new PropertyMetadata(false));
        public bool IsChecked
        {
            get => (bool)GetValue(IsCheckedProperty);
            set => SetValue(IsCheckedProperty, value);
        }
    }
}
