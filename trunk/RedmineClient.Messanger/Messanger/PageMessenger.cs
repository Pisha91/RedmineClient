namespace RedmineClient.Messanger.Messanger
{
    using GalaSoft.MvvmLight.Messaging;

    using Microsoft.Phone.Controls;
    using Microsoft.Phone.Shell;

    using RedmineClient.Messanger.Messages.Issue;
    using RedmineClient.Messanger.Messages.LogOn;
    using RedmineClient.Messanger.Messages.Main;

    /// <summary>
    /// The page messenger.
    /// </summary>
    public class PageMessenger
    {
        /// <summary>
        /// The root frame.
        /// </summary>
        private readonly PhoneApplicationFrame rootFrame;

        /// <summary>
        /// Initializes a new instance of the <see cref="PageMessenger"/> class.
        /// </summary>
        /// <param name="rootFrame">
        /// The root frame.
        /// </param>
        public PageMessenger(PhoneApplicationFrame rootFrame)
        {
            this.rootFrame = rootFrame;
        }

        /// <summary>
        /// The initialize main content messengers.
        /// </summary>
        public void InitializeMainContentMessengers()
        {
            Messenger.Default.Register<MainMessage>(this, message => this.rootFrame.Navigate(message.Uri));
            Messenger.Default.Register<LogOnMessage>(this, message => this.rootFrame.Navigate(message.Uri));

            Messenger.Default.Register<IssueMessage>(
                this,
                message =>
                    {
                        PhoneApplicationService.Current.State["selectedIssue"] = message.SelectedIssue;
                        this.rootFrame.Navigate(message.Uri);
                    });
        }
    }
}
