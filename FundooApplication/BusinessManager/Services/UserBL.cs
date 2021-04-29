using System;
using System.Collections.Generic;
using System.Text;
using BusinessManager.Interfaces;
using CommonLayer;
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

        public Model SampleApi(Model model)
        {
            return this.userRL.SampleApi(model);
        }
    }
}
