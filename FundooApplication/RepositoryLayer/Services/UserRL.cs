using System;
using System.Collections.Generic;
using System.Text;
using CommonLayer;
using RepositoryLayer.Interfaces;

namespace RepositoryLayer.Services
{
    public class UserRL : IUserRL
    {
        public Model SampleApi(Model model)
        {
            Model model1 = new Model()
            {
                mail = model.mail + "updated from RL",
                name = model.name + "updated from RL"
            };
            return model1;
        }
    }
}
