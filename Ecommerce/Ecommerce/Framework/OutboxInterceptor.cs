using Ecommerce.Domain;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Ecommerce.Framework;

public class OutboxInterceptor: SaveChangesInterceptor
{
    public override ValueTask<int> SavedChangesAsync(SaveChangesCompletedEventData eventData, int result,
        CancellationToken cancellationToken = new CancellationToken())
    {
        var dbContext = eventData.Context;

        foreach (var entry in dbContext.ChangeTracker.Entries())
        {

        }
        return base.SavedChangesAsync(eventData, result, cancellationToken);
    }
}