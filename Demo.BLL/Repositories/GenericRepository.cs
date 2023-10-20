using Demo.BLL.Interfaces;
using Demo.DAL.Contexts;
using Demo.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Repositories
{
    public class GenericRepository<T>:IGenericRepository<T> where T:class
    {
        private protected readonly   MvcApplicationDbContext _dbContext; //Attribute

        public GenericRepository(MvcApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task Add(T item)
        
           => await _dbContext.Set<T>().AddAsync(item); 
            
        

        public void Delete(T item)
        
         =>   _dbContext.Set<T>().Remove(item);


        public async Task<IEnumerable<T>> GetAll()
        {
            if (typeof(T) == typeof(Employee))
                return  (IEnumerable<T>/* Casting*/)await _dbContext.Employees.Include(D => D.Department).ToListAsync();//Temporary Solution before using Sepcification DesignPattern
            else
                 return await _dbContext.Set<T>().ToListAsync(); 
        }

        public async Task<T> GetById(int id)
       => await _dbContext.Set<T>().FindAsync(id);


        public void Update(T item)
        
            =>_dbContext.Set<T>().Update(item);
        
    }
}
