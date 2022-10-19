using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JimmyLinq
{
    internal static class ComicAnalyzer
    {
        private static PriceRange CalculatePriceRange(Comic comic)
        {
            return Comic.Prices[comic.Issue] < 100M ? PriceRange.Cheap : PriceRange.Expensive;

        }

        public static IEnumerable<IGrouping<PriceRange,Comic>> GroupComicsByPrice(IEnumerable<Comic> catalog, IReadOnlyDictionary<int,decimal> prices)
        {
            var group =
                from comic in catalog
                orderby prices[comic.Issue]
                group comic by CalculatePriceRange(comic) into comicGroup
                select comicGroup;
            return group;
        }

        public static IEnumerable<string> GetReviews(IEnumerable<Comic> catalog, IEnumerable<Review> reviews)
        {
            var group =
                from comic in catalog
                orderby comic.Issue
                join review in reviews
                on comic.Issue equals review.Issue
                select $"{review.Critic} rated #{comic.Issue} '{comic.Name} {review.Score:0.00}'";
            return group;
        }
    }
}
