namespace RedmineClient.Messanger.Messages.LogOn
{
    using System;

    using RedmineClient.Messanger.Messages.Base;

    /// <summary>
    /// The log on.
    /// </summary>
    public class LogOnMessage : BaseMessage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LogOnMessage"/> class.
        /// </summary>
        /// <param name="content">
        /// The content.
        /// </param>
        /// <param name="uri">
        /// The uri.
        /// </param>
        public LogOnMessage(object content, Uri uri)
            : base(content, uri)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LogOnMessage"/> class.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="content">
        /// The content.
        /// </param>
        public LogOnMessage(object sender, object content)
            : base(sender, content)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LogOnMessage"/> class.
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
        public LogOnMessage(object sender, object target, object content)
            : base(sender, target, content)
        {
        }
    }
}
