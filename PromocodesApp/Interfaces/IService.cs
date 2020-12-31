using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PromocodesApp.Interfaces
{
    public interface IService<T>
    {
        string CurrentUserName();
        Task<IList<T>> Get();
        Task<T> Get(int id);
        Task<T> Put(int id, T itm);
        bool Exists(int id);
        Task<T> Post(T itm);
        Task<bool> Delete(int id);
    }
}
