using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TodoManagerApp.Persistence.State;

namespace TodoManagerApp.Persistence.Mappings;

// public class TodoListStateMapping : IEntityTypeConfiguration<TodoListState>
// {
//     public void Configure(EntityTypeBuilder<TodoListState> builder)
//     {
//         builder
//             .Property(e => e.Id)
//                 .ValueGeneratedNever()
//                 .IsRequired()
//             .Property(e => e.Name).IsRequired()
//             .Property(p => p.PersistenceId)
//             .Property(e => e.IsDeleted)
//             .HasQueryFilter(user => EF.Property<bool>(user, "IsDeleted") == false)
//             .Property(e => e.CreateAt)
//             .Property(e => e.RowVersion);
//     }
// }
