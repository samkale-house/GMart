using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace GMartDataLibrary.Repository
{
   public class Repository<T>:IRepository<T> where T:class
    {
        private readonly GMartDbContext _db;
        DbSet<T> dbset;

        public Repository(GMartDbContext db)//from DI configuration in startup of client e.g. startup of of MVC or webapi app
        {
            _db = db;
            this.dbset = _db.Set<T>();
        }

        public void Add(T entity)
        {
            dbset.Add(entity);
        }

        public IQueryable<T> GetAll()
        {
            return dbset;
        }

        public IQueryable<T> GetAll(string includeproperties, Expression<Func<T, bool>> searchFilterQuery = null, Func<IQueryable<T>, IOrderedQueryable<T>> OrderByQuery = null)
        {

            IQueryable<T> query = dbset;

            //apply serach filter
            if (searchFilterQuery != null)
            {
                query = query.Where(searchFilterQuery);
            }

            //load the datasets for each navigation property 
            if (!string.IsNullOrEmpty(includeproperties))
            {
                var includepropertiesCollection = includeproperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);//get properties separated by comma
                foreach (string includeproperty in includepropertiesCollection)
                {
                    query = query.Include(includeproperty);
                }
            }

            //apply sorting
            if (OrderByQuery != null)
            {
                return OrderByQuery(query);//.ToList()
            }

            return query;//.ToList();
        }

        public T GetbyId(int id)
        {
            return dbset.Find(id);
        }

        public T GetFirstorDefault(string includeproperties, Expression<Func<T, bool>> searchFilterQuery = null)
        {
            IQueryable<T> query = dbset;
            if(searchFilterQuery!=null)
            {
                query = query.Where(searchFilterQuery);
            }
            if(string.IsNullOrEmpty(includeproperties))
            {
                foreach(string property in includeproperties.Split(new char[] {','}, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(property);
                }
            }
            return query.FirstOrDefault();
        }

        public void Remove(T entity)
        {
            dbset.Remove(entity);
        }
    }
}
