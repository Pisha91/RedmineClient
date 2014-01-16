namespace RedmineClient.IocContainers
{
    using System.IO;

    using Windows.Storage;

    using Microsoft.Phone.Shell;

    using Ninject;
    using Ninject.Modules;

    using RedmineClient.Data;
    using RedmineClient.Models.Models.Issues;
    using RedmineClient.Proxy;
    using RedmineClient.Repositories.Abstract.DataBase;
    using RedmineClient.Repositories.Abstract.Service;
    using RedmineClient.Repositories.Implementation.DataBase;
    using RedmineClient.Repositories.Implementation.Service;
    using RedmineClient.ViewModels.ViewModel;

    /// <summary>
    /// The common module.
    /// </summary>
    public class CommonModule : NinjectModule
    {
        /// <summary>
        /// The load.
        /// </summary>
        public override void Load()
        {
            string connectionString = this.GetConnectionString();
            this.Bind<IDataBaseManager>().To<DataBaseManager>().WithConstructorArgument("connectionString", connectionString);
            this.Bind<IUserCredentialsRepository>().To<UserCredentialsRepository>();

            this.Bind<IWebClient>().To<WebClient>();
            this.Bind<IAccountRepository>().To<AccountRepository>();
            this.Bind<IIssueRepository>().To<IssueRepository>();
            this.Bind<IIssueStatusRepository>().To<IssueStatusRepository>();
            this.Bind<IPriorityRepository>().To<PriorityRepository>();
            this.Bind<IProjectRepository>().To<ProjectRepository>();

            this.Bind<MainViewModel>().ToSelf();
            this.Bind<LogOnViewModel>().ToSelf();
            this.Bind<IssueViewModel>().ToMethod(x => this.GetIssueViewModel());
        }

        /// <summary>
        /// The get connection string.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private string GetConnectionString()
        {
            var dataBasePath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "RedmineDataBase.sqlite");
            
            return dataBasePath;
        }

        /// <summary>
        /// The get issue view model.
        /// </summary>
        /// <returns>
        /// The <see cref="IssueViewModel"/>.
        /// </returns>
        private IssueViewModel GetIssueViewModel()
        {
            var issue = PhoneApplicationService.Current.State["selectedIssue"] as Issue;

            return new IssueViewModel(
                issue,
                this.Kernel.Get<IIssueRepository>(),
                this.Kernel.Get<IIssueStatusRepository>(),
                this.Kernel.Get<IAccountRepository>(),
                this.Kernel.Get<IPriorityRepository>());
        }
    }
}
