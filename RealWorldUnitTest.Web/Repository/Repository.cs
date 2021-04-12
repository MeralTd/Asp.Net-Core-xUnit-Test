using Microsoft.EntityFrameworkCore;
using RealWorldUnitTest.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealWorldUnitTest.Web.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly UnitTestDBContext _context;
        private readonly DbSet<TEntity> _dbSet;

        public Repository(UnitTestDBContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();

        }
        public async Task Create(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public void Delete(TEntity entity)
        {
            _dbSet.Remove(entity);
            _context.SaveChanges();
        }

        public async Task<IEnumerable<TEntity>> GetAll()
        {
           return await _dbSet.ToListAsync();
        }

        public async Task<TEntity> GetById(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public void Update(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            //_dbSet.Update(entity);
            _context.SaveChanges();
        }
    }
}
