using Demo.BLL.Interfaces;
using Demo.DAL.Contexts;
using Demo.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Repositories
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        //private readonly MvcApplicationDbContext _dbContext;//this field protected in GenericRepo so we can use it 

        public EmployeeRepository(MvcApplicationDbContext dbContext):base(dbContext)//Dependancy enjection=> Ask From CLR to create Obj from DbContext
        {
            //_dbContext = dbContext;
        }
        public IQueryable<Employee> GetEmployeesByAdress(string address)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Employee> SearchEmployeeByName(string name)
        =>_dbContext.Employees.Where(N => N.Name.ToLower().Contains(name.ToLower()));
        
     
        
    }
}
