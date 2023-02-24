using ASPNetProject.Data.Base;
using ASPNetProject.Models;

namespace ASPNetProject.Data.Services;

public class CinemasServices: EntityBaseRepository<Cinema>, ICinemasService
{
    public CinemasServices(Context context) : base(context)
    {
        
    }
}