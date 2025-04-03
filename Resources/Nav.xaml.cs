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

namespace MMIMApp.Views
{
    /// <summary>
    /// Interaction logic for Nav.xaml
    /// </summary>
    public partial class Nav : UserControl
    {
        public Nav()
        {
            InitializeComponent();
        }

        private void btnMouseEnter(object sender, MouseEventArgs e)
        {
            if (sender is Button btn)
            {
                BrushConverter brushConverter = new();
                btn.Background = brushConverter.ConvertFromString("#212F3D") as Brush;
                Console.WriteLine("Button mouse entered!");
            }
        }

        private void btnMouseLeave(object sender, MouseEventArgs e)
        {
            if (sender is Button btn)
            {
                BrushConverter brushConverter = new();
                btn.Background = brushConverter.ConvertFromString("#17202A") as Brush;
                Console.WriteLine("Button mouse left!");
            }
        }
    }
}
