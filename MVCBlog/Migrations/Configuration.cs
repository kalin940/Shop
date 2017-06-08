//MVCBlog.Models.ApplicationDbContext  MVCBlog.Migrations

namespace MVCBlog.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using MVCBlog.Models;
    using System;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using MVCBlog.Data;
    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            if (!context.Users.Any())
            {
                // If the database is empty, populate sample data in it

                CreateUser(context, "admin@gmail.com", "123456", "Administrator");
                CreateUser(context, "pesho@gmail.com", "123456", "Peter Ivanov");
                CreateUser(context, "merry@gmail.com", "123456", "Maria Petrova");
                CreateUser(context, "geshu@gmail.com", "123456", "George Petrov");

                CreateRole(context, "Administrators");
                AddUserToRole(context, "admin@gmail.com", "Administrators");

                CreatePost(context,
                title: "Продавам РАКИЯ колеги!",
                body: @"Малко Боденце на този пост ",
                date: new DateTime(2016, 02, 18, 22, 14, 38),
                authorUsername: "pesho@gmail.com"
                );
                CreatePost(context,
                 title: "Нов пост",
                 body: @"Голямото Боденце на този пост ",
                 date: new DateTime(2016, 03, 18, 22, 14, 38),
                 authorUsername: "pesho@gmail.com"
                );
                context.SaveChanges();
            }
        }

        private void CreateUser(ApplicationDbContext context,
            string email, string password, string fullName)
        {
            var userManager = new UserManager<AspNetUsers>(
                new UserStore<AspNetUsers>(context));
            userManager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 5,
                RequireNonLetterOrDigit = false,
                RequireDigit = false,
                RequireLowercase = false,
                RequireUppercase = false,
            };

            var user = new AspNetUsers
            {
                UserName = email,
                Email = email,
                FullName = fullName
            };
            var userCreateResult = userManager.Create(user, password);
            if (!userCreateResult.Succeeded)
            {
                throw new Exception(string.Join("; ", userCreateResult.Errors));
            }
        }
        private void CreateRole(ApplicationDbContext context, string roleName)
        {
            var roleManager = new RoleManager<IdentityRole>(
                new RoleStore<IdentityRole>(context));
            var roleCreateResult = roleManager.Create(new IdentityRole(roleName));
            if (!roleCreateResult.Succeeded)
            {
                throw new Exception(string.Join("; ", roleCreateResult.Errors));
            }
        }

        private void AddUserToRole(ApplicationDbContext context, string userName, string roleName)
        {
            var user = context.Users.First(u => u.UserName == userName);
            var userManager = new UserManager<AspNetUsers>(
                new UserStore<AspNetUsers>(context));
            var addAdminRoleResult = userManager.AddToRole(user.Id, roleName);
            if (!addAdminRoleResult.Succeeded)
            {
                throw new Exception(string.Join("; ", addAdminRoleResult.Errors));
            }
        }

        private void CreatePost(ApplicationDbContext context,
            string title, string body, DateTime date, string authorUsername)
        {
            var post = new Post();
            post.Title = title;
            post.Body = body;
            post.Date = date;
            post.Author = context.Users.Where(u => u.UserName == authorUsername).FirstOrDefault();
            context.Posts.Add(post);
        }
    }
}
