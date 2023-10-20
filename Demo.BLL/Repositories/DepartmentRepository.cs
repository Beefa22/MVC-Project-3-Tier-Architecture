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
    public class DepartmentRepository :GenericRepository<Department>,IDepartmentRepository
    {
        private readonly MvcApplicationDbContext _dbContext;

        public DepartmentRepository(MvcApplicationDbContext dbContext):base(dbContext)//Dependancy enjection=> Ask From CLR to create Obj from DbContext
        {
            _dbContext = dbContext;
        }

    }
}
