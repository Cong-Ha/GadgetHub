using System.Text;
using System.Net;
using System.Net.Mail;
using GadgetHub.Domain.Abstract;
using GadgetHub.Domain.Entities;
using System.IO;
using System.Configuration;
using System;

namespace GadgetHub.Domain.Email
{
    public class EmailOrderProcessor
    {
        public class EmailSettings
        {
            public string MailToAddress = "orders_from_GadgetHub@ghub.com";

            public string MailFromAddress = "GadgetHub@GadgetHub.com";

            public bool UseSSL = true;

            public string Username = "MySmtpUserName";

            public string Password = "MySmtpPassword";

            public string ServerName = "smtp.gadgethub.com";

            public int ServerPort = 587;

            public bool WriteAsFile = true;

            public string FileLocation = @"c:\gadgetHub_emails";
        }

        public class EmailOrderProcess : IOrderProcessor
        {
            private readonly EmailSettings _emailSettings;

            public EmailOrderProcess(EmailSettings settings)
            {
                _emailSettings = settings;
            }

            public void ProcessOrder(Cart cart, ShippingDetails shippingDetails)
            {
                using (var smtpClient = new SmtpClient(_emailSettings.MailToAddress))
                {
                    smtpClient.EnableSsl = _emailSettings.UseSSL;
                    smtpClient.Host = _emailSettings.ServerName;
                    smtpClient.Port = _emailSettings.ServerPort;
                    smtpClient.UseDefaultCredentials = false;
                    smtpClient.Credentials = new NetworkCredential(_emailSettings.Username, _emailSettings.Password);
                    var emailDirectory = _emailSettings.FileLocation;

                    if (_emailSettings.WriteAsFile)
                    {
                        if(!Directory.Exists(emailDirectory))
                        {
                            Directory.CreateDirectory(_emailSettings.FileLocation);
                        }
                        if (bool.Parse(ConfigurationManager.AppSettings["DevelopmentMode"]))
                        {
                            smtpClient.PickupDirectoryLocation = emailDirectory;
                        }
                        else
                        {
                            //TODO: Implement a different file location for production
                            throw new DirectoryNotFoundException($"Directory for emails does not exist for Prod.");
                        }
                        smtpClient.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
                        smtpClient.EnableSsl = false;
                    }
                    StringBuilder body = new StringBuilder()
                        .AppendLine("New order has been received")
                        .AppendLine("---")
                        .AppendLine("Items:");

                    foreach (var line in cart.Lines)
                    {
                        var subtotal = line.Gadget.Price * line.Quantity;
                        body.AppendFormat("{0} x {1} (subtotal: {2:c}){3}", line.Quantity, line.Gadget.Name, subtotal, Environment.NewLine);
                    }

                    body.AppendFormat("Total order value: {0:c}", cart.ComputeTotalValue())
                        .AppendLine("---")
                        .AppendLine("Ship to:")
                        .AppendLine(shippingDetails.Name)
                        .AppendLine(shippingDetails.Line1)
                        .AppendLine(shippingDetails.Line2 ?? "")
                        .AppendLine(shippingDetails.Line3 ?? "")
                        .AppendLine(shippingDetails.City)
                        .AppendLine(shippingDetails.State)
                        .AppendLine(shippingDetails.Zip)
                        .AppendLine(shippingDetails.Country)
                        .AppendLine("---")
                        .AppendFormat("Gift wrap: {0}", shippingDetails.GiftWrap ? "Yes" : "No");
                    MailMessage mailMessage = new MailMessage(
                        _emailSettings.MailFromAddress, 
                        _emailSettings.MailToAddress, 
                        "New order submitted!", 
                        body.ToString());

                    if (_emailSettings.WriteAsFile)
                    {
                        mailMessage.BodyEncoding = Encoding.ASCII;
                        mailMessage.SubjectEncoding = Encoding.ASCII;
                    }
                    smtpClient.Send(mailMessage);
                }
            }
        }
    }
}
