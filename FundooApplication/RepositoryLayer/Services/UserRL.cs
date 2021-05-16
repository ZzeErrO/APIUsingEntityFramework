using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonLayer;
using CommonLayer.Models;
using RepositoryLayer.Interfaces;
using RepositoryLayer.MSMQSenderReceiver;

namespace RepositoryLayer.Services
{
    public class UserRL : IUserRL
    {
        readonly UserContext _userContext;
        public UserRL(UserContext context)
        {
            _userContext = context;
        }

        public IEnumerable<UserModel> GetAll()
        {
            return _userContext.Users.ToList();
        }

        public UserModel Get(long id)
        {
            return _userContext.Users
                  .FirstOrDefault(e => e.UserId == id);
        }

        public bool UserRegister(UserRegistration user)
        {
            UserModel _user = new UserModel()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Password = user.Password,

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

        public bool Update(UserModel user, UserModel entity)
        {
            user.FirstName = entity.FirstName;
            user.LastName = entity.LastName;
            user.Email = entity.Email;
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

        public void Delete(UserModel user)
        {
            _userContext.Users.Remove(user);
            _userContext.SaveChanges();
        }
        public UserModel Authenticate(string email, string password)
        {
            try
            {
                if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                    return null;

                var _password = _userContext.Users.FirstOrDefault(e => e.Email == email).Password;
                // check if password is correct
                if (password != _password)
                    return null;

                var user = _userContext.Users.SingleOrDefault(x => x.Email == email);
                // check if email exists
                if (user == null)
                {
                    throw new FundooCustomExceptions(FundooCustomExceptions.ExceptionType.INVALID_EMAIL, "Email is Invalid");
                }

                // authentication successful
                return user;
            }
            catch (Exception ex)
            {
                throw new FundooCustomExceptions(FundooCustomExceptions.ExceptionType.INVALID_EMAIL, "Email is Invalid");
            }
        }

        public bool ForgetPassword(string emailAddress)
        {
            try
            {
                var checkEmail = this._userContext.Users.Where(x => x.Email == emailAddress).FirstOrDefault();

                if (checkEmail != null)
                {
                    MSMQSender.SendMessage();
                    string body = MSMQReceiver.receiverMessage();
                    EmailService.Email(emailAddress, body);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in base64Encode" + ex.Message);
            }
        }

        public bool ResetPassword(string email, string resetPassword)
        {
            try
            {
                var user = _userContext.Users.SingleOrDefault(x => x.Email == email);
                if (user == null)
                {
                    return false;
                }
                user.Password = resetPassword;
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
            catch(Exception ex)
            {
                throw ex;
            }
        }

        /*
        private string Encryptedpassword(string password)
        {
            int _password = password.GetHashCode();
            return _password.ToString();
        }*/

    }
}
