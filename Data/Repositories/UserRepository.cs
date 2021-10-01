using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
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
        public Task AddAsync(User user, string passwor, CancellationToken cancellationToken)
        {
            var passwordHash = SecurityHelper.GetSha256Hash(passwor);
            user.PasswordHash = passwordHash;
            return base.AddAsync(user, cancellationToken);
        }

    }
}
