using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JimmyLinq
{
    public static class ComicAnalyzer
    {
        private static PriceRange CalculatePriceRange(Comic comic, IReadOnlyDictionary<int,decimal> prices)
        {
            if (prices[comic.Issue]<100)
            {
                return PriceRange.Cheap;
            }
            else
            {
                return PriceRange.Expensive;
            }
           

        }

        public static IEnumerable<IGrouping<PriceRange,Comic>> GroupComicsByPrice(IEnumerable<Comic> catalog, IReadOnlyDictionary<int,decimal> prices)
        {
            var group =  catalog.OrderBy(comic => prices[comic.Issue]).GroupBy(comic => CalculatePriceRange(comic, prices));
            return group;
            
            //var group =
            //    from comic in catalog
            //    orderby prices[comic.Issue]
            //    group comic by CalculatePriceRange(comic,prices) into comicGroup
            //    select comicGroup;
            //return group;
        }

        public static IEnumerable<string> GetReviews(IEnumerable<Comic> catalog, IEnumerable<Review> reviews)
        {
            var group = catalog.OrderBy(comic => comic.Issue).Join(reviews, comic => comic.Issue, review => review.Issue, (comic, review) => $"{review.Critic} rated #{comic.Issue} '{comic.Name}' {review.Score:0.00}");     
            ////var group =
            ////    from comic in catalog
            ////    orderby comic.Issue
            ////    join review in reviews
            ////    on comic.Issue equals review.Issue
            ////    select $"{review.Critic} rated #{comic.Issue} '{comic.Name}' {review.Score:0.00}";
            return group;
        }
    }
}
