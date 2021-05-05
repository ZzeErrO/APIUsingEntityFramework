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
        public byte[] passwordHash, passwordSalt;
        //public static List<Password> keyhash = new List<Password>();
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
            User _user = new User()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Password = user.Password,
                //Gender = user.Gender,

            };
            _userContext.Users.Add(_user);
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
            //user.Gender = entity.Gender;
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

        public User Authenticate(string email, string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                return null;

            var user = _userContext.Users.SingleOrDefault(x => x.Email == email);
            // check if username exists
            if (user == null)
                return null;

            var _password = _userContext.Users.FirstOrDefault(e => e.Email == email).Password;
            // check if password is correct
            if (password == _password)
                return user;

            // authentication successful
            return user;
        }

        /*
        private static string CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
            //keyhash = { PasswordHash =  passwordHash, passwordSalt};
            return Convert.ToString(passwordHash);
        }

        private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
            if (storedHash.Length != 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
            if (storedSalt.Length != 128) throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordHash");

            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }

            return true;
        }
        */
    }
}
