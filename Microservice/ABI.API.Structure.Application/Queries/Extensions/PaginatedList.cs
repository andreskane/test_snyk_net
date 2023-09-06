using System;
using System.Collections.Generic;
using System.Linq;

namespace ABI.API.Structure.Application.Queries.Extensions
{
    public class PaginatedList<T>
    {
        public int CurrentPage { get; private set; }
        public int From { get; private set; }
        public List<T> Items { get; private set; }
        public int PageSize { get; private set; }
        public int To { get; private set; }
        public int TotalCount { get; private set; }
        public int TotalPages { get; private set; }

        public PaginatedList(List<T> items, int count, int currentPage, int pageSize)
        {
            CurrentPage = currentPage;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            TotalCount = count;
            PageSize = pageSize;
            From = ((currentPage - 1) * pageSize) + 1;
            To = (From + pageSize) - 1;

            Items = items;
        }

        public bool HasPreviousPage
        {
            get
            {
                return (CurrentPage > 1);
            }
        }

        public bool HasNextPage
        {
            get
            {
                return (CurrentPage < TotalPages);
            }
        }

        public static PaginatedList<T> Create(List<T> source, int currentPage, int pageSize, string sortOn, string sortDirection)
        {
            var count = source.Count;

            var type = typeof(T);
            sortOn = string.IsNullOrEmpty(sortOn) ? "" : sortOn;
            var sortProperty = type.GetProperty(sortOn);

            if (sortProperty != null)
            {
                sortDirection = string.IsNullOrEmpty(sortDirection) ? "ASC" : sortDirection;
                if (sortDirection.ToUpper() == "ASC")
                    source = source.OrderBy(p => sortProperty.GetValue(p, null)).ToList();
                else
                    source = source.OrderByDescending(p => sortProperty.GetValue(p, null)).ToList();
            }

            source = source.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();

            var items = source.ToList();

            return new PaginatedList<T>(items, count, currentPage, pageSize);
        }
    }
}
