using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonLayer;
using RepositoryLayer.Interfaces;
using RepositoryLayer.Models;

namespace RepositoryLayer.Services
{
    public class UserRL : IUserRL
    {
        readonly UserContext _userContext;
        public UserRL(UserContext context)
        {
            _userContext = context;
        }

        public IEnumerable<User> GetAll()
        {
            return _userContext.Users.ToList();
        }

        public User Get(long id)
        {
            return _userContext.Users
                  .FirstOrDefault(e => e.UserId == id);
        }

        public bool UserRegister(User user)
        {
            _userContext.Users.Add(user);
            int result =  _userContext.SaveChanges();
            if (result <= 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool Update(User user, User entity)
        {
            user.FirstName = entity.FirstName;
            user.LastName = entity.LastName;
            user.Email = entity.Email;
            user.Gender = entity.Gender;
            user.Password = entity.Password;
            int result = _userContext.SaveChanges();
            if (result <= 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public void Delete(User user)
        {
            _userContext.Users.Remove(user);
            _userContext.SaveChanges();
        }

    }
}
