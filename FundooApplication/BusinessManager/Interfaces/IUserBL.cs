using System;
using System.Collections.Generic;
using System.Text;
using CommonLayer;
using RepositoryLayer.Models;

namespace BusinessManager.Interfaces
{
    public interface IUserBL/*<TEntity>*/
    {
       /* IEnumerable<TEntity> GetAll();
        TEntity Get(long id);
        void Add(TEntity entity);
        void Update(TEntity dbEntity, TEntity entity);*/
        bool UserRegister(User user);
       // void Delete(TEntity entity);
    }
}
