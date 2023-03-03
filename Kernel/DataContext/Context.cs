using Kernel.DataContext.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Canducci.Pagination;

namespace Kernel.DataContext
{
    internal abstract class Context<TEntity> where TEntity : class, new() 
    {
        private protected Repositorio _context;

        internal Context(Repositorio rep)
        {
            _context = rep;
        }

        public virtual TEntity Salvar(TEntity entidade)
        {
            _context.Set<TEntity>().Add(entidade);
            return entidade;
        }
        public virtual TEntity Alterar(TEntity entidade)
        {
            _context.Entry(entidade).State = EntityState.Modified;
            return entidade;
        }

        public virtual void Deletar(TEntity entidade)
        {
            _context.Set<TEntity>().Remove(entidade);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
        public virtual TEntity Obter(Expression<Func<TEntity, bool>> filter = null, string includeProperties = "")
        {
            int quantity;
            return ExecutarConsulta(filter, null, null, null, includeProperties, out quantity).FirstOrDefault();
        }

        public virtual TEntity Obter(int id)
        {
            return _context.Set<TEntity>().Find(id);
        }

        public virtual IEnumerable<TEntity> Listar(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, int? skip = null, int? take = null, string includeProperties = "")
        {
            int quantity;
            return ExecutarConsulta(filter, orderBy, skip, take, includeProperties, out quantity).ToList();
        }

        public virtual IQueryable<TEntity> ExecutarConsulta(Expression<Func<TEntity, bool>> filter, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy, int? skip, int? take, string includeProperties, out int quantity)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            quantity = query.Count();

            if (orderBy != null)
            {
                if (skip != null && take != null)
                    return orderBy(query).Skip(skip.Value).Take(take.Value);

                if (skip != null && take == null)
                    return orderBy(query).Skip(skip.Value);

                if (skip == null && take != null)
                    return orderBy(query).Take(take.Value);

                return orderBy(query);
            }

            if (skip != null && take != null)
                return query.Skip(skip.Value).Take(take.Value);

            if (skip != null && take == null)
                return query.Skip(skip.Value);

            if (skip == null && take != null)
                return query.Take(take.Value);

            return query;

        }

    }
}
