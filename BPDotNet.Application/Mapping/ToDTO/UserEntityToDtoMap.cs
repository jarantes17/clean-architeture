using BPDotNet.Application.DTO.Response;
using BPDotNet.Common.Mapping;
using BPDotNet.Core.Entities;

namespace BPDotNet.Application.Mapping.ToDTO;

public class UserEntityToDtoMap : ISimpleMap<User, UserResponseDto>
{
    public UserResponseDto Map(User source)
    {
        throw new System.NotImplementedException();
    }
}