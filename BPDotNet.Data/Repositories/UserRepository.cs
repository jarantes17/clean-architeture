using System.Collections.Generic;
using System.Threading.Tasks;
using BPDotNet.Core.Entities;
using BPDotNet.Core.Interfaces.Repositories;
using BPDotNet.Data.Contexts;
using Microsoft.EntityFrameworkCore;

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
        
        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await Query(x => !x.IsDeleted).ToListAsync();
        }
    }
}