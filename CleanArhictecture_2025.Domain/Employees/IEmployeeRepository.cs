using GenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CleanArhictecture_2025.Domain.Employees;

//CRUD+Get işlemlerini bu repositoryde kullanıyoruz (Ana İşlemler)
public interface IEmployeeRepository : IRepository<Employee>   //Taner Saydam GenericRepository Package
{

}

