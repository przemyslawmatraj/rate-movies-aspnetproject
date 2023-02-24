using ASPNetProject.Data.Base;
using ASPNetProject.Models;
using Microsoft.EntityFrameworkCore;

namespace ASPNetProject.Data.Services;

public class ActorsServices: EntityBaseRepository<Actor>, IActorService
{

    public ActorsServices(Context db): base(db) {}
    
    
}