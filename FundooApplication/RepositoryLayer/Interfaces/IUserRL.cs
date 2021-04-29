using System;
using System.Collections.Generic;
using System.Text;
using CommonLayer;
using RepositoryLayer.Models;

namespace RepositoryLayer.Interfaces
{
    public interface IUserRL/*<TEntity>*/
    {
        /*IEnumerable<TEntity> GetAll();
        TEntity Get(long id);
        void Add(TEntity entity);
        void Update(TEntity dbEntity, TEntity entity);
        void Delete(TEntity entity);*/
        bool UserRegister(User user);
    }
}
