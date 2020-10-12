using HomeWork_ToDos.CommonLib.Models.DbModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HomeWork_ToDos.DAL.DbContexts.Configurations
{
    internal class MapLabelsToListEntityTypeConfiguration : IEntityTypeConfiguration<MapLabelsToListDbModel>
    {
        /// <summary>
        /// Configures MapLabelsToListDbModel
        /// </summary>
        /// <param name="builder"></param>
        public void Configure(EntityTypeBuilder<MapLabelsToListDbModel> builder)
        {
            builder.ToTable("MapLabelsToLists");
            builder.HasKey(x => x.ListMappingId);

            builder.HasOne(t => t.Users)
                .WithMany()
                .HasForeignKey(t => t.CreatedBy)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(t => t.Labels)
                .WithMany()
                .HasForeignKey(t => t.LabelId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(t => t.ToDoLists)
                .WithMany(t => t.Labels)
                .HasForeignKey(t => t.ToDoListId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
