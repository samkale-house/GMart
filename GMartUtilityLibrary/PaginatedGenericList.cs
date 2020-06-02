using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace GMartUtilityLibrary
{
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
        public static PaginatedGenericList<T> Create(IQueryable<T> totalItemList, int currentPageIndex, int pageSize)
        {
            var totalRowCount = totalItemList.Count();
            //create a page
            var currentPageItems =  totalItemList.Skip((currentPageIndex - 1) * pageSize).Take(pageSize).ToList();
            return new PaginatedGenericList<T>(currentPageItems, totalRowCount, currentPageIndex, pageSize);

        }
    }
}
