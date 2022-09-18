using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;

namespace AppDevDotNetTask1
{
    public enum MailType
    {
        Registration,
        Statement
    }

    static class Mailer
    {
        private static SmtpClient _mailClient;

        static Mailer()
        {
            // Try & connect to the smtp server in order to allow sending emails in the future.
            try
            {
                _mailClient = new SmtpClient("email-smtp.us-east-2.amazonaws.com", 587);
                _mailClient.Credentials = new NetworkCredential("AKIAU67BVJMN64A77OVV", "BL5y6lG9YQgfEo4QzXNNTSGFzaKuavjYMaGqA5nSyAHk");
                _mailClient.EnableSsl = true;
            }
            catch
            {
                Console.WriteLine("Failed to connect to mail server");
                Console.ReadKey();
            }
        }

        /// <summary>
        /// Sends an email to the specified address using the specified template.
        /// </summary>
        /// <param name="type">The type of mail to use, can be either Registration or Statement</param>
        /// <param name="parameters">A Dictionary of parameters that are used to make the email content dynamic</param>
        /// <returns>Whether the email was sent successfully</returns>
        public static bool SendEmail(MailType type, Dictionary<string, string> parameters)
        {
            try
            {
                MailMessage mail = new MailMessage(
                    new MailAddress("dotnet@jackdonaldson.net", "Banking System"),
                    new MailAddress(parameters["Email"]));

                mail.IsBodyHtml = true;

                // Load the respective plaintext & html email content into the MailMessage object
                // Plaintext is used by email clients that don't support HTML
                ContentType textVersion = new ContentType("text/html");
                string htmlBody;
                if (type == MailType.Registration)
                {
                    mail.Subject = "Account Registration";
                    htmlBody = LoadTemplate("templates/html/registration.html", parameters);
                    mail.Body = LoadTemplate("templates/plaintext/registration.txt", parameters);
                }
                else
                {
                    mail.Subject = "Account Statement";
                    htmlBody = LoadTemplate("templates/html/statement.html", parameters);
                    mail.Body = LoadTemplate("templates/plaintext/statement.txt", parameters);
                }

                // Add the plaintext email alternative to the MailMessage object
                AlternateView htmlAlternative = AlternateView.CreateAlternateViewFromString(htmlBody, textVersion);
                mail.AlternateViews.Add(htmlAlternative);

                // Send the email
                _mailClient.Send(mail);

                return true;
            }
            catch
            {
                Console.WriteLine($"Failed to send email to {parameters["Email"]}");
                Console.ReadKey();

                return false;
            }
        }

        /// <summary>
        /// Read a given file from the disk & replace it's parameter with their respective values.
        /// </summary>
        /// <param name="templateLocation">The file location to read from</param>
        /// <param name="parameters">A dictionary of parameters with their values</param>
        /// <returns></returns>
        private static string LoadTemplate(string templateLocation, Dictionary<string, string> parameters)
        {
            try
            {
                string template = File.ReadAllText(templateLocation);

                // Replace all parameters in a file with their values based on their key
                // e.g. [FirstName] gets replaced with 'Jack'
                foreach (KeyValuePair<string, string> parameter in parameters)
                {
                    template = template.Replace($"[{parameter.Key}]", parameter.Value);
                }

                return template;
            }
            catch
            {
                return "";
            }
        }
    }
}