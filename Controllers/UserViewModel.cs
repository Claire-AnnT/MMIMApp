using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MMIMApp.Controllers
{
    public class UserViewModel : ViewModelBase
    {
        public class AddUserViewModel : UserViewModel
        {
            private string username = null!;
            public string Username
            {
                get => username;
                set
                {
                    username = value;
                    OnPropertyChanged(nameof(Username));
                }
            }

            private string password = null!;
            public string Password
            {
                get => password;
                set
                {
                    password = value;
                    OnPropertyChanged(nameof(Password));
                }
            }

            private string firstName = null!;
            public string FirstName
            {
                get => firstName;
                set
                {
                    firstName = value;
                    OnPropertyChanged(nameof(FirstName));
                }
            }

            private string lastName = null!;
            public string LastName
            {
                get => lastName;
                set
                {
                    lastName = value;
                    OnPropertyChanged(nameof(LastName));
                }
            }

            private bool isAdmin;
            public bool IsAdmin
            {
                get => isAdmin;
                set
                {
                    isAdmin = value;
                    OnPropertyChanged(nameof(IsAdmin));
                }
            }

            public ICommand AddCommand { get; } = null!;
            public ICommand CancelCommand { get; } = null!;

            public AddUserViewModel()
            {

            }
        }

        public class EditUserViewModel : UserViewModel
        {
            private string username = null!;
            public string Username
            {
                get => username;
                set
                {
                    username = value;
                    OnPropertyChanged(nameof(Username));
                }
            }
            private string firstName = null!;
            public string FirstName
            {
                get => firstName;
                set
                {
                    firstName = value;
                    OnPropertyChanged(nameof(FirstName));
                }
            }
            private string lastName = null!;
            public string LastName
            {
                get => lastName;
                set
                {
                    lastName = value;
                    OnPropertyChanged(nameof(LastName));
                }
            }
            private bool isAdmin;
            public bool IsAdmin
            {
                get => isAdmin;
                set
                {
                    isAdmin = value;
                    OnPropertyChanged(nameof(IsAdmin));
                }
            }
            public ICommand SaveCommand { get; } = null!;
            public ICommand CancelCommand { get; } = null!;
            public EditUserViewModel()
            {
            }
        }

        public class LoginViewModel : UserViewModel
        {
            private string username = null!;
            public string Username
            {
                get => username;
                set
                {
                    username = value;
                    OnPropertyChanged(nameof(Username));
                }
            }

            private string password = null!;
            public string Password
            {
                get => password;
                set
                {
                    password = value;
                    OnPropertyChanged(nameof(Password));
                }
            }

            public ICommand LoginCommand { get; } = null!;
            public ICommand CreateAccountCommand { get; } = null!;

            public LoginViewModel()
            {

            }
        }

        public class CreateAccountViewModel : UserViewModel
        {
            private string username = null!;
            public string Username
            {
                get => username;
                set
                {
                    username = value;
                    OnPropertyChanged(nameof(Username));
                }
            }
            private string password = null!;
            public string Password
            {
                get => password;
                set
                {
                    password = value;
                    OnPropertyChanged(nameof(Password));
                }
            }

            private string firstName = null!;
            public string FirstName
            {
                get => firstName;
                set
                {
                    firstName = value;
                    OnPropertyChanged(nameof(FirstName));
                }
            }

            private string lastName = null!;
            public string LastName
            {
                get => lastName;
                set
                {
                    lastName = value;
                    OnPropertyChanged(nameof(LastName));
                }
            }

            public ICommand CreateAccountCommand { get; } = null!;
            public ICommand LoginCommand { get; } = null!;
        }

     }
}
