using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BPDotNet.Core.Entities;

namespace BPDotNet.Core.Interfaces.Repositories
{
    public interface IUserRepository: IRepository<User>
    {
        IEnumerable<User> GetAll();
        public Task<IEnumerable<User>> GetAllAsync();

    }
}