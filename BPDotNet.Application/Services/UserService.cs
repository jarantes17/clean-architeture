using System.Collections.Generic;
using System.Linq;
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

        public UserService(IUserRepository userRepository,
            ISimpleMap<User, UserResponseDto> userEntityToDtoMap
        )
        {
            _userRepository = userRepository;
            _userEntityToDtoMap = userEntityToDtoMap;
        }


        public List<UserResponseDto> GetAll()
        {
            var users = _userRepository.GetAll();
            var userViewModels = users.Select(user => _userEntityToDtoMap.Map(user)).ToList();

            return userViewModels;
        }
    }
}