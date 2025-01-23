using CleanArhictecture_2025.Domain.Employees;
using FluentValidation;
using FluentValidation.Validators;
using GenericRepository;
using Mapster;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TS.Result;

namespace CleanArhictecture_2025.Application.Employees;

//requestin diğer katmanlar tarafından da erişilebilmesi lazım
    public sealed record EmployeeCreateCommand(
    string FirstName,
    string LastName,
    DateOnly BirthOfDate,
    decimal Salary,
    PersonelInformation PersonelInformation,
    Address? Address): IRequest<Result<string>>;
         
//Validasyon kuarlı public olmaz ise çalışmaz!
public sealed class EmployeeCreatCommandValidator : AbstractValidator<EmployeeCreateCommand>
{
    public EmployeeCreatCommandValidator()
    {
      RuleFor(x => x.FirstName).MinimumLength(3).WithMessage("Ad alanı en az 3 karakter olmalıdır!");
      RuleFor(x => x.LastName).MinimumLength(3).WithMessage("Soyad alanı en az 3 karakter olmalıdır!");
      RuleFor(x => x.PersonelInformation.TCNo)
            .MinimumLength(11)
            .WithMessage("Geçerli bir TC numarası yazın!")
            .MaximumLength(11)
            .WithMessage("Geçerli bir TC numarası yazın!");
    }
}
//handlerin başka bir katmandan erişilememesi lazım bu yüzden internal kullandık!
internal sealed class EmployeeCreateCommandHandler(
    IEmployeeRepository employeeRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<EmployeeCreateCommand, Result<string>>
{
    public async Task<Result<string>> Handle(EmployeeCreateCommand request, CancellationToken cancellationToken)
    {
        //Tekil olup olamam durumunu kontrol ettik
        var isEmployeeExists = await employeeRepository.AnyAsync(p=>p.PersonelInformation.TCNo == request.PersonelInformation.TCNo, cancellationToken);
        if (isEmployeeExists)
        {
            return Result<string>.Failure("Bu Tc numarası daha önce kaydedilmiş");
        }

        //Objemizi objeden objeye aktaran bir mappleme kütüphanesiyle yaptık.
        Employee employee = request.Adapt<Employee>();
       
        //async yapıyı kullanmayarak ekleme işleminde hız kazandık
        employeeRepository.Add(employee);

        //async yapıyı kullanarak işlem hacmimizi genişlettik
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return "Personel kaydı başarıyla tamamlandı!";
    }
}

