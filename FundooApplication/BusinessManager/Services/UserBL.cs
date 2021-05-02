using System;
using System.Collections.Generic;
using System.Text;
using BusinessManager.Interfaces;
using CommonLayer;
using RepositoryLayer.Interfaces;
using RepositoryLayer.Models;

namespace BusinessManager.Services
{
    public class UserBL : IUserBL
    {
        IUserRL userRL;
        public UserBL(IUserRL userRL)
        {
            this.userRL = userRL;
        }

        public bool UserRegister(User user)
        {
            return this.userRL.UserRegister(user);
        }

        public IEnumerable<User> GetAll()
        {
            return this.userRL.GetAll();
        }

        public User Get(long id)
        {
            return this.userRL.Get(id);
        }

        public bool Update(User user, User entity)
        {
            return this.userRL.Update(user, entity);
        }

        public void Delete(User user)
        {
            this.userRL.Delete(user);        
        }
    }
}
