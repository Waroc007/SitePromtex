using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PromtexSite.Models
{
    public class PageViewModel<T>
    {
        public IEnumerable<T> Items { get; set; }
        public int PageNumber { get; private set; }
        public int TotalPages { get; private set; }

        public PageViewModel(int count, int pageNumber, int pageSize, IEnumerable<T> items)
        {
            PageNumber = pageNumber;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            Items = items;
        }

        public List<int> PageViews()
        {
            List<int> pages = new List<int>();
            pages.Add(PageNumber);
            if (PageNumber + 1 <= TotalPages)
            {
                pages.Add(PageNumber + 1);
            }
            else if ((TotalPages - PageNumber + 1) <= TotalPages)
            {
                pages.Add(TotalPages - PageNumber + 1);
            }
            if (PageNumber + 2 <= TotalPages)
            {
                pages.Add(PageNumber + 2);
            }
            else if((TotalPages - PageNumber + 2) <= TotalPages)
            {
                pages.Add(TotalPages - PageNumber + 2);
            }
            if (PageNumber - 1 <= 0)
            {
                pages.Add(PageNumber - 1 + TotalPages);
            }
            else if((PageNumber - 1) > 0)
            {
                pages.Add(PageNumber - 1);
            }
            if (PageNumber - 2 <= 0)
            {
                pages.Add(PageNumber - 2 + TotalPages);
            }
            else if ((PageNumber - 2) > 0)
            {
                pages.Add(PageNumber - 2);
            }
            pages = pages.Distinct().ToList();
            pages.Sort();
            
            return pages;
        }

        public bool HasPreviousPage
        {
            get
            {
                return (PageNumber > 1);
            }
        }

        public bool HasNextPage
        {
            get
            {
                return (PageNumber < TotalPages);
            }
        }
    }
}
