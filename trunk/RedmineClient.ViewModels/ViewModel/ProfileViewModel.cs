namespace RedmineClient.ViewModels.ViewModel
{
    using System;
    using System.Net;
    using System.ServiceModel.Channels;
    using System.Windows;

    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Messaging;

    using RedmineClient.Messanger.Messages.LogOn;
    using RedmineClient.Models.Models.Users;
    using RedmineClient.Models.Repository;
    using RedmineClient.Repositories.Abstract.DataBase;
    using RedmineClient.Repositories.Abstract.Service;

    /// <summary>
    /// The profile view model.
    /// </summary>
    public class ProfileViewModel : ViewModelBase
    {
        /// <summary>
        /// The user credentials repository.
        /// </summary>
        private readonly IUserCredentialsRepository userCredentialsRepository;

        /// <summary>
        /// The account repository.
        /// </summary>
        private readonly IAccountRepository accountRepository;

        /// <summary>
        /// The profile.
        /// </summary>
        private User profile;

        /// <summary>
        /// The unauthorized.
        /// </summary>
        private bool unauthorized;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProfileViewModel"/> class.
        /// </summary>
        /// <param name="userCredentialsRepository">
        /// The user credentials repository.
        /// </param>
        /// <param name="accountRepository">
        /// The account repository.
        /// </param>
        public ProfileViewModel(IUserCredentialsRepository userCredentialsRepository, IAccountRepository accountRepository)
        {
            this.userCredentialsRepository = userCredentialsRepository;
            this.accountRepository = accountRepository;
            this.SetProfile();
        }

        /// <summary>
        /// Gets or sets the profile.
        /// </summary>
        public User Profile
        {
            get
            {
                if (this.unauthorized)
                {
                    Messenger.Default.Send(new LogOnMessage(this, new Uri("/LogOnPage.xaml", UriKind.Relative)));
                }

                return this.profile;
            }

            set
            {
                this.profile = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether show progress bar.
        /// </summary>
        public bool ShowProgressBar { get; set; }

        /// <summary>
        /// The set profile.
        /// </summary>
        private async void SetProfile()
        {
            this.ShowProgressBar = true;
            this.RaisePropertyChanged("ShowProgressBar");
            RepositoryResponse<User> profileResponse = await this.accountRepository.GetCurrentUser();
            if (profileResponse.StatusCode == HttpStatusCode.OK)
            {
                this.Profile = profileResponse.ResponseObject;
            }
            else if (profileResponse.StatusCode == HttpStatusCode.Unauthorized)
            {
                this.unauthorized = true;
            }
            else
            {
                MessageBox.Show(string.Format("{0}, {1}", profileResponse.StatusCode, profileResponse.Message));
            }

            this.ShowProgressBar = false;
            this.RaisePropertyChanged("ShowProgressBar");
            this.RaisePropertyChanged("Profile");
        }
    }
}
