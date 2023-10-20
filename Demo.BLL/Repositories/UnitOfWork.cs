using Demo.BLL.Interfaces;
using Demo.DAL.Contexts;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Repositories
{
    public class UnitOfWork : IUnitOfWork 
    {
        private readonly MvcApplicationDbContext _dbContext;

        //Automatic Property
        //Note there is an issue we need to solve it after API
        public IEmployeeRepository EmployeeRepository { get ; set; }
        public IDepartmentRepository DepartmentRepository { get ; set ; }

        public UnitOfWork(MvcApplicationDbContext dbContext)
        {
            EmployeeRepository = new EmployeeRepository(dbContext);
            DepartmentRepository = new DepartmentRepository(dbContext);
            _dbContext = dbContext;
        }

        public async Task<int> Complete()
        {
            return await _dbContext.SaveChangesAsync();
        }
        
        public void Dispose()       //This function to Close DataBase Connection that CLR Created it in heap
           => _dbContext.Dispose();
    }
}
