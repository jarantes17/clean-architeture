using System.Collections.Generic;
using BPDotNet.Core.Entities;

namespace BPDotNet.Core.Interfaces.Repositories
{
    public interface IUserRepository
    {
        IEnumerable<User> GetAll();
    }
}