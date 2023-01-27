using Microsoft.EntityFrameworkCore;
using NDS.Models.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace NDS.Models.Repository
{
    public class CrudGenericMethod<Tentity> where Tentity : class
    {
        private readonly NDSDbContext _context;

        private DbSet<Tentity> _table;


        public CrudGenericMethod(NDSDbContext context)
        {
            _context = context;
            _table = context.Set<Tentity>();

        }



        public virtual void Create(Tentity entity)
        {
            _table.Add(entity);
        }


        public virtual void Update(Tentity entity)
        {
            if (_context.Entry(entity).State == EntityState.Detached)
            {
                _table.Attach(entity);
            }
            _context.Entry(entity).State = EntityState.Modified;
        }



        public virtual void Delete(Tentity entity)
        {
            if (_context.Entry(entity).State == EntityState.Detached)
            {
                _table.Attach(entity);
            }
            _table.Remove(entity);


        }


        public virtual void DeleteAll(IEnumerable<Tentity> entity)
        {
            _table.AttachRange(entity);
            _table.RemoveRange(entity);

        }

        public virtual Tentity GetLastEntity()
        {
            return _table.LastOrDefault();
        }



        public virtual void DeleteById(object id)
        {
            var entity = GetById(id);
            Delete(entity);
        }

        public virtual Tentity GetById(object id)
        {
            return _table.Find(id);
        }

        public virtual IEnumerable<Tentity> Get(Expression<Func<Tentity, bool>> whereVariable = null, Func<IQueryable<Tentity>,
            IOrderedQueryable<Tentity>> orderbyVariable = null, string joinString = "")
        {
            IQueryable<Tentity> query = _table;

            if (whereVariable != null)
            {
                query = query.Where(whereVariable);
            }

            if (orderbyVariable != null)
            {
                query = orderbyVariable(query);
            }

            if (joinString != "")
            {
                foreach (string joins in joinString.Split(','))
                {
                    query = query.Include(joins);
                }
            }

            return query.ToList();

        }

        internal void Create(object category)
        {
            throw new NotImplementedException();
        }

        public virtual async Task<IEnumerable<Tentity>> GetAsync(Expression<Func<Tentity, bool>> whereVariable = null, Func<IQueryable<Tentity>,
           IOrderedQueryable<Tentity>> orderbyVariable = null, string joinString = "")
        {
            IQueryable<Tentity> query = _table;

            if (whereVariable != null)
            {
                query = query.Where(whereVariable);
            }

            if (orderbyVariable != null)
            {
                query = orderbyVariable(query);
            }

            if (joinString != "")
            {
                foreach (string joins in joinString.Split(','))
                {
                    query = query.Include(joins);
                }
            }

            return await query.ToListAsync();

        }

        public virtual async Task<IEnumerable<Tentity>> GetWithSkipAsync(Expression<Func<Tentity, bool>> whereVariable = null, Func<IQueryable<Tentity>,
          IOrderedQueryable<Tentity>> orderbyVariable = null, string joinString = "", int skip = 0, int take = 0)
        {
            IQueryable<Tentity> query = _table;

            if (whereVariable != null)
            {
                query = query.Where(whereVariable);
            }

            if (orderbyVariable != null)
            {
                query = orderbyVariable(query);
            }

            if (joinString != "" && !string.IsNullOrEmpty(joinString))
            {
                foreach (string joins in joinString.Split(','))
                {
                    query = query.Include(joins);
                }
            }

            return await query.Skip(skip).Take(take).ToListAsync();

        }



        public virtual async Task<int> GetCountAsync(Expression<Func<Tentity, bool>> whereVariable = null)
        {

            if (whereVariable != null)
            {
                return await _table.Where(whereVariable).CountAsync();
            }
            else
            {
                return await _table.CountAsync();
            }

        }




        public virtual void Save()
        {
            _context.SaveChangesAsync();
        }


        //async

        public virtual async Task<IEnumerable<Tentity>> GetAllAsync()
        {
            return await _table.ToListAsync();
        }

        public virtual async Task<Tentity> GetAsync(Expression<Func<Tentity, bool>> where)
        {
            return await _table.Where(where).FirstOrDefaultAsync();
        }

        public virtual async Task<Tentity> GetSingleAsync()
        {
            return await _table.FirstOrDefaultAsync();
        }






        public virtual async Task<Tentity> GetLastEntityAsync(Expression<Func<Tentity, bool>> where)
        {
            return await _table.Where(where).LastOrDefaultAsync();
        }


        public virtual async Task<Tentity> GetLastEntityAsync()
        {
            return await _table.LastOrDefaultAsync();
        }





        public virtual async Task<Tentity> GetAsync(Expression<Func<Tentity, bool>> where, string joinstring = "")
        {

            IQueryable<Tentity> query = _table;

            query = query.Where(where);

            foreach (var item in joinstring.Split(','))
            {
                query = query.Include(item);
            }

            return await query.FirstOrDefaultAsync();
        }

        public virtual async Task<Tentity> GetByIdAsync(object Id)
        {
            return await _table.FindAsync(Id);
        }




        public virtual async Task<IEnumerable<Tentity>> GetManyAsync(Expression<Func<Tentity, bool>> where)
        {
            return await _table.Where(where).ToListAsync();
        }

        //........



        public virtual Tentity GetSingle(Expression<Func<Tentity, bool>> where, string join)
        {
            return _table.Where(where).Include(join).FirstOrDefault();
        }

        #region Dispose
        protected bool isDisposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!isDisposed)
            {
                _context.Dispose();
            }

            isDisposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion

    }
}
