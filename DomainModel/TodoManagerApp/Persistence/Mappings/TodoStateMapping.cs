using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TodoManagerApp.Persistence.State;

namespace TodoManagerApp.Persistence.Mappings;

// public class TodoStateMapping : IEntityTypeConfiguration<TodoState>
// {
//     public void Configure(EntityTypeBuilder<TodoState> builder)
//     {
//         builder
//         .Property(p => p.StateId)
//         .Property(e => e.IsDeleted)
//         .HasQueryFilter(user => EF.Property<bool>(user, "IsDeleted") == false)
//         .Property(e => e.CreateAt)
//         .Property(e => e.RowVersion);
//     }
// }

