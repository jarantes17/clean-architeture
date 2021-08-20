using System.Collections.Generic;
using BPDotNet.Core.Entities;
using BPDotNet.Core.Interfaces.Repositories;
using BPDotNet.Data.Contexts;

namespace BPDotNet.Data.Repositories
{
    public class UserRepository: Repository<User>, IUserRepository
    {
        public UserRepository(EfContext context) : base(context)
        { }

        public IEnumerable<User> GetAll()
        {
            return Query(x => !x.IsDeleted);
        }
    }
}