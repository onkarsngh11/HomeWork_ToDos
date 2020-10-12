using HomeWork_ToDos.CommonLib.Models.DbModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HomeWork_ToDos.DAL.DbContexts.Configurations
{
    internal class LabelEntityTypeConfiguration : IEntityTypeConfiguration<LabelDbModel>
    {
        /// <summary>
        /// COnfigures LabelDbModel
        /// </summary>
        /// <param name="builder"></param>
        public void Configure(EntityTypeBuilder<LabelDbModel> builder)
        {
            builder.HasData(new LabelDbModel
            {
                LabelId = 1,
                CreatedBy = 1,
                Description = "Review"
            },
            new LabelDbModel
            {
                LabelId = 2,
                CreatedBy = 1,
                Description = "Buy"
            },
            new LabelDbModel
            {
                LabelId = 3,
                CreatedBy = 1,
                Description = "Explore"
            },
            new LabelDbModel
            {
                LabelId = 4,
                CreatedBy = 1,
                Description = "Discover"
            });
        }
    }
}
