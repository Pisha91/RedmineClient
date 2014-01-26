namespace RedmineClient.Messanger.Messages.Back
{
    using GalaSoft.MvvmLight.Messaging;

    public class BackMessage : GenericMessage<object>
    {
        public BackMessage(object content)
            : base(content)
        {
        }

        public BackMessage(object sender, object content)
            : base(sender, content)
        {
        }

        public BackMessage(object sender, object target, object content)
            : base(sender, target, content)
        {
        }
    }
}
