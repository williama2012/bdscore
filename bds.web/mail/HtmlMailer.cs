using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.IO;
using System.Text;
using System.Net;
using System.Net.Mail;

namespace bds.web.mail {
    class HtmlMailer {

        private SmtpClient _client = new SmtpClient();

        private SmtpClient Client {
            get { return _client; }
        }

        /// <summary>
        /// Send cancel to Asynchronous Send.
        /// </summary>
        public void CancelSend() { Client.SendAsyncCancel(); }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="host"></param>
        /// <param name="port"></param>
        /// <param name="credential"></param>
        public HtmlMailer(string host, int port, NetworkCredential credential) {
            Client.Host = host;
            Client.Port = port;
            Client.DeliveryMethod = SmtpDeliveryMethod.Network;
            Client.Credentials = credential;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="body"></param>
        /// <param name="subject"></param>
        /// <param name="attachments"></param>
        /// <param name="toAddresses"></param>
        /// <param name="ccAddresses"></param>
        /// <param name="bccAddresses"></param>
        /// <returns></returns>
        public Guid SendMessage(string body, string subject, Dictionary<string, byte[]> attachments, IEnumerable<string> toAddresses, IEnumerable<string> ccAddresses, IEnumerable<string> bccAddresses) {
            MailMessage message = BuildMessage(body, subject, attachments, toAddresses, ccAddresses, bccAddresses);
            Guid id = Guid.NewGuid();
            Client.SendAsync(message, id);
            return id;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="body"></param>
        /// <param name="subject"></param>
        /// <param name="attachments"></param>
        /// <param name="toAddresses"></param>
        /// <param name="ccAddresses"></param>
        /// <param name="bccAddresses"></param>
        /// <returns></returns>
        private MailMessage BuildMessage(string body, string subject, Dictionary<string, byte[]> attachments, IEnumerable<string> toAddresses, IEnumerable<string> ccAddresses, IEnumerable<string> bccAddresses) {
            MailMessage message = new MailMessage();
            message.BodyEncoding = System.Text.Encoding.UTF8;

            message.Body = body;
            message.Subject = subject;
            
            foreach (var to in toAddresses)
                message.To.Add(new MailAddress(to));

            foreach (var cc in ccAddresses)
                message.CC.Add(new MailAddress(cc));

            foreach (var bcc in bccAddresses)
                message.Bcc.Add(new MailAddress(bcc));
            
            foreach (var attachment in attachments) {
                Stream stream = new MemoryStream(attachment.Value);
                message.Attachments.Add(new Attachment(stream, attachment.Key));
            }

            return message;
        }

        #region IDisposible Members

        private bool _disposed = false;

        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Dispose(bool disposing) {
            if (!_disposed) {
                if (disposing) {
                    ReleaseResources();
                }
            }
            _disposed = true;
        }

        public void ReleaseResources() {
            Client.Dispose();
        }

        #endregion

    }
}
