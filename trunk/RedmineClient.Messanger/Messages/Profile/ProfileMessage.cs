namespace RedmineClient.Messanger.Messages.Profile
{
    using System;

    using RedmineClient.Messanger.Messages.Base;

    /// <summary>
    /// The profile message.
    /// </summary>
    public class ProfileMessage : BaseMessage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProfileMessage"/> class.
        /// </summary>
        /// <param name="content">
        /// The content.
        /// </param>
        /// <param name="uri">
        /// The uri.
        /// </param>
        public ProfileMessage(object content, Uri uri)
            : base(content, uri)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProfileMessage"/> class.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="content">
        /// The content.
        /// </param>
        public ProfileMessage(object sender, object content)
            : base(sender, content)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProfileMessage"/> class.
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
        public ProfileMessage(object sender, object target, object content)
            : base(sender, target, content)
        {
        }
    }
}
