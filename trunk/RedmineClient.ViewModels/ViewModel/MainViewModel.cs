namespace RedmineClient.ViewModels.ViewModel
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Net;
    using System.Windows;
    using System.Windows.Input;

    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Command;
    using GalaSoft.MvvmLight.Messaging;

    using RedmineClient.Messanger.Messages.Issue;
    using RedmineClient.Messanger.Messages.LogOn;
    using RedmineClient.Models.Models.Issues;
    using RedmineClient.Models.Models.Projects;
    using RedmineClient.Models.Models.Users;
    using RedmineClient.Models.Repository;
    using RedmineClient.Repositories.Abstract.Service;

    /// <summary>
    /// The main view model.
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        /// <summary>
        /// The issue repository.
        /// </summary>
        private readonly IIssueRepository issueRepository;

        /// <summary>
        /// The project repository.
        /// </summary>
        private readonly IProjectRepository projectRepository;

        /// <summary>
        /// The account repository.
        /// </summary>
        private readonly IAccountRepository accountRepository;

        /// <summary>
        /// The unauthorized.
        /// </summary>
        private bool unauthorized;

        /// <summary>
        /// The loading issues.
        /// </summary>
        private bool loadingIssues;

        /// <summary>
        /// The loading projects.
        /// </summary>
        private bool loadingProjects;

        /// <summary>
        /// The issues.
        /// </summary>
        private ObservableCollection<Issue> issues;

        /// <summary>
        /// The projects.
        /// </summary>
        private ObservableCollection<Project> projects; 

        /// <summary>
        /// The issue tap command.
        /// </summary>
        private RelayCommand<Issue> issueTapCommand;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainViewModel"/> class.
        /// </summary>
        /// <param name="issueRepository">
        /// The issue repository.
        /// </param>
        /// <param name="accountRepository">
        /// The account repository.
        /// </param>
        /// <param name="projectRepository">
        /// The project repository.
        /// </param>
        public MainViewModel(
            IIssueRepository issueRepository,
            IAccountRepository accountRepository,
            IProjectRepository projectRepository)
        {
            this.issueRepository = issueRepository;
            this.accountRepository = accountRepository;
            this.projectRepository = projectRepository;
            this.unauthorized = false;

            this.SetLoadingParameters();
            this.GetIssues();
            this.GetProjects();
        }

        /// <summary>
        /// Gets the issue tap command.
        /// </summary>
        public ICommand IssueTapCommand
        {
            get
            {
                return this.issueTapCommand ?? (this.issueTapCommand = new RelayCommand<Issue>(this.IssueTap));
            }
        }

        /// <summary>
        /// Gets a value indicating whether show progress bar.
        /// </summary>
        public bool ShowProgressBar
        {
            get
            {
                return this.loadingProjects || this.loadingIssues;
            }
        }

        /// <summary>
        /// Gets or sets the issues.
        /// </summary>
        public ObservableCollection<Issue> Issues
        {
            get
            {
                if (this.unauthorized)
                {
                    Messenger.Default.Send(new LogOnMessage(this, new Uri("/LogOnPage.xaml", UriKind.Relative)));
                }

                return this.issues;
            }

            set
            {
                this.issues = value;
            }
        }

        /// <summary>
        /// Gets or sets the projects.
        /// </summary>
        public ObservableCollection<Project> Projects
        {
            get
            {
                if (this.unauthorized)
                {
                    Messenger.Default.Send(new LogOnMessage(this, new Uri("/LogOnPage.xaml", UriKind.Relative)));
                }

                return this.projects;
            }

            set
            {
                this.projects = value;
            }
        }

        /// <summary>
        /// The set issues.
        /// </summary>
        private async void GetIssues()
        {
            RepositoryResponse<User> userResponse = await this.accountRepository.GetCurrentUser();
            if (userResponse.StatusCode == HttpStatusCode.OK)
            {
                RepositoryResponse<List<Issue>> issuesResponse = await this.issueRepository.GetIssues(userResponse.ResponseObject.Id);
                this.issues = new ObservableCollection<Issue>(issuesResponse.ResponseObject);
            }
            else if (userResponse.StatusCode == HttpStatusCode.Unauthorized)
            {
                this.unauthorized = true;
            }
            else
            {
                MessageBox.Show(userResponse.Message);
            }

            this.loadingIssues = false;
            this.RaisePropertyChanged("Issues");
            this.RaisePropertyChanged("ShowProgressBar");
        }

        /// <summary>
        /// The get projects.
        /// </summary>
        private async void GetProjects()
        {
            RepositoryResponse<List<Project>> projectResponse = await this.projectRepository.GetProjects();
            if (projectResponse.StatusCode == HttpStatusCode.OK)
            {
                this.projects = new ObservableCollection<Project>(projectResponse.ResponseObject);
            }
            else if (projectResponse.StatusCode == HttpStatusCode.Unauthorized)
            {
                this.unauthorized = true;
            }
            else
            {
                MessageBox.Show(projectResponse.Message);
            }

            this.loadingProjects = false;
            this.RaisePropertyChanged("Projects");
            this.RaisePropertyChanged("ShowProgressBar");
        }

        /// <summary>
        /// The issue tap.
        /// </summary>
        /// <param name="selectedIssue">
        /// The selected issue.
        /// </param>
        private void IssueTap(Issue selectedIssue)
        {
            Messenger.Default.Send(new IssueMessage(selectedIssue, new Uri("/IssuePage.xaml", UriKind.Relative)));
        }

        /// <summary>
        /// The set loading parameters.
        /// </summary>
        private void SetLoadingParameters()
        {
            this.loadingIssues = true;
            this.loadingProjects = true;
        }
    }
}