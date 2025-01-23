using CleanArhictecture_2025.Domain.Abstractions;
using CleanArhictecture_2025.Domain.Employees;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArhictecture_2025.Application.Employees;

                                                //Neden Listable değilde queryble
                                                 //sorguyu yapmadan müdahale etmemizi sağladığı için!
public sealed class EmployeeGetAllQuery() : IRequest<IQueryable<EmployeeGetAllQueryResponse>>;

//veriyi özelleştirmek için kullandık!
public sealed class EmployeeGetAllQueryResponse : BaseEntityDto
//Dto oluşturarark görüntülemede manipulasyona gidebiliyoruz istediğimizi çıkarıp ekledık
{
    public string FirstName { get; set; } = default!; 
    public string LastName { get; set; } = default!;
    public DateOnly BirthOfDate { get; set; }
    public decimal Salary { get; set; }
    public string TCNo { get; set; } = default!;
}

internal sealed class EmployeeGetAllQueryHandler(
    IEmployeeRepository employeeRepository) : IRequestHandler<EmployeeGetAllQuery, IQueryable<EmployeeGetAllQueryResponse>>
{
    public Task<IQueryable<EmployeeGetAllQueryResponse>> Handle(EmployeeGetAllQuery request, CancellationToken cancellationToken)
    {
        var response = employeeRepository.GetAll()
            .Select(s => new EmployeeGetAllQueryResponse
            {
                FirstName = s.FirstName,
                LastName = s.LastName,
                Salary = s.Salary,
                BirthOfDate = s.BirthOfDate,
                CreateAt = s.CreateAt,
                DeleteAt = s.DeleteAt,
                Id = s.Id,
                IsDeleted = s.IsDeleted,
                TCNo = s.PersonelInformation.TCNo,
                UpdateAt = s.UpdateAt,
            })
            .AsQueryable();

        return Task.FromResult(response);
    }
}