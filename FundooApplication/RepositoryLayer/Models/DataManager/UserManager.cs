using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RepositoryLayer.Models.Repository;

namespace RepositoryLayer.Models.DataManager
{
    public class UserManager : IDataRepository<User>
    {
        readonly UserContext _userContext;
        public UserManager(UserContext context)
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
        public void Add(User entity)
        {
            _userContext.Users.Add(entity);
            _userContext.SaveChanges();
        }
        public void Update(User user, User entity)
        {
            user.FirstName = entity.FirstName;
            user.LastName = entity.LastName;
            user.Email = entity.Email;
            user.Gender = entity.Gender;
            user.Password = entity.Password;
            _userContext.SaveChanges();
        }
        public void Delete(User user)
        {
            _userContext.Users.Remove(user);
            _userContext.SaveChanges();
        }
    }
}
