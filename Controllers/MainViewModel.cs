using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MMIMApp.Data;
using MMIMApp.Views.CategoryViews;

namespace MMIMApp.Controllers
{
    public class MainViewModel : ViewModelBase
    {
        public ViewModelBase CurrentViewModel { get; }

        public MainViewModel()
        {
            CurrentViewModel = new SearchCategoriesViewModel();
        }
    }
}
