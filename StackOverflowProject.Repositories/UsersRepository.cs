using System;
using System.Collections.Generic;
using System.Linq;
using StackOverflowProject.DomainModels;

namespace StackOverflowProject.Repositories
{
    public interface IUsersRepository
    {
        void InsertUser(User u);
        void UpdateUserDetails(User u);
        void UpdateUserPassword(User u);
        void DeleteUser(int uid);
        List<User> GetUsers();
        List<User> GetUsersByEmailAndPassword(string Email,string PasswordHash);
        List<User> GetUsersByEmail(string Email);
        List<User> GetUsersByUSerID(int UserID);
        int getLatestUserID();
    }
    public class UsersRepository : IUsersRepository
    {
        StackOverflowDatabaseDbContext db;

        public UsersRepository()
        {
            db = new StackOverflowDatabaseDbContext(); 
        }

        public void InsertUser(User u)
        {
            db.Users.Add(u);
            db.SaveChanges();
        }

        public void UpdateUserDetails(User u)
        {
            User existingUser = db.Users.Where(temp => temp.UserID == u.UserID).FirstOrDefault();
            if (existingUser != null)
            {
                existingUser.Name = u.Name;
                existingUser.Mobile = u.Mobile;
                db.SaveChanges();
            }
        }

        public void UpdateUserPassword(User u)
        {
            User existingUser = db.Users.Where(temp => temp.UserID == u.UserID).FirstOrDefault();
            if (existingUser != null)
            {
                existingUser.PasswordHash = u.PasswordHash;
               
                db.SaveChanges();
            }
        }

        public void DeleteUser(int uid)
        {
            User existingUser = db.Users.Where(temp => temp.UserID == uid).FirstOrDefault();
            if (existingUser != null)
            {
                db.Users.Remove(existingUser);
                db.SaveChanges();
            }
        }

        public List<User> GetUsers()
        {
            List<User> users = db.Users.Where(temp => temp.IsAdmin == false).OrderBy(temp => temp.Name).ToList();
            return users;
        }


        public List<User> GetUsersByEmailAndPassword(string Email, string PasswordHash)
        {
            List<User> user = db.Users.Where(temp => temp.Email == Email && temp.PasswordHash == PasswordHash).ToList();
            return user;
        }

        public List<User> GetUsersByEmail(string Email)
        {
            List<User> user = db.Users.Where(temp => temp.Email == Email).ToList();
            return user;
        }

        public List<User> GetUsersByUSerID(int UserID)
        {
            List<User> user = db.Users.Where(temp => temp.UserID == UserID).ToList();
            return user;
        }

        public int getLatestUserID()
        {
            int id = db.Users.Select(s => s.UserID).Max();
            return id;
        }

    }
}
