using System;
using System.Collections.Generic;
using System.Text;
using BusinessManager.Interfaces;
using CommonLayer;
using CommonLayer.Models;
using RepositoryLayer.Interfaces;


namespace BusinessManager.Services
{
    public class UserBL : IUserBL
    {
        IUserRL userRL;
        public UserBL(IUserRL userRL)
        {
            this.userRL = userRL;
        }

        public bool UserRegister(UserModel user)
        {
            return this.userRL.UserRegister(user);
        }

        public IEnumerable<UserModel> GetAll()
        {
            return this.userRL.GetAll();
        }

        public UserModel Get(long id)
        {
            return this.userRL.Get(id);
        }

        public bool Update(UserModel user, UserModel entity)
        {
            return this.userRL.Update(user, entity);
        }

        public void Delete(UserModel user)
        {
            this.userRL.Delete(user);        
        }

        public UserModel Authenticate(string email, string password)
        {
            return userRL.Authenticate(email,password);
        }
    }
}
