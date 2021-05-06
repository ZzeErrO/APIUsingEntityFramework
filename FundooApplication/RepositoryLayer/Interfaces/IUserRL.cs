using System;
using System.Collections.Generic;
using System.Text;
using CommonLayer;
using CommonLayer.Models;

namespace RepositoryLayer.Interfaces
{
    public interface IUserRL
    {
        bool UserRegister(UserModel user);
        public IEnumerable<UserModel> GetAll();
        public UserModel Get(long id);
        public bool Update(UserModel user, UserModel entity);
        public void Delete(UserModel user);
        public UserModel Authenticate(string email, string password);
    }
}
