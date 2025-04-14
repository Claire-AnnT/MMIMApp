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
    public partial class FormField : UserControl
    {
        public FormField()
        {
            InitializeComponent();
        }

        public static DependencyProperty FieldLabelProperty = DependencyProperty.Register("FieldLabel", typeof(string), typeof(FormField), new PropertyMetadata("Field Label"));
        public string FieldLabel
        {
            get => (string)GetValue(FieldLabelProperty);
            set => SetValue(FieldLabelProperty, value);
        }

        public static DependencyProperty FieldTextProperty = DependencyProperty.Register("FieldText", typeof(string), typeof(FormField), new PropertyMetadata(string.Empty));
        public string FieldText
        {
            get => (string)GetValue(FieldTextProperty);
            set => SetValue(FieldTextProperty, value);
        }
    }


}
