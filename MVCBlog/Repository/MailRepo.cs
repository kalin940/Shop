using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MVCBlog.Data;
using MVCBlog.Extensions;
using MVCBlog.Models;
namespace MVCBlog.Repository
{
    public class MailRepo
    {
        /// <summary>
        /// Saving new subscribers in DB, checking for existing ones
        /// </summary>
        /// <param name="email"></param>
        public static bool SaveNewSubscriber(string email)
        {
            try
            {
                using (var db = new ApplicationDbContext())
                {
                    var check = db.Subscribers.FirstOrDefault(p => p.EmailAddress.Equals(email));
                    if (check != null)
                    {
                        return false;
                    }
                    var subscriber = new Subscribers();
                    subscriber.EmailAddress = email;
                    db.Subscribers.Add(subscriber);
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public static List<string> GetAllSubscribersEmails()
        {
            try
            {
                using (var db = new ApplicationDbContext())
                {
                    var subsList = db.Subscribers.Select(p=>p.EmailAddress).ToList();
                   
                    return subsList;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}