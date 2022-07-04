using Microsoft.EntityFrameworkCore;
using OnionArchitect.Domain.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionArchitect.DataAccess.RepositoryPattern
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        public DbSet<T> _dbSet;

        public Repository(ApplicationDbContext context, DbSet<T> dbSet)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _dbSet = dbSet ?? throw new ArgumentNullException(nameof(dbSet));
        }


        public async Task Add(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            await _dbSet.AddAsync(entity);
            _context.SaveChanges();
        }

        public async Task Delete(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            _dbSet.Remove(entity);
            _context.SaveChanges();
        }

        public Task<IEnumerable<T>> GetAlls()
        {
            throw new NotImplementedException();
        }

        public Task<T> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task Update(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
