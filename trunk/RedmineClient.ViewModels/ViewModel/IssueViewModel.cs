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

    using Microsoft.Phone.Tasks;

    using RedmineClient.Messanger.Messages.LogOn;
    using RedmineClient.Models.Models.Attachments;
    using RedmineClient.Models.Models.Issues;
    using RedmineClient.Models.Models.Journal;
    using RedmineClient.Models.Models.Property;
    using RedmineClient.Models.Models.Status;
    using RedmineClient.Models.Models.Users;
    using RedmineClient.Models.Repository;
    using RedmineClient.Repositories.Abstract.DataBase;
    using RedmineClient.Repositories.Abstract.Service;

    /// <summary>
    /// The issue view model.
    /// </summary>
    public class IssueViewModel : ViewModelBase
    {
        /// <summary>
        /// The issue repository.
        /// </summary>
        private readonly IIssueRepository issueRepository;

        /// <summary>
        /// The membership repository.
        /// </summary>
        private readonly IIssueStatusRepository issueStatusRepository;

        /// <summary>
        /// The priority repository.
        /// </summary>
        private readonly IPriorityRepository priorityRepository;

        /// <summary>
        /// The account repository.
        /// </summary>
        private readonly IAccountRepository accountRepository;

        /// <summary>
        /// The user credentials repository.
        /// </summary>
        private readonly IUserCredentialsRepository userCredentialsRepository;

        /// <summary>
        /// The unauthorized.
        /// </summary>
        private bool unauthorized;

        /// <summary>
        /// The loading journal.
        /// </summary>
        private bool loadingJournal;

        /// <summary>
        /// The loading statuses.
        /// </summary>
        private bool loadingStatuses;

        /// <summary>
        /// The loading users.
        /// </summary>
        private bool loadingUsers;

        /// <summary>
        /// The loading priorities.
        /// </summary>
        private bool loadingPriorities;

        /// <summary>
        /// The selected issue.
        /// </summary>
        private Issue selectedIssue;

        /// <summary>
        /// The attachment tap command.
        /// </summary>
        private RelayCommand<Attachment> attachmentTapCommand;

        /// <summary>
        /// Initializes a new instance of the <see cref="IssueViewModel"/> class.
        /// </summary>
        /// <param name="selectedIssue">
        /// The selected Issue.
        /// </param>
        /// <param name="issueRepository">
        /// The issue repository.
        /// </param>
        /// <param name="issueStatusRepository">
        /// The membership repository.
        /// </param>
        /// <param name="accountRepository">
        /// The account repository.
        /// </param>
        /// <param name="priorityRepository">
        /// The priority repository
        /// </param>
        /// <param name="userCredentialsRepository">
        /// The user Credentials Repository.
        /// </param>
        public IssueViewModel(
            Issue selectedIssue, 
            IIssueRepository issueRepository, 
            IIssueStatusRepository issueStatusRepository, 
            IAccountRepository accountRepository, 
            IPriorityRepository priorityRepository, 
            IUserCredentialsRepository userCredentialsRepository)
        {
            this.selectedIssue = selectedIssue;
            this.issueRepository = issueRepository;
            this.issueStatusRepository = issueStatusRepository;
            this.accountRepository = accountRepository;
            this.priorityRepository = priorityRepository;
            this.userCredentialsRepository = userCredentialsRepository;

            this.SetLoadingParameters();
            this.UpdateIssueWithHistory();
        }

        /// <summary>
        /// Gets a value indicating whether show progress bar.
        /// </summary>
        public bool ShowProgressBar
        {
            get
            {
                return this.loadingJournal || this.loadingStatuses || this.loadingUsers || this.loadingPriorities;
            }
        }

        /// <summary>
        /// Gets or sets the selected issue.
        /// </summary>
        public Issue SelectedIssue
        {
            get
            {
                if (this.unauthorized)
                {
                    Messenger.Default.Send(new LogOnMessage(this, new Uri("/LogOnPage.xaml", UriKind.Relative)));
                }

                return this.selectedIssue;
            }

            set
            {
                this.selectedIssue = value;
                this.RaisePropertyChanged("SelectedIssue");
            }
        }

        /// <summary>
        /// Gets or sets the statuses.
        /// </summary>
        public List<Status> Statuses { get; set; }

        /// <summary>
        /// Gets or sets the priorities.
        /// </summary>
        public List<Priority> Priorities { get; set; }

        /// <summary>
        /// Gets or sets the users.
        /// </summary>
        public List<User> Users { get; set; }

        /// <summary>
        /// Gets or sets the history.
        /// </summary>
        public ObservableCollection<JournalItem> History { get; set; }

        /// <summary>
        /// Gets or sets the attachments.
        /// </summary>
        public ObservableCollection<Attachment> Attachments { get; set; }

        /// <summary>
        /// Gets the attachment tap command.
        /// </summary>
        public ICommand AttachmentTapCommand
        {
            get
            {
                return this.attachmentTapCommand ?? (this.attachmentTapCommand = new RelayCommand<Attachment>(this.AttachmentTap));
            }
        }

        /// <summary>
        /// The attachment tap.
        /// </summary>
        /// <param name="attachment">
        /// The attachment.
        /// </param>
        private void AttachmentTap(Attachment attachment)
        {
           string host = this.userCredentialsRepository.Get().Host;
            string oldHost = attachment.ContentUrl.Substring(8, attachment.ContentUrl.Length - 8);
            string url = attachment.ContentUrl.Substring(8 + oldHost.IndexOf("/", StringComparison.Ordinal), attachment.ContentUrl.Length - 8 - oldHost.IndexOf("/", StringComparison.Ordinal));
            var webBrowserTask = new WebBrowserTask { Uri = new Uri(host + url, UriKind.Absolute) };
            webBrowserTask.Show();

        }

        /// <summary>
        /// The update issue with history.
        /// </summary>
        private async void UpdateIssueWithHistory()
        {
            RepositoryResponse<Issue> issueResponse = await this.issueRepository.GetIssue(this.selectedIssue.Id);
            if (issueResponse.StatusCode == HttpStatusCode.OK)
            {
                this.SelectedIssue = issueResponse.ResponseObject;
                this.SelectedIssue.Journals.Reverse();
                this.Attachments = new ObservableCollection<Attachment>(issueResponse.ResponseObject.Attachments);
                this.LoadUsers();
                this.LoadStatuses();
                this.LoadPriorities();
            }
            else if (issueResponse.StatusCode == HttpStatusCode.Unauthorized)
            {
                this.unauthorized = true;
            }
            else
            {
                MessageBox.Show(issueResponse.Message);
            }

            this.loadingJournal = false;
            this.RaisePropertyChanged("ShowProgressBar");
            this.RaisePropertyChanged("Attachments");
        }

        /// <summary>
        /// The load statuses.
        /// </summary>
        private async void LoadStatuses()
        {
            RepositoryResponse<List<Status>> issueStatusesResponse = await this.issueStatusRepository.GetStatuses();
            if (issueStatusesResponse.StatusCode == HttpStatusCode.OK)
            {
                this.Statuses = issueStatusesResponse.ResponseObject;
                foreach (var journalItem in this.selectedIssue.Journals)
                {
                    foreach (var journalDetailse in journalItem.Details.Where(x => x.Name == "status_id"))
                    {
                        journalDetailse.OldStatus = this.Statuses.FirstOrDefault(x => x.Id == int.Parse(journalDetailse.OldValue)).Name;
                        journalDetailse.NewStatus = this.Statuses.FirstOrDefault(x => x.Id == int.Parse(journalDetailse.NewValue)).Name;
                    }
                }
            }
            else if (issueStatusesResponse.StatusCode == HttpStatusCode.Unauthorized)
            {
                this.unauthorized = true;
            }
            else
            {
                MessageBox.Show(string.Format("{0} - {1}", issueStatusesResponse.StatusCode, issueStatusesResponse.Message));
            }

            this.History = new ObservableCollection<JournalItem>(this.selectedIssue.Journals);

            this.loadingStatuses = false;
            this.UpdateUiHistory();
        }

        /// <summary>
        /// The load users.
        /// </summary>
        private async void LoadUsers()
        {
            this.Users = new List<User>();
            foreach (var journalItem in this.selectedIssue.Journals)
            {
                foreach (var journalDetailse in journalItem.Details.Where(x => x.Name == "assigned_to_id").ToList())
                {
                    int oldUserId;
                    int newUserId;
                    if (int.TryParse(journalDetailse.OldValue, out oldUserId))
                    {
                        User oldUser = this.Users.FirstOrDefault(x => x.Id == oldUserId);
                        if (oldUser == null)
                        {
                            RepositoryResponse<User> userResponse = await this.accountRepository.GetUser(oldUserId);
                            if (userResponse.StatusCode == HttpStatusCode.OK)
                            {
                                this.Users.Add(userResponse.ResponseObject);
                                journalDetailse.OldAssignName = string.Format("{1} {0}", userResponse.ResponseObject.LastName, userResponse.ResponseObject.FirstName);
                            }
                            else if (userResponse.StatusCode == HttpStatusCode.Unauthorized)
                            {
                                this.unauthorized = true;
                                this.RaisePropertyChanged("SelectedIssue");
                            }
                            else
                            {
                                MessageBox.Show(string.Format("{0} - {1}", userResponse.StatusCode, userResponse.Message));
                            }
                        }
                        else
                        {
                            journalDetailse.OldAssignName = string.Format("{1} {0}", oldUser.LastName, oldUser.FirstName);
                        }
                    }

                    if (int.TryParse(journalDetailse.NewValue, out newUserId))
                    {
                        User newUser = this.Users.FirstOrDefault(x => x.Id == newUserId);
                        if (newUser == null)
                        {
                            RepositoryResponse<User> userResponse = await this.accountRepository.GetUser(newUserId);
                            if (userResponse.StatusCode == HttpStatusCode.OK)
                            {
                                this.Users.Add(userResponse.ResponseObject);
                                journalDetailse.NewAssignName = string.Format("{1} {0}", userResponse.ResponseObject.LastName, userResponse.ResponseObject.FirstName);
                            }
                            else if (userResponse.StatusCode == HttpStatusCode.Unauthorized)
                            {
                                this.unauthorized = true;
                                this.RaisePropertyChanged("SelectedIssue");
                            }
                            else
                            {
                                MessageBox.Show(string.Format("{0} - {1}", userResponse.StatusCode, userResponse.Message));
                            }
                        }
                        else
                        {
                            journalDetailse.NewAssignName = string.Format("{1} {0}", newUser.LastName, newUser.FirstName);
                        }
                    }
                }
            }

            this.History = new ObservableCollection<JournalItem>(this.selectedIssue.Journals);

            this.loadingUsers = false;
            this.UpdateUiHistory();
        }

        /// <summary>
        /// The load priorities.
        /// </summary>
        private async void LoadPriorities()
        {
            RepositoryResponse<List<Priority>> priorityResponse = await this.priorityRepository.GetPriorities();
            if (priorityResponse.StatusCode == HttpStatusCode.OK)
            {
                this.Priorities = priorityResponse.ResponseObject;
                foreach (var journalItem in this.selectedIssue.Journals)
                {
                    foreach (var journalDetailse in journalItem.Details.Where(x => x.Name == "priority_id"))
                    {
                        journalDetailse.OldPriority = this.Priorities.FirstOrDefault(x => x.Id == int.Parse(journalDetailse.OldValue)).Name;
                        journalDetailse.NewPriority = this.Priorities.FirstOrDefault(x => x.Id == int.Parse(journalDetailse.NewValue)).Name;
                    }
                }
            }
            else if (priorityResponse.StatusCode == HttpStatusCode.Unauthorized)
            {
                this.unauthorized = true;
            }
            else
            {
                MessageBox.Show(string.Format("{0} - {1}", priorityResponse.StatusCode, priorityResponse.Message));
            }

            this.History = new ObservableCollection<JournalItem>(this.selectedIssue.Journals);

            this.loadingPriorities = false;
            this.UpdateUiHistory();
        }

        /// <summary>
        /// The update user interface history.
        /// </summary>
        private void UpdateUiHistory()
        {
            if (!this.ShowProgressBar)
            {
                this.RaisePropertyChanged("History");
                this.RaisePropertyChanged("ShowProgressBar");
            }
        }

        /// <summary>
        /// The set loading parameters.
        /// </summary>
        private void SetLoadingParameters()
        {
            this.loadingJournal = true;
            this.loadingStatuses = true;
            this.loadingUsers = true;
            this.loadingPriorities = true;
        }
    }
}
