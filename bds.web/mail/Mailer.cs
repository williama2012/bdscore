using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.IO;
using System.Text;
using System.Xml;
using System.Net;
using System.Net.Mail;
using System.Security;

namespace DatasetEmailer {
    public class Mailer {

        private string addressFrom = "test@test.com";
        private string addressTo = "test@test.com";
        private string emailSubject = "Test";

        public void Email(string templateFilename, DataTable data) {
            var template = LoadTemplate(templateFilename);
            var messages = BuildXml(data, template);

            foreach (var msg in messages) {
                MailMessage m = BuildEmailMessage(msg);
                SendMessage(m);
            }
        }

        public static XmlDocument LoadTemplate(string fileName) {
            XmlDocument doc = new XmlDocument();

            try {
                doc.LoadXml(File.ReadAllText(fileName));
            }
            catch (Exception e) { 
            
            }
            
            return doc;
        }

        public XmlDocument[] BuildXml(DataTable datatable, XmlDocument emailTemplate) {
            XmlDocument[] emails = new XmlDocument[datatable.Rows.Count];
            XmlDocument emailCopy = (XmlDocument)emailTemplate.Clone();
            int count = 0;
            foreach (DataRow row in datatable.Rows) {
                emails[count] = BuildXml(row, emailTemplate);
                count++;
            }
            return emails;
        }

        public XmlDocument BuildXml(DataRow row, XmlDocument emailTemplate) {
            foreach (DataColumn col in row.Table.Columns) {
                XmlNodeList list = emailTemplate.SelectNodes(string.Format("//{0}", col.ColumnName));
                foreach (XmlElement elem in list) {
                    XmlElement parent = (XmlElement)elem.ParentNode;
                    parent.InnerText = row[col].ToString();
                }
            }
            return emailTemplate;
        }

        private MailMessage BuildEmailMessage(XmlDocument emailDocument) {
            MailMessage message = new MailMessage();
            message.IsBodyHtml = true;
            message.Subject = emailSubject;
            message.Body = emailDocument.InnerXml;

            message.From = new MailAddress(addressFrom);
            MailAddressCollection addressToCollection = message.To;

            addressTo = addressTo.Replace(";", ",");
            addressToCollection.Add(addressTo);

            return message;
        }

        private void SendMessage(MailMessage message) {
            SmtpClient client = new SmtpClient();

            try {
                SetupSmtpClient(client);
                client.Send(message);

            }
            catch (ArgumentNullException e) {

            }
            catch (InvalidOperationException e) {

            }
            catch (SmtpFailedRecipientsException e) { 
            
            }
            catch (SmtpException e) {

            }

        }


        #region Configuration


        public static void SetupSmtpClient(SmtpClient client) {
            client.Host = Host;
            client.Port = Port;

            client.DeliveryMethod = DeliveryMethod;
            client.PickupDirectoryLocation = PickupDirectoryLocation;

            client.EnableSsl = EnableSsl;
            client.UseDefaultCredentials = false;
            client.Timeout = Timeout;
            client.TargetName = TargetName;

        }

        public static string Host {
            get {
                string host = "";
                if (host == string.Empty)
                    return "localhost";
                return "losthost";
            }
        }

        public static int Port {
            get {
                int port;
                int.TryParse("", out port);
                if (port == 0)
                    return 25;
                return port;
            }
        }

        public static SmtpDeliveryMethod DeliveryMethod {
            get {
                string method = "Network";

                switch (method) { 
                    case "PickupDirectoryFromIis":
                        return SmtpDeliveryMethod.PickupDirectoryFromIis;
                    case "SpecifiedPickupDirectory":
                        return SmtpDeliveryMethod.SpecifiedPickupDirectory;
                    default:
                        return SmtpDeliveryMethod.Network;
                }
            }
        }

        public static string PickupDirectoryLocation {
            get {
                string directory = string.Empty;
                directory = "";
                return directory;
            }
        }

        public static bool EnableSsl {
            get {
                bool enable = false;
                bool.TryParse("", out enable);
                return enable;
            }
        }

        public static int Timeout {
            get {
                int timeout;
                int.TryParse("", out timeout);
                if (timeout == 0)
                    return 120;
                return timeout;
            }
        }

        public static string TargetName {
            get {
                string targetname = string.Empty;
                targetname = "";
                return targetname;
            }
        }

        #endregion
    

    }
}
