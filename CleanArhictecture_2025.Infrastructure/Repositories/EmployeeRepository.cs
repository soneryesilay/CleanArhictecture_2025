using CleanArhictecture_2025.Domain.Employees;
using CleanArhictecture_2025.Infrastructure.Context;
using GenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArhictecture_2025.Infrastructure.Repositories;

//repository de ana işlemler yapılır (database işlemlerini yapar)
//serviste gelen requesti kontrol ederiz
internal sealed class EmployeeRepository : Repository<Employee, ApplicationDbContext>, IEmployeeRepository
{
    public EmployeeRepository(ApplicationDbContext context) : base(context)
    {
    }
}
