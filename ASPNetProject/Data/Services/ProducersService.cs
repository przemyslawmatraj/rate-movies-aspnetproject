using ASPNetProject.Data.Base;
using ASPNetProject.Models;

namespace ASPNetProject.Data.Services;

public class ProducersService: EntityBaseRepository<Producer>, IProducersService
{
    public ProducersService(Context db): base(db) {}
}