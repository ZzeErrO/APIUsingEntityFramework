using System;
using System.Collections.Generic;
using System.Text;
using Experimental.System.Messaging;

namespace RepositoryLayer.MSMQSenderReceiver
{
    public class MSMQReceiver
    {
        public static string receiverMessage()
        {
            try
            {
                MessageQueue reciever = new MessageQueue(@".\Private$\MyQueue");
                var recieving = reciever.Receive();
                recieving.Formatter = new BinaryMessageFormatter();
                string body = recieving.Body.ToString();
                return body;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
