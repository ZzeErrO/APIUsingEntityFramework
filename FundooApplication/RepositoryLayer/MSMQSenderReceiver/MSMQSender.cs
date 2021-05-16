using System;
using System.Collections.Generic;
using System.Text;
using Experimental.System.Messaging;

namespace RepositoryLayer.MSMQSenderReceiver
{
    public class MSMQSender
    {
        public static void SendMessage()
        {
            try
            {
                var body = "Click on following link to reset your credentials for Fundoo Notes App: ";
                MessageQueue msmqQueue = new MessageQueue();
                if (MessageQueue.Exists(@".\Private$\MyQueue"))
                {
                    msmqQueue = new MessageQueue(@".\Private$\MyQueue");
                }
                else
                {
                    msmqQueue = MessageQueue.Create(@".\Private$\MyQueue");
                }

                Message message = new Message();
                message.Formatter = new BinaryMessageFormatter();
                message.Body = body;
                msmqQueue.Label = "url link";
                msmqQueue.Send(message);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
