namespace RedmineClient.Messanger.Messages.Base
{
    using System;

    using GalaSoft.MvvmLight.Messaging;

    /// <summary>
    /// The base message.
    /// </summary>
    public class BaseMessage : GenericMessage<object>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseMessage"/> class.
        /// </summary>
        /// <param name="content">
        /// The content.
        /// </param>
        /// <param name="uri">
        /// The uri.
        /// </param>
        public BaseMessage(object content, Uri uri)
            : base(content)
        {
            this.Uri = uri;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseMessage"/> class.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="content">
        /// The content.
        /// </param>
        public BaseMessage(object sender, object content)
            : base(sender, content)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseMessage"/> class.
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
        public BaseMessage(object sender, object target, object content)
            : base(sender, target, content)
        {
        }

        /// <summary>
        /// Gets or sets the uri.
        /// </summary>
        public Uri Uri { get; set; }
    }
}
