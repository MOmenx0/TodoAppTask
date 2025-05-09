using AGI.Morn.Application.Common.Behaviours;
using AGI.Morn.Application.Common.Interfaces;
using AGI.Morn.Application.Common.Specifications;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AGI.Morn.Application
{
    public static class DependencyInjecrtion
    {
        public static IServiceCollection AddApplicationServices (this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            

            services.AddMediatR(configuration =>
            {
                configuration.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
                configuration.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));

            });

            services.AddSingleton(typeof(ISpecifications<>), typeof(Specifications<>));

            return services;
        }
    }
}
