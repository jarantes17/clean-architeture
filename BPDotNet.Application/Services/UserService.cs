using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BPDotNet.Application.DTO.Request;
using BPDotNet.Application.DTO.Response;
using BPDotNet.Application.Services.Abstracts;
using BPDotNet.Common.Mapping;
using BPDotNet.Core.Entities;
using BPDotNet.Core.Interfaces.Repositories;

namespace BPDotNet.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ISimpleMap<User, UserResponseDto> _userEntityToDtoMap;
        private readonly ISimpleMap<CreateUserRequestDto, User> _userDtoToEntityMap;

        public UserService(IUserRepository userRepository,
            ISimpleMap<User, UserResponseDto> userEntityToDtoMap,
            ISimpleMap<CreateUserRequestDto, User> userDtoToEntityMap
        )
        {
            _userRepository = userRepository;
            _userEntityToDtoMap = userEntityToDtoMap;
            _userDtoToEntityMap = userDtoToEntityMap;
        }

        public async Task<IEnumerable<UserResponseDto>> GetAllAsync()
        {
            var users = await _userRepository.GetAllAsync();
            return users.Select(user => _userEntityToDtoMap.Map(user)).ToList();
        }

        public async Task<UserResponseDto> GetOneAsync(int id)
        {
            return _userEntityToDtoMap.Map(await _userRepository.GetAsync(x => x.Id == id));
        }

        public async Task<UserResponseDto> CreateAsync(CreateUserRequestDto user)
        {
            var userToCreate = _userDtoToEntityMap.Map(user);
            var createdUser = await _userRepository.CreateAsync(userToCreate);
            return _userEntityToDtoMap.Map(createdUser);
        }
    }
}