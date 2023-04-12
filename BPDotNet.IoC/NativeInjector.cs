using BPDotNet.Application.DTO.Response;
using BPDotNet.Application.Mapping.ToDTO;
using BPDotNet.Application.Services;
using BPDotNet.Application.Services.Abstracts;
using BPDotNet.Common.Mapping;
using BPDotNet.Core.Entities;
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

            #region Mappers

            services.AddScoped<ISimpleMap<User, UserResponseDto>, UserEntityToDtoMap>();

            #endregion
        }
    }
}