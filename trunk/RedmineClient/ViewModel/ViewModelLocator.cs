/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:RedmineClient"
                           x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

namespace RedmineClient.ViewModel
{
    using GalaSoft.MvvmLight;

    using Ninject;

    using RedmineClient.IocContainers;
    using RedmineClient.ViewModels.ViewModel;

    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        /// <summary>
        /// The kernel.
        /// </summary>
        private readonly IKernel kernel;

        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            var designTimeModule = new DesignTimeModule();
            var runTimeModule = new RunTimeModule();
            this.kernel = ViewModelBase.IsInDesignModeStatic ? new StandardKernel(designTimeModule) : new StandardKernel(runTimeModule);
        }

        /// <summary>
        /// Gets the main.
        /// </summary>
        public MainViewModel Main
        {
            get
            {
                return this.kernel.Get<MainViewModel>();
            }
        }

        /// <summary>
        /// Gets the log on.
        /// </summary>
        public LogOnViewModel LogOn
        {
            get
            {
                return this.kernel.Get<LogOnViewModel>();
            }
        }

        /// <summary>
        /// Gets the issue.
        /// </summary>
        public IssueViewModel Issue
        {
            get
            {
                return this.kernel.Get<IssueViewModel>();
            }
        }

        public static void Cleanup()
        {
        }
    }
}