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

        public bool UserRegister(UserRegistration user)
        {
            try
            {
                return this.userRL.UserRegister(user);
            }
            catch (Exception ex)
            {
                throw ex;
            }
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
            try
            {
                return this.userRL.Update(user, entity);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void Delete(UserModel user)
        {
            this.userRL.Delete(user);        
        }

        public UserModel Authenticate(string email, string password)
        {
            try
            {
                return userRL.Authenticate(email, password);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool ForgetPassword(string emailAddress)
        {
            try
            {
                bool result = this.userRL.ForgetPassword(emailAddress);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool ResetPassword(string email, string resetPassword)
        {
            try
            {
                return this.userRL.ResetPassword(email, resetPassword);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
