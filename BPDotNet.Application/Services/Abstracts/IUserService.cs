using System.Collections.Generic;
using BPDotNet.Application.ViewModels;

namespace BPDotNet.Application.Services.Abstracts
{
    public interface IUserService
    {
        List<UserViewModel> GetAll();
    }
}