using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PromocodesApp.Interfaces
{
    public interface IServiceService<T> : IService<T>
    {
        Task<T> GetByName(string name);
        Task<IList<T>> GetInfiniteScroll(int page, int elements);
    }
}
