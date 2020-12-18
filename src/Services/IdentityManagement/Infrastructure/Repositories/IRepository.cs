using IdentityManagement.SharedKernel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IdentityManagement.Infrastructure.Repositories
{
    public interface IRepository
    {
        Task<T> GetByIdAsync<T>(Guid id) where T : BaseEntity;
        Task<bool> FindById<T>(Guid id) where T : BaseEntity;
        Task<List<T>> ListAsync<T>() where T : BaseEntity;
        Task<T> AddAsync<T>(T entity) where T : BaseEntity;
        Task UpdateAsync<T>(T entity) where T : BaseEntity;
        Task DeleteAsync<T>(T entity) where T : BaseEntity;
        Task<bool> PersonalEmailDuplicateCheck(string email);
        Task<bool> PersonalEmailModificationCheck(string email, Guid userId);
    }
}
