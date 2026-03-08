using Microsoft.EntityFrameworkCore;
using PhoneCompany.Infrastructure.Interfaces;
using PhoneCompany.Infrastructure.Specifications;

namespace PhoneCompany.Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly DbContext _context;
        protected readonly DbSet<T> _dbSet;

        public Repository(PhoneCompanyDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public virtual async Task<T?> GetSingleAsync(
            Specification<T> specification,
            CancellationToken cancellationToken)
        {
            var query = _dbSet
                .AsNoTracking()
                .Where(specification.ToExpression());

            query = specification.Includes
                .Aggregate(query, (current, include) => current.Include(include));

            var entity = await query
                .FirstOrDefaultAsync(cancellationToken);

            return entity;
        }

        public virtual async Task<(IEnumerable<T> Items, int TotalPages)> GetAsync(
            Specification<T> specification,
            int pageIndex,
            int pageSize,
            CancellationToken cancellationToken)
        {
            var query = _dbSet
                .AsNoTracking()
                .Where(specification.ToExpression());

            query = specification.Includes
                .Aggregate(query, (current, include) => current.Include(include));

            var totalCount = await query.CountAsync(cancellationToken);
            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

            var items = await query
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            return (items, totalPages);
        }

        public virtual async Task CreateAsync(
            T entity,
            CancellationToken cancellationToken)
        {
            await _dbSet.AddAsync(entity, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);
        }

        public virtual async Task UpdateAsync(
            T entity,
            CancellationToken cancellationToken)
        {
            _dbSet.Update(entity);

            await _context.SaveChangesAsync(cancellationToken);
        }

        public virtual async Task DeleteAsync(
            T entity,
            CancellationToken cancellationToken)
        {
            _dbSet.Remove(entity);

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
