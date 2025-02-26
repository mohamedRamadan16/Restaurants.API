namespace Restaurants.Application.Common
{
    public class PagedResults<T>
    {
        public PagedResults(IEnumerable<T> items, int pageNumber, int pageSize, int totalCount)
        {
            Items = items;
            TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
            TotalItemsCount = totalCount;
            ItemsFrom = pageSize * (pageNumber - 1) + 1;
            ItemsTo = ItemsFrom + pageSize - 1;
        }
        public IEnumerable<T> Items { get; set; }
        public int TotalPages { get; set; }
        public int TotalItemsCount { get; set; }
        public int ItemsFrom { get; set; }
        public int ItemsTo { get; set; }
    }
}
