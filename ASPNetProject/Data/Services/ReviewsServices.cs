using ASPNetProject.Data.Base;
using ASPNetProject.Models;

namespace ASPNetProject.Data.Services;

public class ReviewsServices: EntityBaseRepository<Review>, IReviewsService
{
    public ReviewsServices(Context context) : base(context)
    {
        
    }
}