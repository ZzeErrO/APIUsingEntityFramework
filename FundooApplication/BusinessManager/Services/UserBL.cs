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
        /*
                public void Add(User entity)
                {
                    throw new NotImplementedException();
                }

                public void Delete(User entity)
                {
                    throw new NotImplementedException();
                    //return this.userRL.Delete(entity);
                }

                public User Get(long id)
                {
                    //throw new NotImplementedException();
                    return this.userRL.Get(id);
                }

                public IEnumerable<User> GetAll()
                {
                    //throw new NotImplementedException();
                    return this.userRL.GetAll();
                }

                /*public Model SampleApi(Model model)
                {
                    return this.userRL.SampleApi(model);
                }*/
        /*
                public void Update(User dbEntity, User entity)
                {
                    throw new NotImplementedException();
                }*/

        public bool UserRegister(User user)
        {
            return this.userRL.UserRegister(user);
        }
    }
}
