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
    /// Interaction logic for FormComboBox.xaml
    /// </summary>
    public partial class FormComboBox : UserControl
    {
        public FormComboBox()
        {
            InitializeComponent();
        }

        public static DependencyProperty FieldLabelProperty = DependencyProperty.Register("FieldLabel", typeof(string), typeof(FormComboBox), new PropertyMetadata("Field Label"));
        public string FieldLabel
        {
            get => (string)GetValue(FieldLabelProperty);
            set => SetValue(FieldLabelProperty, value);
        }

        public static DependencyProperty SelectedItemIdProperty = DependencyProperty.Register("SelectedItemId", typeof(int), typeof(FormComboBox), new PropertyMetadata(0));
        public int SelectedItemId
        {
            get => (int)GetValue(SelectedItemIdProperty);
            set => SetValue(SelectedItemIdProperty, value);
        }

        public static DependencyProperty ItemSourceProperty = DependencyProperty.Register("ItemSource", typeof(IEnumerable<ComboBoxItem>), typeof(FormComboBox), new PropertyMetadata(null));
        public IEnumerable<ComboBoxItem> ItemSource
        {
            get => (IEnumerable<ComboBoxItem>)GetValue(ItemSourceProperty);
            set => SetValue(ItemSourceProperty, value);
        }
    }
}
