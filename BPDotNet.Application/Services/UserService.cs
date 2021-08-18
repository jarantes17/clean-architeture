using System.Collections.Generic;
using System.Linq;
using BPDotNet.Application.Services.Abstracts;
using BPDotNet.Application.ViewModels;
using BPDotNet.Core.Interfaces.Repositories;

namespace BPDotNet.Application.Services
{
    public class UserService: IUserService
    {
        private readonly IUserRepository _userRepository;
        
        public UserService(IUserRepository userRepository)
        {
            this._userRepository = userRepository;
        }
        
        public List<UserViewModel> GetAll()
        {
            var users = this._userRepository.GetAll();
            
            var userViewModels = users.Select(x => new UserViewModel
            {
                Id = x.Id,
                Email = x.Email,
                Name = x.Name
            }).ToList();

            return userViewModels;
        }
    }
}