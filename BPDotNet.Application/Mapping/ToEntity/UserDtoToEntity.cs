using BPDotNet.Application.DTO.Request;
using BPDotNet.Common.Mapping;
using BPDotNet.Core.Entities;

namespace BPDotNet.Application.Mapping.ToEntity;

public class UserDtoToEntity: ISimpleMap<CreateUserRequestDto, User>
{
    public User Map(CreateUserRequestDto source)
    {
        return new User
        {
            Name = source.Name,
            Email = source.Email,
            Password = source.Password
        };
    }
}