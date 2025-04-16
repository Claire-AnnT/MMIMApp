using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MMIMApp.Commands;
using System.Windows.Input;
using MMIMApp.Data;
using MMIMApp.Models;
using System.IO;

namespace MMIMApp.Controllers
{
    internal class SearchCategoriesViewModel : ViewModelBase
    {
    private Category categoryToDelete;
        private Category categoryToEdit;

        private string categoryToEditName;
        public string CategoryToEditName
        {
            get => categoryToEditName;
            set
            {
                categoryToEditName = value;
                OnPropertyChanged(nameof(CategoryToEditName));
                ((RelayCommand)UpdateCommand).RaiseCanExecuteChanged();
            }
        }

        //Observable collections
        public ObservableCollection<Category> AllCategories { get; set; } = new ObservableCollection<Category>();
    public ObservableCollection<Category> PagedCategories { get; set; } = new ObservableCollection<Category>();
    public ObservableCollection<Category> FilteredCategories { get; private set; }

    //Properties
    private Category selectedCategory;
    public Category SelectedCategory
    {
        get => selectedCategory;
        set
        {
            selectedCategory = value;
            OnPropertyChanged(nameof(SelectedCategory));
        }
    }

    private bool successPopUpIsOpen;
    public bool SuccessPopUpIsOpen
    {
        get => successPopUpIsOpen;
        set
        {
            successPopUpIsOpen = value;
            OnPropertyChanged(nameof(SuccessPopUpIsOpen));
        }
    }

    private string successPopUpMessage;
    public string SuccessPopUpMessage
    {
        get => successPopUpMessage;
        set
        {
            successPopUpMessage = value;
            OnPropertyChanged(nameof(SuccessPopUpMessage));
        }
    }

    private bool errorPopUpIsOpen;
    public bool ErrorPopUpIsOpen
    {
        get => errorPopUpIsOpen;
        set
        {
            errorPopUpIsOpen = value;
            OnPropertyChanged(nameof(ErrorPopUpIsOpen));
        }
    }

    private string errorPopUpMessage;
    public string ErrorPopUpMessage
    {
        get => errorPopUpMessage;
        set
        {
            errorPopUpMessage = value;
            OnPropertyChanged(nameof(ErrorPopUpMessage));
        }
    }

    private bool confirmationPopUpIsOpen;
    public bool ConfirmationPopUpIsOpen
    {
        get => confirmationPopUpIsOpen;
        set
        {
            confirmationPopUpIsOpen = value;
            OnPropertyChanged(nameof(ConfirmationPopUpIsOpen));
        }
    }

        private bool addViewIsOpen;
        public bool AddViewIsOpen
        {
            get => addViewIsOpen;
            set
            {
                addViewIsOpen = value;
                OnPropertyChanged(nameof(AddViewIsOpen));
            }
        }

    private string confirmationPopUpTitle;
    public string ConfirmationPopUpTitle
    {
        get => confirmationPopUpTitle;
        set
        {
            confirmationPopUpTitle = value;
            OnPropertyChanged(nameof(ConfirmationPopUpTitle));
        }
    }

    private string confirmationPopUpMessage;
    public string ConfirmationPopUpMessage
    {
        get => confirmationPopUpMessage;
        set
        {
            confirmationPopUpMessage = value;
            OnPropertyChanged(nameof(ConfirmationPopUpMessage));
        }
    }

    private bool editViewIsOpen;
    public bool EditViewIsOpen
        {
            get => editViewIsOpen;
            set
            {
                editViewIsOpen = value;
                OnPropertyChanged(nameof(EditViewIsOpen));
            }
        }

        private string searchString;
    public string SearchString
    {
        get => searchString;
        set
        {
            searchString = value;
            OnPropertyChanged(nameof(SearchString));
            Search(searchString);
        }
    }

        private string categoryName;
        public string CategoryName
        {
            get => categoryName;
            set
            {
                categoryName = value;
                OnPropertyChanged(nameof(CategoryName));
                ((RelayCommand)AddCommand).RaiseCanExecuteChanged();
            }
        }


        public string NumPageItems =>
        $"Page {CurrentPage} of {TotalPages} " +
        $"({PagedCategories.Count} items / {FilteredCategories?.Count ?? 0} total)";

    private int currentPage = 1;
    public int CurrentPage
    {
        get => currentPage;
        set
        {
            if (currentPage != value)
            {
                currentPage = value;
                OnPropertyChanged(nameof(CurrentPage));
                UpdatePagedCategories();

                (PreviousPageCommand as RelayCommand)?.RaiseCanExecuteChanged();
                (NextPageCommand as RelayCommand)?.RaiseCanExecuteChanged();
            }
        }
    }

    private void UpdatePagedCategories()
    {
        if (FilteredCategories == null)
            return;

        PagedCategories.Clear();
        foreach (var category in FilteredCategories
            .Skip((CurrentPage - 1) * 5)
            .Take(5))
        {
            PagedCategories.Add(category);
        }

        OnPropertyChanged(nameof(PagedCategories));
        OnPropertyChanged(nameof(NumPageItems));
    }

    public int TotalPages =>
(FilteredCategories != null && FilteredCategories.Count > 0)
    ? (int)Math.Ceiling((double)FilteredCategories.Count / 5)
    : 1;

    private void Search(string searchString)
    {
        if (string.IsNullOrWhiteSpace(searchString))
        {
            FilteredCategories = new ObservableCollection<Category>(AllCategories);
            PagedCategories.Clear();
            foreach (var category in FilteredCategories.Take(5))
            {
                PagedCategories.Add(category);
            }
        }
        else
        {
            var categories = context.Categories
                .Where(p =>
                    p.Name != null && p.Name.ToLower().Contains(searchString.ToLower()))
                .ToList();

            FilteredCategories = new ObservableCollection<Category>(categories);
            PagedCategories.Clear();
            foreach (var category in categories.Take(5))
            {
                PagedCategories.Add(category);
            }
        }

        CurrentPage = 1;
        UpdatePagedCategories();
        OnPropertyChanged(nameof(FilteredCategories));
        OnPropertyChanged(nameof(PagedCategories));
    }

    private readonly MMIMAppContext context;

    //Commands
    public ICommand EditCategoryCommand { get; }
    public ICommand DeleteCommand { get; }
    public ICommand LogoutCommand { get; }
    public ICommand CloseSuccessPopup { get; }
    public ICommand ShowConfirmationPopUpCmd { get; }
    public ICommand CloseConfirmationPopup { get; }
    public ICommand CloseErrorPopup { get; }
    public ICommand ExportCommand { get; }
    public ICommand NextPageCommand { get; }
    public ICommand PreviousPageCommand { get; }
    public ICommand ShowAddView { get; }
    public ICommand CancelCommand { get; }
    public ICommand AddCommand { get; }
    public ICommand UpdateCommand { get; }
    public ICommand ShowEditView { get; }




        //Constructor
        public SearchCategoriesViewModel()
    {
        context = new MMIMAppContext();
        context.Database.EnsureCreated();

        //Initialize collections
        List<Category> categories = context.Categories.ToList();
        AllCategories = new ObservableCollection<Category>(categories);

        if (!context.Categories.Any())
        {

            for (int i = 0; i < 25; i++)
            {
                Category newCategory = new Category($"Category {i}");
                context.Categories.Add(newCategory);
            }

            context.SaveChanges();
        }

        FilteredCategories = new ObservableCollection<Category>(AllCategories);
        PagedCategories = new ObservableCollection<Category>(AllCategories.Take(5));

        //Initialize properties
        SelectedCategory = null!;
        SuccessPopUpIsOpen = false;
        ErrorPopUpIsOpen = false;
        ConfirmationPopUpIsOpen = false;
        SuccessPopUpMessage = string.Empty;
        ErrorPopUpMessage = string.Empty;
        ConfirmationPopUpMessage = string.Empty;
        ConfirmationPopUpTitle = $"Delete Category?";
            AddViewIsOpen = false;
            EditViewIsOpen = false;
            categoryToDelete = null!;
            categoryToEdit = null!;
            currentPage = 1;




            //Initialize commands
        DeleteCommand = new RelayCommand(DeleteCategory, CanDeleteCategory);
        CloseSuccessPopup = new RelayCommand(CloseSuccessPopUp, CanCloseSuccessPopUp);
        CloseConfirmationPopup = new RelayCommand(CloseConfirmationPopUp, CanCloseConfirmationPopUp);
        CloseErrorPopup = new RelayCommand(CloseErrorPopUp, CanCloseErrorPopUp);
        ShowConfirmationPopUpCmd = new RelayCommand(ShowConfirmationPopUp, CanShowConfirmationPopUp);
        ExportCommand = new RelayCommand(Export, CanExport);
        NextPageCommand = new RelayCommand(NextPage, CanNextPage);
        PreviousPageCommand = new RelayCommand(PreviousPage, CanPreviousPage);
        ShowAddView = new RelayCommand(ShowAddCategoryView, CanShowAddCategoryView);
        CancelCommand = new RelayCommand(Cancel, CanCancel);
        AddCommand = new RelayCommand(AddCategory, CanAddCategory);
        UpdateCommand = new RelayCommand(UpdateCategory, CanUpdateCategory);
        ShowEditView = new RelayCommand(ShowEditCategoryView, CanShowEditCategoryView);
        }

        private bool CanShowEditCategoryView(object obj) => true;

        private void ShowEditCategoryView(object obj)
        {
            if (obj is Category category)
            {
                categoryToEdit = category;
            }
            EditViewIsOpen = true;
        }

        private bool CanUpdateCategory(object obj)
        {
            if (string.IsNullOrWhiteSpace(CategoryToEditName))
                return false;
            return true;
        }

        private void UpdateCategory(object obj)
        {
            if (categoryToEdit != null)
            {
                try
                {
                    var dbCategory = context.Categories.FirstOrDefault(p => p.Id == categoryToEdit.Id);
                    if (dbCategory != null)
                    {
                        dbCategory.Name = CategoryToEditName;
                        context.Categories.Update(dbCategory);
                        context.SaveChanges();

                        categoryToEdit.Name = CategoryToEditName;

                        EditViewIsOpen = false;
                        SuccessPopUpMessage = "Category saved successfully.";
                        SuccessPopUpIsOpen = true;
                    }
                    else
                    {
                        ErrorPopUpMessage = "Error deleting category: Category not found.";
                        ErrorPopUpIsOpen = true;
                    }
                }
                catch (Exception e)
                {
                    ErrorPopUpMessage = $"Error deleting category: {e.Message}";
                    ErrorPopUpIsOpen = true;
                }
                finally
                {
                    categoryToEdit = null;
                }
            }
        }

        private bool CanAddCategory(object obj)
        {
            if (string.IsNullOrWhiteSpace(CategoryName))
                return false;
            return true;
        }

        private void AddCategory(object obj)
        {
            Category newCategory = new Category(CategoryName);
            context.Categories.Add(newCategory);
            context.SaveChanges();
            AddViewIsOpen = false;
            SuccessPopUpMessage = "Category added successfully.";
            SuccessPopUpIsOpen = true;
        }

        private bool CanCancel(object obj) => true;

        private void Cancel(object obj)
        {
            AddViewIsOpen = false;
            EditViewIsOpen = false;
            CategoryName = string.Empty;
            CategoryToEditName = string.Empty;
        }

        private bool CanShowAddCategoryView(object obj) => true;

        private void ShowAddCategoryView(object obj)
        {
            AddViewIsOpen = true;
        }

        private bool CanPreviousPage(object obj) => CurrentPage > 1;

    private void PreviousPage(object obj)
    {
        CurrentPage--;
    }

    private bool CanNextPage(object obj) => CurrentPage < TotalPages;

    private void NextPage(object obj)
    {
        CurrentPage++;
    }

    private bool CanExport(object obj) => AllCategories.Count > 0 && FilteredCategories.Count > 0;

    private void Export(object obj)
    {
        var sb = new StringBuilder();
        sb.AppendLine($"Category Name");

        foreach (var category in context.Categories)
        {
            sb.AppendLine($"{Escape(category.Name)}");
        }

        var saveFileDialog = new Microsoft.Win32.SaveFileDialog
        {
            Filter = "CSV files (*.csv)|*.csv",
            DefaultExt = "csv",
            FileName = "Categories.csv"
        };

        bool? result = saveFileDialog.ShowDialog();
        if (result == true)
        {
            try
            {
                File.WriteAllText(saveFileDialog.FileName, sb.ToString(), Encoding.UTF8);
                SuccessPopUpMessage = "Exported successfully.";
                SuccessPopUpIsOpen = true;
            }
            catch
            {
                ErrorPopUpMessage = "Error exporting file.";
                ErrorPopUpIsOpen = true;
            }
        }
        else
        {
            ErrorPopUpMessage = "Export cancelled.";
            ErrorPopUpIsOpen = true;
        }

    }

    private string Escape(string input)
    {
        if (string.IsNullOrWhiteSpace(input)) return "\"\"";
        return input.Contains(",") || input.Contains("\"") ? $"\"{input.Replace("\"", "\"\"")}\"" : input;
    }

    // Misc commands
    private bool CanShowConfirmationPopUp(object obj) => true;

    private void ShowConfirmationPopUp(object obj)
    {
        if (obj is Category category)
        {
            categoryToDelete = category;
            ConfirmationPopUpMessage = "Are you sure you want to delete this category?";
            ConfirmationPopUpIsOpen = true;
        }
    }

    private bool CanCloseErrorPopUp(object obj) => true;

    private void CloseErrorPopUp(object obj)
    {
        ErrorPopUpIsOpen = false;
    }

    private bool CanCloseConfirmationPopUp(object obj) => true;

    private void CloseConfirmationPopUp(object obj)
    {
        ConfirmationPopUpIsOpen = false;
    }

    private bool CanCloseSuccessPopUp(object obj) => true;

    private void CloseSuccessPopUp(object obj)
    {
        SuccessPopUpIsOpen = false;
    }


    //Data grid commands

    

    private bool CanEditCategory(object obj) => true;

    private void EditCategory(object obj)
    {
        // Navigate to EditCategoryView
    }

    private bool CanDeleteCategory(object obj) => true;

    private void DeleteCategory(object obj)
    {
        if (categoryToDelete != null)
        {
            ConfirmationPopUpIsOpen = false;
            try
            {
                var dbCategory = context.Categories.FirstOrDefault(p => p.Id == categoryToDelete.Id);
                if (dbCategory != null)
                {
                        var relatedProducts = context.Products
                    .Where(p => p.CategoryId == categoryToDelete.Id)
                    .ToList();
                        foreach (var product in relatedProducts)
                        {
                            product.CategoryId = null;
                        }


                        context.Categories.Remove(dbCategory);
                    context.SaveChanges();
                    SuccessPopUpMessage = "Category deleted successfully.";
                    SuccessPopUpIsOpen = true;
                    RefreshCategoryList();
                }
                else
                {
                    ErrorPopUpMessage = "Error deleting category: Category not found.";
                    ErrorPopUpIsOpen = true;
                }
            }
            catch (Exception e)
            {
                ErrorPopUpMessage = $"Error deleting category: {e.Message}";
                ErrorPopUpIsOpen = true;
            }
            finally
            {
                categoryToDelete = null;
            }
        }
    }

    private void RefreshCategoryList()
    {
        var categories = context.Categories.ToList();

        AllCategories = new ObservableCollection<Category>(categories);
        FilteredCategories = new ObservableCollection<Category>(AllCategories);

        PagedCategories.Clear();
        foreach (var category in AllCategories.Take(5))
        {
            PagedCategories.Add(category);
        }

        CurrentPage = 1;
        UpdatePagedCategories();

        OnPropertyChanged(nameof(AllCategories));
        OnPropertyChanged(nameof(FilteredCategories));
        OnPropertyChanged(nameof(PagedCategories));
        OnPropertyChanged(nameof(NumPageItems));
    }



}
}
