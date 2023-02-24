using ASPNetProject.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ASPNetProject.Data.Base;

public class EntityBaseRepository<T>: IEntityBaseRepository<T> where T: class, IEntityBase, new()
{
    private readonly Context _db;
    
    public EntityBaseRepository(Context db)
    {
        _db = db;
    }

    
    public async Task<IEnumerable<T>> GetAllAsync()
    {
        var result = await _db.Set<T>().ToListAsync();
        return result;
    }

    public async Task<T> GetByIdAsync(int id)
    {
        var result = await _db.Set<T>().FindAsync(id);
        return result;
    }
    

    public async Task AddAsync(T entity)
    {
        await _db.Set<T>().AddAsync(entity);
        await _db.SaveChangesAsync();
    }

    public async Task UpdateAsync(int id, T entity)
    {
        EntityEntry dbEntityEntry =  _db.Entry<T>(entity);
        dbEntityEntry.State = EntityState.Modified;
        await _db.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await _db.Set<T>().FindAsync(id);
        EntityEntry dbEntityEntry =  _db.Entry<T>(entity);
        dbEntityEntry.State = EntityState.Deleted;
        await _db.SaveChangesAsync();
    }
}