namespace RedmineClient.Messanger.Messages.Issue
{
    using System;

    using RedmineClient.Messanger.Messages.Base;

    /// <summary>
    /// The issue message.
    /// </summary>
    public class IssueMessage : BaseMessage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IssueMessage"/> class.
        /// </summary>
        /// <param name="content">
        /// The content.
        /// </param>
        /// <param name="uri">
        /// The uri.
        /// </param>
        public IssueMessage(object content, Uri uri)
            : base(content, uri)
        {
            this.SelectedIssue = content;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="IssueMessage"/> class.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="content">
        /// The content.
        /// </param>
        public IssueMessage(object sender, object content)
            : base(sender, content)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="IssueMessage"/> class.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="target">
        /// The target.
        /// </param>
        /// <param name="content">
        /// The content.
        /// </param>
        public IssueMessage(object sender, object target, object content)
            : base(sender, target, content)
        {
        }

        /// <summary>
        /// Gets or sets the selected issue.
        /// </summary>
        public object SelectedIssue { get; set; }
    }
}
