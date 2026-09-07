using LabResultsService.Services.Interfaces;
using LabResultsService.Services.PatientServices;
using Microsoft.Extensions.DependencyInjection;
using LabResultsServiceImpl = LabResultsService.Services.Services.LabResultsService;

namespace LabResultsService.Services
{
    public static class ServicesDependencyInjection
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IPatientService, PatientService>();
            services.AddScoped<ILabResultsService, LabResultsServiceImpl>();
        }
    }
}
