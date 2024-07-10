using Database;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Data.Services.BaseServices
{
    public class Repository<TModel, TKey> : IRepository<TModel, TKey> where TModel : class
    {
        private readonly ApplicationDbContext _context;

        public Repository(ApplicationDbContext context)
        {
            _context = context;
        }



        public TModel GetById(TKey id)
        {
            return _context.Set<TModel>().Find(id)!;
        }

        public IEnumerable<TModel> GetAll()
        {
            return _context.Set<TModel>().ToList();
        }

        public IEnumerable<TModel> Find(Expression<Func<TModel, bool>> predicate)
        {
            return _context.Set<TModel>().Where(predicate);
        }

        public void Add(TModel entity)
        {
            _context.Set<TModel>().Add(entity);
        }

        public void AddRange(IEnumerable<TModel> entities)
        {
            _context.Set<TModel>().AddRange(entities);
        }

        public void Remove(TModel entity)
        {
            _context.Set<TModel>().Remove(entity);
        }

        public void RemoveRange(IEnumerable<TModel> entities)
        {
            _context.Set<TModel>().RemoveRange(entities);
        }

        public void Update(TModel entity)
        {
            _context.Set<TModel>().Update(entity);
        }

        public TModel SingleOrDefault(Expression<Func<TModel, bool>> predicate)
        {
            return _context.Set<TModel>().SingleOrDefault(predicate)!;
        }

        public TModel FirstOrDefault(Expression<Func<TModel, bool>> predicate)
        {
            return _context.Set<TModel>().FirstOrDefault(predicate)!;
        }

        public TModel LastOrDefault(Expression<Func<TModel, bool>> predicate)
        {
            return _context.Set<TModel>().LastOrDefault(predicate)!;
        }

        public int Count(Expression<Func<TModel, bool>> predicate)
        {
            return _context.Set<TModel>().Count(predicate);
        }

        public bool Any(Expression<Func<TModel, bool>> predicate)
        {
            return _context.Set<TModel>().Any(predicate);
        }

        public IQueryable<TModel> Query()
        {
            return _context.Set<TModel>();
        }


        public IEnumerable<TModel> Paginate(int page, int pageSize)
        {
            return _context.Set<TModel>()
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }

        public IEnumerable<TModel> Paginate(Expression<Func<TModel, bool>> predicate, int page, int pageSize)
        {
            return _context.Set<TModel>()
                .Where(predicate)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }

        public IQueryable<TModel> Include(params Expression<Func<TModel, object>>[] includeProperties)
        {
            IQueryable<TModel> query = _context.Set<TModel>();
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public IQueryable<TModel> Include(string includeProperties)
        {
            IQueryable<TModel> query = _context.Set<TModel>();
            foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public void LoadReference(TModel entity, Expression<Func<TModel, object>> property)
        {
            _context.Entry(entity).Reference(property).Load();
        }

        public void LoadCollection(TModel entity, Expression<Func<TModel, IEnumerable<object>>> property)
        {
            _context.Entry(entity).Collection(property).Load();
        }

        public async Task<TModel> GetAsync(Expression<Func<TModel, bool>> expression)
        {
            return await _context.Set<TModel>().FirstOrDefaultAsync(expression);
        }

        public async Task<IEnumerable<TModel>> GetAllAsync(Expression<Func<TModel, bool>> expression)
        {
            return await _context.Set<TModel>().Where(expression).ToListAsync();
        }


    }
}
