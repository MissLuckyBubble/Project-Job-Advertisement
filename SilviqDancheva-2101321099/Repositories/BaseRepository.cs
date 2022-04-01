using Microsoft.EntityFrameworkCore;
using SilviqDancheva_2101321099.DB;
using SilviqDancheva_2101321099.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SilviqDancheva_2101321099.Repositories
{
    public class BaseRepository<T>
        where T : BaseEntity
    {
        protected DbContext Context { get; set; }
        protected DbSet<T> Items { get; set; }

        public BaseRepository()
        {
            Context = new MyDbContext();
            Items = Context.Set<T>();
        }
        public List<JobAd> GetJobAd<JobAd>(Expression<Func<T, bool>> where, Expression<Func<T, JobAd>> select)
        {
            IQueryable<T> query = Items;

            return query.Where(where).Select(select).ToList();
        }

        public List<User> GetUsers<User>(Expression<Func<T, bool>> where, Expression<Func<T, User>> select)
        {
            IQueryable<T> query = Items;

            return query.Where(where).Select(select).ToList();
        }

        public List<T> GetAll(Expression<Func<T, bool>> filter = null,
                              Expression<Func<T, bool>> filter2 = null,
                              Expression<Func<T, object>> orderBy = null,
                              int page = 1,
                              int itemsPerPage = Int32.MaxValue)
        {
            IQueryable<T> query = Items;

            if (filter != null)
                query = query.Where(filter);

            if (filter2 != null)
                query = query.Where(filter2);

            if (orderBy != null)
                query = query.OrderBy(orderBy);

            return query
                .Skip(itemsPerPage * (page - 1))
                .Take(itemsPerPage)
                .ToList();
        }
        public List<T> Pager(List<T> items, int page = 1, int itemsPerPage = Int32.MaxValue)
        {
            return items
                .Skip(itemsPerPage * (page - 1))
                .Take(itemsPerPage)
                .ToList();
        }
        public T GetFirstOrDefault(Expression<Func<T, bool>> filter = null)
        {
            IQueryable<T> query = Items;

            if (filter != null)
                query = query.Where(filter);

            return query.FirstOrDefault();

        }

        public int Count(Expression<Func<T, bool>> filter = null)
        {
            IQueryable<T> query = Items;

            if (filter != null)
                query = query.Where(filter);

            return query.Count();
        }

        public void Save(T item)
        {
            if (item.Id > 0)
                Items.Update(item);
            else
                Items.Add(item);

            Context.SaveChanges();
        }

        public void Delete(T item)
        {
            Items.Remove(item);
            Context.SaveChanges();
        }
    }
}