using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;


namespace DataAccess.Data.Interceptors
{
    public class AuditableEntityInterceptor: SaveChangesInterceptor
    {
        public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        {
            UpdateEntities(eventData.Context);

            return base.SavingChanges(eventData, result);
        }

        public void UpdateEntities(DbContext context)
        {
            if (context == null) return;

            foreach (var entry in context.ChangeTracker.Entries<BaseAuditableEntity>())
            {
                if (entry.State is EntityState.Added or EntityState.Modified)
                {
                    var utcNow = DateTime.UtcNow;
                    if (entry.State == EntityState.Added)
                    {                       
                        entry.Entity.CreatedAt = utcNow;
                        entry.Entity.IsActive = true;
                    }
                    else
                    {
                        entry.Entity.LastModifiedAt = utcNow;
                    }

                }
            }
        }
    }
}
