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
    using RedmineClient.Models.Repository;
    using RedmineClient.Repositories.Abstract.Service;

    /// <summary>
    /// The project view model.
    /// </summary>
    public class ProjectViewModel : ViewModelBase
    {
        /// <summary>
        /// The issue repository.
        /// </summary>
        private readonly IIssueRepository issueRepository;

        /// <summary>
        /// The selected project.
        /// </summary>
        private Project selectedProject;

        /// <summary>
        /// The unauthorize.
        /// </summary>
        private bool unauthorized;

        /// <summary>
        /// The loading issues.
        /// </summary>
        private bool loadingIssues;

        /// <summary>
        /// The limit.
        /// </summary>
        private int limit;

        /// <summary>
        /// The loaded issues count.
        /// </summary>
        private int loadedIssuesCount;

        /// <summary>
        /// The issues total count.
        /// </summary>
        private int issuesTotalCount;

        /// <summary>
        /// The issues.
        /// </summary>
        private ObservableCollection<Issue> issues;

        /// <summary>
        /// The issue tap command.
        /// </summary>
        private RelayCommand<Issue> issueTapCommand;

        /// <summary>
        /// The issues item realized.
        /// </summary>
        private RelayCommand<ItemRealizationEventArgs> issuesItemRealized;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectViewModel"/> class.
        /// </summary>
        /// <param name="selectedProject">
        /// The selected project.
        /// </param>
        /// <param name="issueRepository">
        /// The issue Repository.
        /// </param>
        public ProjectViewModel(Project selectedProject, IIssueRepository issueRepository)
        {
            this.selectedProject = selectedProject;
            this.issueRepository = issueRepository;
            this.SetLoadingParameters();
            this.GetIssues();
        }

        /// <summary>
        /// Gets a value indicating whether show progress bar.
        /// </summary>
        public bool ShowProgressBar
        {
            get
            {
                return this.loadingIssues;
            }
        }

        /// <summary>
        /// Gets or sets the selected project.
        /// </summary>
        public Project SelectedProject
        {
            get
            {
                if (this.unauthorized)
                {
                    Messenger.Default.Send(new LogOnMessage(this, new Uri("/LogOnPage.xaml", UriKind.Relative)));
                }

                return this.selectedProject;
            }

            set
            {
                this.selectedProject = value;
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
        /// Gets the issues item realized command.
        /// </summary>
        public ICommand IssuesItemRealizedCommand
        {
            get
            {
                return this.issuesItemRealized
                       ?? (this.issuesItemRealized = new RelayCommand<ItemRealizationEventArgs>(this.IssuesItemRealized));
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
                   if (this.issues.IndexOf(this.issues.First(x => x.Id == issue.Id)) >= this.issues.Count - this.issues.Count / 2)
                    {
                        this.GetIssues();
                    }
                }
            }
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
        /// The set issues.
        /// </summary>
        private async void GetIssues()
        {
            this.loadingIssues = true;
            this.RaisePropertyChanged("ShowProgressBar");

            RepositoryResponse<List<Issue>> issuesResponse = await this.issueRepository.GetIssuesByProjectId(this.selectedProject.Id, this.limit, this.loadedIssuesCount);
            if (issuesResponse.StatusCode == HttpStatusCode.OK)
            {
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
            else if (issuesResponse.StatusCode == HttpStatusCode.Unauthorized)
            {
                this.unauthorized = true;
            }
            else
            {
                MessageBox.Show(issuesResponse.Message);
            }

            this.loadingIssues = false;
            this.RaisePropertyChanged("Issues");
            this.RaisePropertyChanged("ShowProgressBar");
        }

        /// <summary>
        /// The set loading parameters.
        /// </summary>
        private void SetLoadingParameters()
        {
            this.limit = 25;
            this.loadedIssuesCount = 0;
            this.issuesTotalCount = 25;
        }
    }
}
