using HomeWork_ToDos.CommonLib.Models.DbModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HomeWork_ToDos.DAL.DbContexts.Configurations
{
    internal class MapLabelsToItemEntityTypeConfiguration : IEntityTypeConfiguration<MapLabelsToItemDbModel>
    {
        /// <summary>
        /// Configures MapLabelsToItemDbModel
        /// </summary>
        /// <param name="builder"></param>
        public void Configure(EntityTypeBuilder<MapLabelsToItemDbModel> builder)
        {
            builder.ToTable("MapLabelsToItems");
            builder.HasKey(x => x.ItemMappingId);

            builder.HasOne(t => t.Users)
                .WithMany()
                .HasForeignKey(t => t.CreatedBy)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(t => t.Labels)
                .WithMany()
                .HasForeignKey(t => t.LabelId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(t => t.ToDoItems)
                .WithMany(t => t.Labels)
                .HasForeignKey(t => t.ToDoItemId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
