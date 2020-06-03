using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace GMartUtilityLibrary
{
    /// <summary>
    /// Class that extends List,to give the pagination functionality.Provides HasPreviousPage,HasNextPage,PageIndex and TotalPages properties.
    /// Provides create method that returns paginated list
    /// </summary>
    /// <typeparam name="T">T is a entity type for entityset to be paginated</typeparam>
    public class PaginatedGenericList<T>:List<T>
    {
        public int PageIndex;
        public int TotalPages;
        public PaginatedGenericList(List<T> itemList, int totalRowCount, int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            TotalPages = (int)Math.Ceiling(totalRowCount / (double)pageSize);//set totalpages count
            Console.WriteLine($"TotalPaes:{TotalPages}");
            this.AddRange(itemList);
        }
        public bool HasPreviousPage
        {
            get
            {
                return (PageIndex > 1);
            }
        }

        public bool HasNextPage
        {
            get
            {
                return (PageIndex < TotalPages);
            }
        }
        /// <summary>
        /// This will generate a Paginated List 
        /// </summary>
        /// <param name="totalItemList">Iqueryable dataset that needs to be paginated</param>
        /// <param name="currentPageIndex">current page number</param>
        /// <param name="pageSize">total number of records to be presented in a single page</param>
        /// <returns>PaginatedGenericList class object</returns>
        public static PaginatedGenericList<T> Create(IQueryable<T> totalItemList, int currentPageIndex, int pageSize)
        {
            if (totalItemList != null)
            {
                var totalRowCount = totalItemList.Count();
                //create a page
                var currentPageItems = totalItemList.Skip((currentPageIndex - 1) * pageSize).Take(pageSize).ToList();
                return new PaginatedGenericList<T>(currentPageItems, totalRowCount, currentPageIndex, pageSize);
            }
            return new PaginatedGenericList<T>(null, 0, currentPageIndex, pageSize);

        }
    }
}
