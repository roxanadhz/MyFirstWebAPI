using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Common.Exceptions;
using Common.Utilities;
using Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext dbContext)
            : base(dbContext)
        {
        }
        public Task<User> GetByUserAndPass(string username, string password, CancellationToken cancellationToken)
        {

            var passwordHash = SecurityHelper.GetSha256Hash(password);
            return Table.Where(p => p.UserName == username && p.PasswordHash == passwordHash).SingleOrDefaultAsync(cancellationToken);

        }
        public async Task AddAsync(User user, string passwor, CancellationToken cancellationToken)
        {
            var exists = await TableNoTracking.AnyAsync(p => p.UserName == user.UserName);
            if (exists)
                throw new BadRequestException("Profile name is repetitive");

            var passwordHash = SecurityHelper.GetSha256Hash(passwor);
            user.PasswordHash = passwordHash;
            await base.AddAsync(user, cancellationToken);
        }

    }
}
