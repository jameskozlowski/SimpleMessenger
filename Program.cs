using System;
using SimpleMessenger;

namespace msgtest
{
    class Program
    {
        static void Main(string[] args)
        {
            //message reciver
            DummyObject1 o1 = new DummyObject1();

            //send a message
            DummyObject2 o2 = new DummyObject2();
            o2.SomeFunction();
        }


    }

    class DummyObject1: IMessageReceiver
    {
        public const int MSG_PRINTSTRING = 1;

        public DummyObject1()
        {
            Messenger.GetMessenger().Register(this);
        }
        public void ReceiveMessage(Message msg)
        {
            if (msg.MessageID == MSG_PRINTSTRING)
                Console.WriteLine((string)msg.Data);
        }
    }
    
    class DummyObject2
    {
        public void SomeFunction()
        {
            Message msg = new Message(DummyObject1.MSG_PRINTSTRING, this, "Hello World");
            Messenger.GetMessenger().Send(msg);
        }
    }

    
}
