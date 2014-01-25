namespace RedmineClient.ViewModels.ViewModel
{
    using System;
    using System.Windows.Input;

    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Command;
    using GalaSoft.MvvmLight.Messaging;

    using RedmineClient.Messanger.Messages.Main;
    using RedmineClient.Repositories.Abstract.Service;

    /// <summary>
    /// The log on view model.
    /// </summary>
    public class LogOnViewModel : ViewModelBase
    {
        /// <summary>
        /// The account repository.
        /// </summary>
        private readonly IAccountRepository accountRepository;

        /// <summary>
        /// The log in command.
        /// </summary>
        private RelayCommand logInCommand;

        /// <summary>
        /// The host.
        /// </summary>
        private string host;

        /// <summary>
        /// The username.
        /// </summary>
        private string username;

        /// <summary>
        /// The password.
        /// </summary>
        private string password;

        /// <summary>
        /// Initializes a new instance of the <see cref="LogOnViewModel"/> class.
        /// </summary>
        /// <param name="accountRepository">
        /// The account repository.
        /// </param>
        public LogOnViewModel(IAccountRepository accountRepository)
        {
            this.accountRepository = accountRepository;
        }

        /// <summary>
        /// Gets or sets the host.
        /// </summary>
        public string Host
        {
            get
            {
                return this.host;
            }

            set
            {
                this.host = value;
                this.RaisePropertyChanged("CanLogIn");
            }
        }

        /// <summary>
        /// Gets or sets the user name.
        /// </summary>
        public string Username
        {
            get
            {
                return this.username;
            }

            set
            {
                this.username = value;
                this.RaisePropertyChanged("CanLogIn");
            }
        }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        public string Password
        {
            get
            {
                return this.password;
            }

            set
            {
                this.password = value;
                this.RaisePropertyChanged("CanLogIn");
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether show progress bar.
        /// </summary>
        public bool ShowProgressBar { get; set; }

        /// <summary>
        /// Gets a value indicating whether can log in.
        /// </summary>
        public bool CanLogIn
        {
            get
            {
                return !string.IsNullOrEmpty(this.Host) && !string.IsNullOrEmpty(this.Username) && !string.IsNullOrEmpty(this.Password);
            }
        }

        /// <summary>
        /// Gets the log in command.
        /// </summary>
        public ICommand LogInCommand
        {
            get
            {
                return this.logInCommand ?? (this.logInCommand = new RelayCommand(this.LogIn));
            }
        }

        /// <summary>
        /// The log in.
        /// </summary>
        public async void LogIn()
        {
            this.ShowProgressBar = true;
            this.RaisePropertyChanged("ShowProgressBar");
            bool result = await this.accountRepository.LogIn(this.Host, this.Username, this.Password);
            
            if (result)
            {
                Messenger.Default.Send(new MainMessage(this, new Uri("/MainPage.xaml", UriKind.Relative)));
            }
            this.ShowProgressBar = false;
            this.RaisePropertyChanged("ShowProgressBar");
        }
    }
}
