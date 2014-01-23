namespace RedmineClient.Messanger.Messages.Project
{
    using System;

    using RedmineClient.Messanger.Messages.Base;

    /// <summary>
    /// The project message.
    /// </summary>
    public class ProjectMessage : BaseMessage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectMessage"/> class.
        /// </summary>
        /// <param name="content">
        /// The content.
        /// </param>
        /// <param name="uri">
        /// The uri.
        /// </param>
        public ProjectMessage(object content, Uri uri)
            : base(content, uri)
        {
            this.SelectedProject = content;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectMessage"/> class.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="content">
        /// The content.
        /// </param>
        public ProjectMessage(object sender, object content)
            : base(sender, content)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectMessage"/> class.
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
        public ProjectMessage(object sender, object target, object content)
            : base(sender, target, content)
        {
        }

        /// <summary>
        /// Gets or sets the selected project.
        /// </summary>
        public object SelectedProject { get; set; }
    }
}
