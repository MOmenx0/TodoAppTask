using AGI.Morn.Application.Common.Interfaces;
using AGI.Morn.Infrastructure.Data;
using AGI.Morn.Infrastructure.Repository.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGI.Morn.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastrctureServices(this IServiceCollection services, IConfiguration configuration)
        {
            string DefaultConnecation = configuration.GetConnectionString("Connecation");
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(DefaultConnecation);
            });

            services.AddTransient<IunitOfWork,UnitOfWork>();
            services.AddTransient(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            return services;
        }
    }
}
