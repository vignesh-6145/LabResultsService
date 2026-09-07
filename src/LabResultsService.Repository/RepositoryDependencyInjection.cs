using LabResultsService.Repository.Data;
using LabResultsService.Repository.Interfaces;
using LabResultsService.Repository.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace LabResultsService.Repository
{
    public static class RepositoryDependencyInjection
    {
        public static void RegisterContext(this IServiceCollection services, string connectionString)
        {
            ArgumentNullException.ThrowIfNullOrWhiteSpace(connectionString, nameof(connectionString));

            services.AddDbContext<LabResultsDbContext>(options =>
                options.UseSqlServer(connectionString));
        }

        public static void RegisterRepositories(this IServiceCollection services)
        {
            services.AddScoped<IPatientRepository, PatientRepository>();
        }

    }
}
