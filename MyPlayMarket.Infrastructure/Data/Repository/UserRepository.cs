using Microsoft.EntityFrameworkCore;
using MyPlayMarket.Infrastructure.Data.IRepository;
using MyPlayMarket.Infrastructure.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyPlayMarket.Infrastructure.Data.Repository
{
    public class UserRepository:IUserRepository
    {
        //private readonly ApplicationDbContext _db;
        //public UserRepository(ApplicationDbContext db)
        //{
        //    _db = db;
        //}
        //public async Task<List<User>> GetUsersQueryable(Func <IQueryable<User>, IQueryable<User>> expression)
        //{
        //    try
        //    {
        //        var query = expression(_db.User);
        //        return (await query.ToListAsync());

        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception($"Couldn't retrieve entities: {ex.Message}");
        //    }
        //}
        ////public async Task<int> GetGamesCountAsync(Func<IQueryable<Game>, IQueryable<Game>> sortPageExpression)
        ////{
        ////    try
        ////    {
        ////        return await sortPageExpression(_db.Games).CountAsync();
        ////    }
        ////    catch (Exception ex)
        ////    {
        ////        throw new Exception($"Couldn't retrieve entities: {ex.Message}");
        ////    }

        ////}
        //public async Task<IEnumerable<User>> GetAllUsersAsync()
        //{
        //    try
        //    {
        //        return await _db.User.AsNoTracking().ToListAsync();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception($"Couldn't retrieve entities: {ex.Message}");
        //    }

        //}
        //public async Task<User> GetUserByEmailAsync(User entity)
        //{
        //    try
        //    {
        //        return await _db.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Email == Email);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception($"Couldn't retrieve entities: {ex.Message}");
        //    }

        //}
        //public async Task<bool> UserAddAsync(User entity)
        //{
        //    try
        //    {
        //        if (await _db.Users.FirstOrDefaultAsync(x => x.UserName == entity.UserName) != null)
        //        {
        //            await _db.Users.AddAsync(entity);
        //            await _db.SaveChangesAsync();
        //            return true;
        //        }
        //        else
        //        {
        //            throw new Exception();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception($"{nameof(entity)} could not be saved: {ex.Message}");
        //    }
        //}

        //public async Task<bool> UpdateUserAsync(User entity)
        //{
        //    try
        //    {
        //        _db.Users.Update(entity);
        //        await _db.SaveChangesAsync();
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception($"{nameof(entity)} could not be updated: {ex.Message}");
        //    }
        //}
        //public async Task<bool> DeleteUserAsync(int id)
        //{
        //    try
        //    {
        //        var user = await _db.Users.FirstOrDefaultAsync(x => x.UserName == UserName);
        //        if (user == null)
        //        {
        //            throw new Exception($"{id} is not exist");
        //        }
        //        _db.Users.Remove(user);
        //        await _db.SaveChangesAsync();
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception($"{id} could not be saved: {ex.Message}");
        //    }
        //}
    }
}

