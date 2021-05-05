using System;
using System.Collections.Generic;
using System.Text;
using CommonLayer;
using RepositoryLayer.Models;

namespace RepositoryLayer.Interfaces
{
    public interface IUserRL
    {
        bool UserRegister(User user);
        public IEnumerable<User> GetAll();
        public User Get(long id);
        public bool Update(User user, User entity);
        public void Delete(User user);
        public User Authenticate(string email, string password);
    }
}
