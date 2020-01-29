namespace SimpleMessenger
{
    /// <summary>
    /// A simple Messenge reciever interface in order to recieve Message objects
    /// You must call SimpleMessenger.Register in order to register the object
    /// before it will start to receive messages
    /// </summary>
    public interface IMessageReceiver
    {
        /// <summary>
        /// Called when the Simple Messenger recieves a new Message
        /// </summary>
        /// <param name="msg">The Message object sent</param>   
        void ReceiveMessage(Message msg);
    }
}