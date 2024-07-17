using MyPlayMarket.Core;
using MyPlayMarket.Core.Entities;
using MyPlayMarket.Core.Entities.DTO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyPlayMarket.Core.IRepository
{
    public interface IUserRepository
    {
        //public Task<List<User>> GetUsersQueryable(Func<IQueryable<User>, IQueryable<User>> expression);
        //public Task<IEnumerable<User>> GetAllUsersAsync();
        public Task<bool> UserAddAsync(User user);
        public Task<User> GetUserByEmailAsync(string email);
    }
}
