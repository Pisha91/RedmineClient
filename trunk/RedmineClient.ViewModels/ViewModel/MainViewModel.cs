namespace RedmineClient.ViewModels.ViewModel
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Net;
    using System.Windows;
    using System.Windows.Input;

    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Command;
    using GalaSoft.MvvmLight.Messaging;

    using Microsoft.Phone.Controls;

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
        /// The limit.
        /// </summary>
        private int limit;

        /// <summary>
        /// The loaded Issues.
        /// </summary>
        private int loadedIssuesCount;

        /// <summary>
        /// The total issues count.
        /// </summary>
        private int issuesTotalCount;

        /// <summary>
        /// The loaded projects.
        /// </summary>
        private int loadedProjectsCount;

        /// <summary>
        /// The total projects count.
        /// </summary>
        private int projectsTotalCount;

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
        /// The project item realized.
        /// </summary>
        private RelayCommand<ItemRealizationEventArgs> projectsItemRealized;

        /// <summary>
        /// The issues item realized.
        /// </summary>
        private RelayCommand<ItemRealizationEventArgs> issuesItemRealized;

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
        /// Gets the news item realized command.
        /// </summary>
        public ICommand ProjectsItemRealizedCommand
        {
            get
            {
                return this.projectsItemRealized
                       ?? (this.projectsItemRealized = new RelayCommand<ItemRealizationEventArgs>(this.ProjectsItemRealized));
            }
        }

        /// <summary>
        /// Gets the issues item realized command.
        /// </summary>
        public ICommand IssuesItemRealizedCommand
        {
            get
            {
                return this.IssuesItemRealizedCommand
                       ?? (this.issuesItemRealized = new RelayCommand<ItemRealizationEventArgs>(this.IssuesItemRealized));
            }
        }

        /// <summary>
        /// The projects item realized.
        /// </summary>
        /// <param name="eventArgs">
        /// The event args.
        /// </param>
        private void ProjectsItemRealized(ItemRealizationEventArgs eventArgs)
        {
            if (this.projects != null && this.projectsTotalCount > this.projects.Count && !this.loadingProjects)
            {
                if (eventArgs.ItemKind == LongListSelectorItemKind.Item)
                {
                    var newsDisplayModel = eventArgs.Container.Content as Project;
                    if (this.projects.IndexOf(newsDisplayModel) > this.Projects.Count - this.Projects.Count / 2)
                    {
                        this.GetProjects();
                    }
                }
            }
        }

        /// <summary>
        /// The issues item realized.
        /// </summary>
        /// <param name="eventArgs">
        /// The event args.
        /// </param>
        private void IssuesItemRealized(ItemRealizationEventArgs eventArgs)
        {
            if (this.issues != null && this.issuesTotalCount > this.issues.Count && !this.loadingIssues)
            {
                if (eventArgs.ItemKind == LongListSelectorItemKind.Item)
                {
                    var issue = eventArgs.Container.Content as Issue;
                    if (this.issues.IndexOf(issue) > this.issues.Count - this.issues.Count / 2)
                    {
                        this.GetIssues();
                    }
                }
            }
        }

        /// <summary>
        /// The set issues.
        /// </summary>
        private async void GetIssues()
        {
            this.loadingIssues = true;
            RepositoryResponse<User> userResponse = await this.accountRepository.GetCurrentUser();
            if (userResponse.StatusCode == HttpStatusCode.OK)
            {
                RepositoryResponse<List<Issue>> issuesResponse = await this.issueRepository.GetIssues(userResponse.ResponseObject.Id, this.limit, this.loadedIssuesCount);
                if (this.issues == null)
                {
                    this.issues = new ObservableCollection<Issue>(issuesResponse.ResponseObject);
                    this.issuesTotalCount = issuesResponse.TotalCount;
                }
                else
                {
                    foreach (var issue in issuesResponse.ResponseObject)
                    {
                        this.issues.Add(issue);
                    }
                }

                this.loadedIssuesCount = this.issues.Count;
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
            this.loadingProjects = true;
            RepositoryResponse<List<Project>> projectResponse = await this.projectRepository.GetProjects(this.limit, this.loadedProjectsCount);
            if (projectResponse.StatusCode == HttpStatusCode.OK)
            {
                if (this.projects == null)
                {
                    this.projects = new ObservableCollection<Project>(projectResponse.ResponseObject);
                    this.projectsTotalCount = projectResponse.TotalCount;
                }
                else
                {
                    foreach (var project in projectResponse.ResponseObject)
                    {
                        this.projects.Add(project);
                    }
                }

                this.loadedProjectsCount = this.projects.Count;
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
            this.limit = 25;
            this.loadedIssuesCount = 0;
            this.loadedProjectsCount = 0;
            this.projectsTotalCount = 25;
            this.issuesTotalCount = 25;
        }
    }
}