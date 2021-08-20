using BPDotNet.Application.Services;
using BPDotNet.Application.Services.Abstracts;
using BPDotNet.Core.Interfaces.Repositories;
using BPDotNet.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace BPDotNet.IoC
{
    public static class NativeInjector
    {
        public static void RegisterDependencies(IServiceCollection services)
        {
            #region Services

            services.AddScoped<IUserService, UserService>();

            #endregion

            #region Repositories

            services.AddScoped<IUserRepository, UserRepository>();

            #endregion
        }
    }
}