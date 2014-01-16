namespace RedmineClient.Messanger.Messages.Main
{
    using System;

    using RedmineClient.Messanger.Messages.Base;

    /// <summary>
    /// The main message.
    /// </summary>
    public class MainMessage : BaseMessage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MainMessage"/> class.
        /// </summary>
        /// <param name="content">
        /// The content.
        /// </param>
        /// <param name="uri">
        /// The uri.
        /// </param>
        public MainMessage(object content, Uri uri)
            : base(content, uri)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MainMessage"/> class.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="content">
        /// The content.
        /// </param>
        public MainMessage(object sender, object content)
            : base(sender, content)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MainMessage"/> class.
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
        public MainMessage(object sender, object target, object content)
            : base(sender, target, content)
        {
        }
    }
}
