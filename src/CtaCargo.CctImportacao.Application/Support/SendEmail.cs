using CtaCargo.CctImportacao.Application.Support.Contracts;
using Microsoft.Extensions.Configuration;
using System;
using System.Net;
using System.Net.Mail;

namespace CtaCargo.CctImportacao.Application.Support
{
    public class SendEmail : ISendEmail
    {
        public readonly IConfiguration _configuration;
        private readonly string _smtpHost;
        private readonly int _smtpHostPort;
        private readonly string _smtpHostUSer;
        private readonly string _smtpHostPwd;
        private readonly bool _smtpUseSSL;
        private readonly string _smtpEmailFrom;

        public SendEmail(IConfiguration configuration)
        {
            _configuration = configuration;
            _smtpHost = _configuration.GetSection("EmailConfiguration").GetSection("SMTPHost").Value;
            _smtpHostPort = Convert.ToInt32(_configuration.GetSection("EmailConfiguration").GetSection("SMTPHostPort").Value);
            _smtpHostUSer = _configuration.GetSection("EmailConfiguration").GetSection("SMTPUSer").Value;
            _smtpHostPwd = _configuration.GetSection("EmailConfiguration").GetSection("SMPTPassword").Value;
            _smtpUseSSL = Convert.ToBoolean(_configuration.GetSection("EmailConfiguration").GetSection("SMTPUseSSL").Value);
            _smtpEmailFrom = _configuration.GetSection("EmailConfiguration").GetSection("EmailFrom").Value;
        }

        public void Email(string emailTo, string subject, string htmlString)
        {
            MailMessage message = new MailMessage();
            SmtpClient smtp = new SmtpClient(_smtpHost, _smtpHostPort);
            message.From = new MailAddress(_smtpEmailFrom);
            message.To.Add(new MailAddress(emailTo));
            message.Subject = subject;
            message.IsBodyHtml = true;
            message.Body = htmlString;
            smtp.EnableSsl = _smtpUseSSL;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential(_smtpHostUSer, _smtpHostPwd);
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.Send(message);
        }
    }
}
