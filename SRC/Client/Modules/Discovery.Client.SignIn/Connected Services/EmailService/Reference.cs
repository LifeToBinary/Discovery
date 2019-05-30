﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Discovery.Client.SignIn.EmailService {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="EmailService.IEmailService")]
    public interface IEmailService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IEmailService/SendEmail", ReplyAction="http://tempuri.org/IEmailService/SendEmailResponse")]
        void SendEmail(string recipientAddress, string subject, string content);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IEmailService/SendEmail", ReplyAction="http://tempuri.org/IEmailService/SendEmailResponse")]
        System.Threading.Tasks.Task SendEmailAsync(string recipientAddress, string subject, string content);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IEmailServiceChannel : Discovery.Client.SignIn.EmailService.IEmailService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class EmailServiceClient : System.ServiceModel.ClientBase<Discovery.Client.SignIn.EmailService.IEmailService>, Discovery.Client.SignIn.EmailService.IEmailService {
        
        public EmailServiceClient() {
        }
        
        public EmailServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public EmailServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public EmailServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public EmailServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public void SendEmail(string recipientAddress, string subject, string content) {
            base.Channel.SendEmail(recipientAddress, subject, content);
        }
        
        public System.Threading.Tasks.Task SendEmailAsync(string recipientAddress, string subject, string content) {
            return base.Channel.SendEmailAsync(recipientAddress, subject, content);
        }
    }
}