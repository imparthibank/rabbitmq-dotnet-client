using IdentityManagement.Infrastructure.DbContexts;
using IdentityManagement.SharedKernel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityManagement.Infrastructure.Repositories
{
    public class EfRepository : IRepository
    {
        private readonly IdentityManagementContext dbContext;
        public EfRepository(IdentityManagementContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public T GetById<T>(Guid id) where T : BaseEntity
        {
            return dbContext.Set<T>().SingleOrDefault(e => e.Id == id);
        }
        public Task<bool> FindById<T>(Guid id) where T : BaseEntity
        {
            return dbContext.Set<T>().AsNoTracking().Where(e => e.Id == id).AnyAsync();
        }
        public Task<T> GetByIdAsync<T>(Guid id) where T : BaseEntity
        {
            return dbContext.Set<T>().SingleOrDefaultAsync(e => e.Id == id);
        }

        public Task<List<T>> ListAsync<T>() where T : BaseEntity
        {
            return dbContext.Set<T>().ToListAsync();
        }

        public async Task<T> AddAsync<T>(T entity) where T : BaseEntity
        {
            await dbContext.Set<T>().AddAsync(entity);
            await dbContext.SaveChangesAsync();

            return entity;
        }

        public async Task UpdateAsync<T>(T entity) where T : BaseEntity
        {
            dbContext.Entry(entity).State = EntityState.Modified;
            await dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync<T>(T entity) where T : BaseEntity
        {
            dbContext.Set<T>().Remove(entity);
            await dbContext.SaveChangesAsync();
        }
        public async Task<bool> PersonalEmailDuplicateCheck(string email)
        {
            bool isValid = true;
            var duplicateEmail = await dbContext.Users.AsNoTracking()
                                                .Where(e => e.PersonalEmail.ToLower() == email.ToLower())
                                                .AnyAsync();
            return isValid = !duplicateEmail;
        }
        public async Task<bool> PersonalEmailModificationCheck(string email, Guid userId)
        {
            bool isModify = false;
            var exsistByUserId = await dbContext.Users.AsNoTracking()
                                                .Where(e => e.PersonalEmail.ToLower() == email.ToLower() && e.Id == userId)
                                                .AnyAsync();
            var exsistByOtherUser = await dbContext.Users.AsNoTracking()
                                    .Where(e => e.PersonalEmail.ToLower() == email.ToLower() && e.Id != userId)
                                    .AnyAsync();
            if (exsistByUserId)
                isModify = false;
            if (exsistByOtherUser)
                isModify = true;
            return isModify == false;
        }
    }
}
