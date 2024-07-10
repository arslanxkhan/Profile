using System.Linq.Expressions;

namespace Data.Services.BaseServices
{
    public interface IRepository<TModel, TKey> where TModel : class
    {
        Task<TModel> GetAsync(Expression<Func<TModel, bool>> expression);
        TModel GetById(TKey id);
        IEnumerable<TModel> GetAll();
        Task<IEnumerable<TModel>> GetAllAsync(Expression<Func<TModel, bool>> expression);
        IEnumerable<TModel> Find(Expression<Func<TModel, bool>> predicate);
        void Add(TModel entity);
        void AddRange(IEnumerable<TModel> entities);
        void Remove(TModel entity);
        void RemoveRange(IEnumerable<TModel> entities);
        void Update(TModel entity);
        TModel SingleOrDefault(Expression<Func<TModel, bool>> predicate);
        TModel FirstOrDefault(Expression<Func<TModel, bool>> predicate);
        TModel LastOrDefault(Expression<Func<TModel, bool>> predicate);
        int Count(Expression<Func<TModel, bool>> predicate);
        bool Any(Expression<Func<TModel, bool>> predicate);
        IQueryable<TModel> Query();
        IEnumerable<TModel> Paginate(int page, int pageSize);
        IEnumerable<TModel> Paginate(Expression<Func<TModel, bool>> predicate, int page, int pageSize);
        IQueryable<TModel> Include(params Expression<Func<TModel, object>>[] includeProperties);
        IQueryable<TModel> Include(string includeProperties);
        void LoadReference(TModel entity, Expression<Func<TModel, object>> property);
        void LoadCollection(TModel entity, Expression<Func<TModel, IEnumerable<object>>> property);

    }
}
