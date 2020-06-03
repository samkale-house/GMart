using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;

namespace GMartDataLibrary.Repository
{
    public interface IRepository<T> where T:class
    {
        T GetbyId(int id);
        void Add(T etity);

        void AddRange(IEnumerable<T> entities);

        
       IQueryable<T> GetAll();
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);


        /// <summary>
        /// This function is the efficient way of retriving all data based on user input,as it loads only the data required to work.It works best for large datasets as loadtime gets reduced.
        /// </summary>
        /// <param name="includeproperties">string that holds all navigation property names</param>
        /// <param name="searchFilterQuery">lambda function query based on search filter</param>
        /// <param name="OrderByQuery">lambda function query based on sortorder on column</param>
        /// <returns>returns IEnumerable result</returns>
        IEnumerable<T> GetAll(string includeproperties, //for navigation properties(foreignkeyreference)
                                       Expression<Func<T, bool>> searchFilterQuery = null, //search filter 
                                       Func<IQueryable<T>, IOrderedQueryable<T>> OrderByQuery = null   //orderby query for sorting
                                       );
        T GetFirstorDefault(string includeproperties,Expression<Func<T,bool>> searchFilterQuery=null);        
        

    }
}
