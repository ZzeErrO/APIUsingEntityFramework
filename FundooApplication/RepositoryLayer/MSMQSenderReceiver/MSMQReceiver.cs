using System;
using System.Collections.Generic;
using System.Text;
using Experimental.System.Messaging;

namespace RepositoryLayer.MSMQSenderReceiver
{
    public delegate void MessageReceivedEventhandler(object sender, MessageEventArgs args);
    public class MSMQReceiver
    {
        public static string receiverMessage()
        {
            try
            {
                MessageQueue reciever = new MessageQueue(@".\Private$\MyQueue");
                var recieving = reciever.Receive();
                recieving.Formatter = new BinaryMessageFormatter();

               // reciever.PeekCompleted += new PeekCompletedEventHandler(OnPeekCompleted);
               // reciever.ReceiveCompleted += new PeekCompletedEventHandler(OnReceiveCompleted);

                string body = recieving.Body.ToString();
                return body;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }

    public class MessageEventArgs : EventArgs
    {

        private object _messageBody;

        public object MessageBody
        {
            get { return _messageBody; }
        }

        public MessageEventArgs(object body)
        {
            _messageBody = body;
        }

    }
}
