using Demo.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Interfaces
{
    public interface IGenericRepository<T> where T:class
    {
        //Common Five Signiture 
        Task<IEnumerable<T>> GetAll();

        Task<T> GetById(int id);

        void Update(T item);

        void Delete(T item);

        Task Add(T item);
    }
}
