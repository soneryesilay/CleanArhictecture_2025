using CleanArhictecture_2025.Application.Behaviors;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArhictecture_2025.Application
{
    public static class ApplicationRegistrar
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {

            //mediatr kütüphanesi için
            services.AddMediatR(conf =>
            {
                conf.RegisterServicesFromAssemblies(typeof(ApplicationRegistrar).Assembly);
                conf.AddOpenBehavior(typeof(ValidationBehavior<,>));
            });

            //validasyon kuralları için
            //Bunu kullanmazsak validation behavior (IEnumerable<IValidator<TRequest>>)interface'ini kullanarak erişim sağlayamıyor!
            services.AddValidatorsFromAssembly(typeof(ApplicationRegistrar).Assembly);

            return services;
        }
    }
}
