using System;
using System.Collections.Generic;
using System.Text;
using CommonLayer;
using RepositoryLayer.Models;

namespace BusinessManager.Interfaces
{
    public interface IUserBL
    {
        bool UserRegister(User user);
        public IEnumerable<User> GetAll();
        public User Get(long id);
        public bool Update(User user, User entity);
        public void Delete(User user);
    }
}
