
namespace SimpleMessenger
{
    /// <summary>
    /// A simple Messenge class that can be sent using the SimpleMessenger    
    /// </summary>
    public class Message
    {
        /// <summary>
        /// The Message ID, used by the other side to ID the Message meaning
        /// </summary>
        public int MessageID { get; set; }

        /// <summary>
        /// The sender of the message
        /// </summary>
        public object Sender { get; set; }

        /// <summary>
        /// A data object being sent with the Message 
        /// </summary>
        public object Data { get; set; }

        public Message()
        {

        }

        /// <summary>
        /// A simple Messenge class that can be sent using the SimpleMessenger
        /// </summary>
        /// <param name="messageID">The Message ID, used by the other side to ID the Message meaning</param>
        /// <param name="sender">The sender of the message (ex. this)</param>
        /// <param name="data">A data object being sent with the Message (use null if not needed)</param>
        public Message(int messageID, object sender, object data)
        {
            MessageID = messageID;
            Sender = sender;
            Data = data;
        }
    }


}
