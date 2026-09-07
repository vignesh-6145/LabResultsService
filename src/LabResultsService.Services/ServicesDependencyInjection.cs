using LabResultsService.Services.Interfaces;
using LabResultsService.Services.PatientServices;
using Microsoft.Extensions.DependencyInjection;

namespace LabResultsService.Services
{
    public static class ServicesDependencyInjection
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IPatientService, PatientService>();
        }
    }
}
