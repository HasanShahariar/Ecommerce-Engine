using Ecommerce.Models.Common;
using Ecommerce.Models.Common.Paging;
using Ecommerce.Models.Entities.Setup;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.BLL.Abstraction.Base
{
    public interface IManager<T> : IDisposable where T:class
    {
        Task<Result> Add(T entity);
        Task<bool> AddAsync(T entity);
        Task<Result> Update(T entity);
        Task<bool> UpdateAsync(T entity);
        Task<Result> Remove(long id);
        Task<Result> Remove(int id);
        Task<ICollection<T>> GetAll();
        Task<T> GetById(long id);
        Task<T> GetById(int id);
        Task<T> GetFirstorDefault(Expression<Func<T, bool>> predicate);
        Task<bool> RemoveAsync(T entity);
        Task<Result> AddRangeAsync(ICollection<T> entities);
        Task<Result> UpdateRangeAsync(ICollection<T> entity);
    }
}
