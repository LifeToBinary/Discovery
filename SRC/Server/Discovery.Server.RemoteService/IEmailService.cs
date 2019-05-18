using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Discovery.Server.RemoteService
{
    [ServiceContract]
    public interface IEmailService
    {
        [OperationContract]
        void SendEmail(string recipientAddress,
                       string subject,
                       string content);
    }
}
