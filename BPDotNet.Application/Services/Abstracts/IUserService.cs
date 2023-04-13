using System.Collections.Generic;
using System.Threading.Tasks;
using BPDotNet.Application.DTO.Request;
using BPDotNet.Application.DTO.Response;

namespace BPDotNet.Application.Services.Abstracts
{
    public interface IUserService
    {
        Task<IEnumerable<UserResponseDto>> GetAllAsync();
        Task<UserResponseDto> GetOneAsync(int id);
        Task<UserResponseDto> CreateAsync(CreateUserRequestDto user);
    }
}