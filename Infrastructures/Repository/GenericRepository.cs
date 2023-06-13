using Application.Interface;
using Application.InterfaceRepository;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructures.Repository
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        protected DbSet<TEntity> _dbSet;
        private readonly ICurrentTime _timeService;
        protected readonly IClaimService _claimsService;
        public GenericRepository(AppDbContext dbContext, ICurrentTime timeService, IClaimService claimsService)
        {
            _dbSet = dbContext.Set<TEntity>();
            _timeService = timeService;
            _claimsService = claimsService;
        }

        public async Task AddAsync(TEntity entity)
        {
            entity.CreationDate = _timeService.GetCurrentTime();
            entity.CreatedBy = _claimsService.GetCurrentUserId;
            await _dbSet.AddAsync(entity);
        }

        public async Task AddRangeAsync(List<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                entity.CreationDate = _timeService.GetCurrentTime();
                entity.CreatedBy = _claimsService.GetCurrentUserId;
            }
            await _dbSet.AddRangeAsync(entities);
        }

        public async  Task<List<TEntity>> FindAsync(Expression<Func<TEntity, bool>> expression, params Expression<Func<TEntity, object>>[] includes)
        {
            return await includes
            .Aggregate(_dbSet.AsQueryable(),
                (entity, property) => entity.Include(property))
            .Where(expression).Where(x => x.IsDeleted == false).ToListAsync();

        }

        public async Task<List<TEntity>> GetAllAsync(params Expression<Func<TEntity, object>>[] includes)
        {
            return await includes
           .Aggregate(_dbSet.AsQueryable(),
               (entity, property) => entity.Include(property))
           .Where(x => x.IsDeleted == false)
           .ToListAsync();
        }

      

        public async  Task<TEntity?> GetByIdAsync(Guid id, params Expression<Func<TEntity, object>>[] includes)
        {
            //return result;
            return await includes
               .Aggregate(_dbSet.AsQueryable(),
                   (entity, property) => entity.Include(property))
               .AsNoTracking()
               .FirstOrDefaultAsync(x => x.Id.Equals(id) && x.IsDeleted == false);
        }
        public Task<TEntity?> GetByIdAsync(Guid id)
        {
            return this.GetByIdAsync(id, Array.Empty<Expression<Func<TEntity, object>>>());
        }

        public void SoftRemove(TEntity entity)
        {
            entity.IsDeleted = true;
            entity.DeleteBy = _claimsService.GetCurrentUserId;
            _dbSet.Update(entity);
        }

        public void SoftRemoveRange(List<TEntity> entities)
        {
            foreach(var entity in entities)
            {
                entity.IsDeleted = true;
                entity.DeleteBy = _claimsService.GetCurrentUserId;
            }
            _dbSet.UpdateRange(entities);
        }

        public void Update(TEntity entity)
        {
            entity.ModificationDate = _timeService.GetCurrentTime();
            entity.ModificationBy = _claimsService.GetCurrentUserId;
            _dbSet.Update(entity);
        }

        public void UpdateRange(List<TEntity> entities)
        {
            foreach(var entity in entities)
            {
                entity.ModificationDate = _timeService.GetCurrentTime();
                entity.ModificationBy = _claimsService.GetCurrentUserId;
            }
            _dbSet.UpdateRange(entities);
        }


    }
}
