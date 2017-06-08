using MVCBlog.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using System.Web;

namespace MVCBlog.Services
{

    public class MailServices
    {
        static string domainMail = ConfigurationManager.AppSettings["DomainMail"];
        static string mailPassword = ConfigurationManager.AppSettings["DomainMailPassword"];
        /// <summary>
        /// Sending a Welcoming Mail to every new subscriber
        /// </summary>
        /// <param name="reciever"></param>
        public static void sendWelcomeMail(string reciever)
        {
            #region formatter
            //string text = string.Format("Welcome to our shop!");
            string html = "Welcome to our shop!<br/>You are going to recieve mails about our new products.<br/>";

           // html += HttpUtility.HtmlEncode(@"Or click on the copy the following link on the browser:" + "test");
            #endregion

            MailMessage msg = new MailMessage();
            msg.From = new MailAddress(domainMail);
            msg.To.Add(new MailAddress(reciever));
            msg.Subject = "Welcome letter";
            //msg.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(text, null, MediaTypeNames.Text.Plain));
            msg.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(html, null, MediaTypeNames.Text.Html));

            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", Convert.ToInt32(587));
            System.Net.NetworkCredential credentials = new System.Net.NetworkCredential(domainMail, mailPassword);
            smtpClient.Credentials = credentials;
            smtpClient.EnableSsl = true;
            smtpClient.Send(msg);
        }
        /// <summary>
        /// Sending Mail to a List of Subscribers
        /// </summary>
        /// <param name="reciever"></param>
        /// <param name="subject"></param>
        /// <param name="content"></param>
        public static void sendMailToSubscribers(List<string> reciever,string subject,string content)
        {
            
            #region formatter
            //string text = string.Format("Welcome to our shop!");
            string html = content;

            // html += HttpUtility.HtmlEncode(@"Or click on the copy the following link on the browser:" + "test");
            #endregion
            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", Convert.ToInt32(587));
            System.Net.NetworkCredential credentials = new System.Net.NetworkCredential(domainMail, mailPassword);
            smtpClient.Credentials = credentials;
            smtpClient.EnableSsl = true;
            for (var i=0;i<reciever.Count;i++)
            {
                MailMessage msg = new MailMessage();
                msg.From = new MailAddress(domainMail);
                msg.To.Add(new MailAddress(reciever[i]));
                msg.Subject = subject;
                //msg.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(text, null, MediaTypeNames.Text.Plain));
                msg.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(html, null, MediaTypeNames.Text.Html));
                smtpClient.Send(msg);
            }           
        }
        /// <summary>
        /// Emails from customers from contact form
        /// </summary>
        /// <param name="contacts"></param>
        public static void ContactUsMail(ContactUsViewModel contacts)
        {
          
            #region formatter
            //string text = string.Format("Welcome to our shop!");
            string html = $"{contacts.FirstName} {contacts.LastName}:<br/>{contacts.Message}";

            // html += HttpUtility.HtmlEncode(@"Or click on the copy the following link on the browser:" + "test");
            #endregion

            MailMessage msg = new MailMessage();
            msg.From = new MailAddress(contacts.EmailAddress);
            msg.To.Add(new MailAddress(domainMail));
            msg.Subject = contacts.Subject;
            //msg.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(text, null, MediaTypeNames.Text.Plain));
            msg.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(html, null, MediaTypeNames.Text.Html));

            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", Convert.ToInt32(587));
            System.Net.NetworkCredential credentials = new System.Net.NetworkCredential(domainMail, mailPassword);
            smtpClient.Credentials = credentials;
            smtpClient.EnableSsl = true;
            smtpClient.Send(msg);
        }
        /// <summary>
        /// Reseting Password sends Mail
        /// </summary>
        /// <param name="url"></param>
        /// <param name="mail"></param>
        public static void ForgotPassword(string url,string mail)
        {

            #region formatter
            //string text = string.Format("Welcome to our shop!");
            string html = "Please reset your password by clicking <a href=\"" + url + "\">here</a>";

            // html += HttpUtility.HtmlEncode(@"Or click on the copy the following link on the browser:" + "test");
            #endregion

            MailMessage msg = new MailMessage();
            msg.From = new MailAddress(domainMail);
            msg.To.Add(new MailAddress(mail));
            msg.Subject = "Forgotten Password";
            //msg.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(text, null, MediaTypeNames.Text.Plain));
            msg.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(html, null, MediaTypeNames.Text.Html));

            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", Convert.ToInt32(587));
            System.Net.NetworkCredential credentials = new System.Net.NetworkCredential(domainMail, mailPassword);
            smtpClient.Credentials = credentials;
            smtpClient.EnableSsl = true;
            smtpClient.Send(msg);
        }
        /// <summary>
        /// Conformation email
        /// </summary>
        /// <param name="url"></param>
        /// <param name="mail"></param>
        public static void ConfirmEmail(string url, string mail)
        {

            #region formatter
            //string text = string.Format("Welcome to our shop!");
            string html = "Please confirm your account by clicking <a href=\"" + url + "\">here</a>";

            // html += HttpUtility.HtmlEncode(@"Or click on the copy the following link on the browser:" + "test");
            #endregion

            MailMessage msg = new MailMessage();
            msg.From = new MailAddress(domainMail);
            msg.To.Add(new MailAddress(mail));
            msg.Subject = "Confirm Email";
            //msg.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(text, null, MediaTypeNames.Text.Plain));
            msg.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(html, null, MediaTypeNames.Text.Html));

            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", Convert.ToInt32(587));
            System.Net.NetworkCredential credentials = new System.Net.NetworkCredential(domainMail, mailPassword);
            smtpClient.Credentials = credentials;
            smtpClient.EnableSsl = true;
            smtpClient.Send(msg);
        }
    }
}