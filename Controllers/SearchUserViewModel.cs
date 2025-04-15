using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using Microsoft.Win32;
using MMIMApp.Commands;
using MMIMApp.Models;

namespace MMIMApp.Controllers
{
    class SearchUserViewModel : ViewModelBase
    {
        // Constants
        private const int PAGE_SIZE = 5;

        // Filtering flags
        private bool isFilteringByAdmin = false;
        private bool isFilteringByNotAdmin = false;

        // Observable collections
        public ObservableCollection<User> AllUsers { get; set; }
        public ObservableCollection<User> PagedUsers { get; set; }
        public ObservableCollection<User> FilterUsers { get; private set; }

        // Pagination
        public int TotalPages => (int)Math.Ceiling((double)FilterUsers.Count / PAGE_SIZE);
        public IEnumerable<int> PageNumbers => Enumerable.Range(1, TotalPages);

        private int currentPage = 1;
        public int CurrentPage
        {
            get => currentPage;
            set
            {
                if (value != currentPage)
                {
                    currentPage = value;
                    OnPropertyChanged(nameof(CurrentPage));
                    UpdatePagedUser(); // triggers recalculation and UI update
                }
            }
        }

        private string numPageItems = string.Empty;
        public string NumPageItems
        {
            get => numPageItems;
            set
            {
                numPageItems = value;
                OnPropertyChanged(nameof(NumPageItems));
            }
        }

        // Search
        private string searchString = string.Empty;
        public string SearchString
        {
            get => searchString;
            set
            {
                if (searchString != value)
                {
                    searchString = value;
                    OnPropertyChanged(nameof(SearchString));
                    ApplyFilters();
                }
            }
        }

        private void ApplyFilters()
        {
            IEnumerable<User> filtered = AllUsers;

            
            if (isFilteringByAdmin)
                filtered = filtered.Where(u => u.IsAdmin);
            else if (isFilteringByNotAdmin)
                filtered = filtered.Where(u => !u.IsAdmin);

            
            if (!string.IsNullOrWhiteSpace(SearchString))
            {
                string lowerSearch = SearchString.ToLower();
                filtered = filtered.Where(u =>
                    (!string.IsNullOrEmpty(u.Username) && u.Username.ToLower().Contains(lowerSearch)) ||
                    (!string.IsNullOrEmpty(u.FirstName) && u.FirstName.ToLower().Contains(lowerSearch)) ||
                    (!string.IsNullOrEmpty(u.LastName) && u.LastName.ToLower().Contains(lowerSearch)));
            }

            FilterUsers = new ObservableCollection<User>(filtered);

            CurrentPage = 1;
            UpdatePagedUser(); ;
        }

        // Success message UI
        private Visibility successMsgVisibiliy = Visibility.Collapsed;
        public Visibility SuccessMsgVisibility
        {
            get => successMsgVisibiliy;
            set
            {
                successMsgVisibiliy = value;
                OnPropertyChanged(nameof(SuccessMsgVisibility));
            }
        }

        private string successMsgBody;
        public string SuccessMsgBody
        {
            get => successMsgBody;
            set
            {
                successMsgBody = value;
                OnPropertyChanged(nameof(SuccessMsgBody));
            }
        }

        // Helper properties
        private User selectedUser;
        public User SelectedUser
        {
            get => selectedUser;
            set
            {
                if (selectedUser != value)
                {
                    selectedUser = value;
                    OnPropertyChanged(nameof(SelectedUser));

                    CommandManager.InvalidateRequerySuggested();
                }
            }
        }

        // Commands
        public ICommand ShowFilterMenuCommand { get; set; }
        public ICommand FilterByAdminCommand { get; set; }
        public ICommand FilterByNotAdminCommand { get; set; }
        public ICommand ClearFiltersCommand { get; set; }

        public ICommand AddUserCommand { get; set; }
        public ICommand EditUserCommand { get; set; }
        public ICommand DeleteUserCommand { get; set; }

        public ICommand ExportCommand { get; set; }

        public ICommand PrevPageCommand { get; set; }
        public ICommand NextPageCommand { get; set; }
        public ICommand GoToPageCommand { get; set; }

        public ICommand ShowContextMenuCommand { get; set; }

        public ICommand HideSuccessMsgCommand { get; set; }
        public ICommand LogoutCommand { get; set; }

        // Constructor
        public SearchUserViewModel()
        {
            AllUsers = UserManager.GetUsers();
            for (int i = 0; i < 25; i++)
            {
                User newUser = new User($"User{i}", "Password123", $"FirstName{i}", $"LastName{i}", i % 2 == 0);
                AllUsers.Add(newUser);
            }

            FilterUsers = new ObservableCollection<User>(AllUsers);
            PagedUsers = new ObservableCollection<User>();

            SearchString = string.Empty;
            CurrentPage = 1; 

            UpdatePagedUser();

            SearchString = string.Empty;
            NumPageItems = $"Page {CurrentPage} of {TotalPages} ({PagedUsers.Count} items / {AllUsers.Count} total)";

            SuccessMsgVisibility = Visibility.Collapsed;
            SuccessMsgBody = string.Empty;
            SelectedUser = PagedUsers.FirstOrDefault();



            // Initialize commands
            FilterByAdminCommand = new RelayCommand(FilterByAdmin, CanFilterByAdmin);
            FilterByNotAdminCommand = new RelayCommand(FilterByNotAdmin, CanFilterByNotAdmin);
            ClearFiltersCommand = new RelayCommand(ClearFilters, CanClearFilters);

            AddUserCommand = new RelayCommand(AddUser, CanAddUser);
            EditUserCommand = new RelayCommand(EditUser, CanEditUser);
            DeleteUserCommand = new RelayCommand(DeleteUser, CanDeleteUser);

            ExportCommand = new RelayCommand(Export, CanExport);

            PrevPageCommand = new RelayCommand(PrevPage, CanPrevPage);
            NextPageCommand = new RelayCommand(NextPage, CanNextPage);
            GoToPageCommand = new RelayCommand(GoToPage, CanGoToPage);
            HideSuccessMsgCommand = new RelayCommand(HideSuccessMsg, CanHideSuccessMsg);
            LogoutCommand = new RelayCommand(Logout, CanLogout);
        }

        #region Pagination Methods

        private void UpdatePagedUser()
        {
            if (FilterUsers == null || !FilterUsers.Any())
            {
                PagedUsers.Clear();
                NumPageItems = "No users found.";
                return;
            }

            if (CurrentPage > TotalPages) CurrentPage = TotalPages;
            if (CurrentPage < 1) CurrentPage = 1;

            var userToShow = FilterUsers
                .Skip((CurrentPage - 1) * PAGE_SIZE)
                .Take(PAGE_SIZE)
                .ToList();

            PagedUsers.Clear();
            foreach (var user in userToShow)
                PagedUsers.Add(user);

            NumPageItems = $"Page {CurrentPage} of {TotalPages} ({PagedUsers.Count} items / {FilterUsers.Count} total)";

            OnPropertyChanged(nameof(PagedUsers));
            OnPropertyChanged(nameof(NumPageItems));

            CommandManager.InvalidateRequerySuggested();
        }

        private bool CanNextPage(object obj) => (CurrentPage * PAGE_SIZE) < FilterUsers.Count;
        private void NextPage(object obj)
        {
            if (!CanNextPage(obj)) return;
            CurrentPage++;
            UpdatePagedUser();
        }

        private bool CanPrevPage(object obj) { return true; }
        private void PrevPage(object obj)
        {
            if (!CanPrevPage(obj)) return;
            CurrentPage--;
            UpdatePagedUser();
            CommandManager.InvalidateRequerySuggested();
        }


        private bool CanGoToPage(object obj)
        {
            if (obj == null) return false;
            return int.TryParse(obj.ToString(), out int page) && page >= 1 && page <= TotalPages;
        }

        private void GoToPage(object obj)
        {
            if (!CanGoToPage(obj)) return;
            CurrentPage = int.Parse(obj.ToString());
            UpdatePagedUser();
        }

        #endregion

        #region Filter Methods

        private bool CanFilterByAdmin(object obj) => !isFilteringByAdmin;
        private void FilterByAdmin(object obj)
        {
            isFilteringByAdmin = true;
            isFilteringByNotAdmin = false;
            ApplyFilters();
            CommandManager.InvalidateRequerySuggested();
        }

        private bool CanFilterByNotAdmin(object obj) => !isFilteringByNotAdmin;
        private void FilterByNotAdmin(object obj)
        {
            isFilteringByAdmin = false;
            isFilteringByNotAdmin = true;
            ApplyFilters();
            CommandManager.InvalidateRequerySuggested();
        }

        private bool CanClearFilters(object obj) => isFilteringByAdmin || isFilteringByNotAdmin;
        private void ClearFilters(object obj)
        {
            isFilteringByAdmin = false;
            isFilteringByNotAdmin = false;
            SearchString = string.Empty;
            ApplyFilters();
            CommandManager.InvalidateRequerySuggested();
        }

        #endregion

        #region User Actions

        private bool CanAddUser(object obj) => true;
        private void AddUser(object obj) => throw new NotImplementedException();

        private bool CanEditUser(object obj) => true;
        private void EditUser(object obj) => throw new NotImplementedException();

        private bool CanDeleteUser(object obj) => SelectedUser != null;
        private void DeleteUser(object obj)
        {
            try
            {
                if (SelectedUser == null)
                {
                    MessageBox.Show("Please select a user to delete.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                AllUsers.Remove(SelectedUser);
                FilterUsers.Remove(SelectedUser);
                PagedUsers.Remove(SelectedUser);

                UpdatePagedUser();

                SelectedUser = PagedUsers.FirstOrDefault();

                SuccessMsgBody = $"User deleted.";
                SuccessMsgVisibility = Visibility.Visible;

            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while deleting the user: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #endregion

        #region Export

        private bool CanExport(object obj) => PagedUsers != null && PagedUsers.Count > 0;

        private void Export(object obj)
        {
            var sb = new StringBuilder();
            sb.AppendLine("Username,FirstName,LastName,IsAdmin");

            foreach (var user in FilterUsers)
                sb.AppendLine($"{Escape(user.Username)},{Escape(user.FirstName)},{Escape(user.LastName)},{Escape(user.IsAdmin.ToString())}");

            var saveFileDialog = new SaveFileDialog
            {
                FileName = "Users",
                DefaultExt = "csv",
                Filter = "CSV Files (*.csv)|*.csv"
            };

            bool? result = saveFileDialog.ShowDialog();
            if (result == true)
            {
                try
                {
                    File.WriteAllText(saveFileDialog.FileName, sb.ToString(), Encoding.UTF8);
                    SuccessMsgBody = $"Exported {FilterUsers.Count} users to {saveFileDialog.FileName}";
                }
                catch (Exception e)
                {
                    SuccessMsgBody = $"Error exporting users: {e.Message}";
                }
            }
            else
            {
                SuccessMsgBody = "Export cancelled.";
            }

            SuccessMsgVisibility = Visibility.Visible;
        }

        private string Escape(string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return "\"\"";
            return input.Contains(",") || input.Contains("\"") ? $"\"{input.Replace("\"", "\"\"")}\"" : input;
        }

        #endregion

        #region Misc

        private bool CanHideSuccessMsg(object obj) => true;
        private void HideSuccessMsg(object obj)
        {
            SuccessMsgVisibility = Visibility.Collapsed;
            SuccessMsgBody = string.Empty;
        }

        private bool CanLogout(object obj) => true;
        private void Logout(object obj)
        {
            // Returns login view
        }

        #endregion
    }
}
