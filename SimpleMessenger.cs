using System;
using System.Collections.Generic;
using System.Linq;

namespace SimpleMessenger
{
    
    /// <summary>
    /// A simple Messenger class to allow objects to send messeges to each other without 
    /// static references
    /// </summary>
    public class Messenger
    {
        /// <summary>
        /// A list of Weak References (to avoid memory leaks) of message receivers
        /// </summary>
        private readonly List<WeakReference> _receivers = new List<WeakReference>();
        
        /// <summary>
        /// A global static version of the SimpleMessenger
        /// </summary>
        private static Messenger _globalMessenger = null;

        /// <summary>
        /// A helper function to get a global static version of the SimpleMessenger
        /// </summary>        
        /// <returns>Returns a global static version of the SimpleMessenger</returns>
        public static Messenger GetMessenger()
        {
            if (_globalMessenger == null)
                _globalMessenger = new Messenger();

            return _globalMessenger;
        }

        
        /// <summary>
        /// Register a IMessageReceiver to recieve messages
        /// </summary>
        /// <param name="receiver">A reference to the IMessageReceiver to add</param>
        /// <returns>Returns false if the reciever has allready been added</returns>
        public bool Register(IMessageReceiver receiver)
        {
            //make sure we do not add a double
            foreach (var reference in _receivers.ToList())
            {
                if (((IMessageReceiver)reference.Target) == receiver)
                    return false;
            }
            //add the receiver
            _receivers.Add(new WeakReference(receiver));
            return true;
        }

        /// <summary>
        /// Remove a IMessageReceiver to so that is no longer receives messages
        /// </summary>
        /// <param name="receiver">A reference to the IMessageReceiver to remove</param>
        /// <returns>Returns true if the receiver was found and removed</returns>
        public bool UnRegister(IMessageReceiver receiver)
        {
            foreach (var reference in _receivers.ToList())
            {
                if (((IMessageReceiver)reference.Target) == receiver)
                {
                    //remove the found receiver
                    _receivers.Remove(reference);
                    return true;
                }
            }
            //did not find one to remove
            return false;
        }

        /// <summary>
        /// Send a message to all registered receivers
        /// </summary>
        /// <param name="msg">A Message object to send</param>        
        public void Send(Message msg)
        {
            // Locking the receivers to avoid multithreaded issues.
            lock (_receivers)
            {
                var toRemove = new List<WeakReference>();
                foreach (var reference in _receivers.ToList())
                {
                    //check that the object still exists
                    if (reference.IsAlive)
                        ((IMessageReceiver)reference.Target).ReceiveMessage(msg);
                    //if not add it to a list to be removed
                    else
                        toRemove.Add(reference);
                }
                //Remove Recievers that no longer exist
                foreach (var dead in toRemove)
                    _receivers.Remove(dead);
            }
        }
    }
}
