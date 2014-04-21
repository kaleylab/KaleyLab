using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KaleyLab.Data
{
    public class PagedResult<T> where T : class
    {
        public PagedResult()
        {
            this.PagedData = new List<T>();
        }

        public PagedResult(int totalRecords, int pageSize, int pageNumber, IEnumerable<T> pagedData)
        {
            this.PageSize = pageSize;
            this.TotalRecords = totalRecords;
            this.PageNumber = pageNumber;
            this.PagedData = pagedData;
        }

        public int TotalRecords { get; set; }

        public int TotalPages
        {
            get
            {
                return (this.TotalRecords + this.PageSize - 1) / this.PageSize;
            }
        }

        public int PageSize { get; set; }

        public int PageNumber { get; set; }

        public IEnumerable<T> PagedData { get; set; }
    }
}
