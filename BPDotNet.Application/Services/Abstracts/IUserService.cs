using System.Collections.Generic;
using BPDotNet.Application.DTO.Response;

namespace BPDotNet.Application.Services.Abstracts
{
    public interface IUserService
    {
        List<UserResponseDto> GetAll();
    }
}