using core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations
{
    public class CourseConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.Property(c => c.Description)
                .IsRequired()
                .HasMaxLength(5000);

            builder.HasKey(c => c.Id);

            builder.HasOne(p => p.Author)
                .WithMany(c => c.Courses)
                .HasForeignKey(c => c.AuthorId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(c => c.Cover)
                .WithOne(i => i.Course)
                .HasForeignKey<Cover>(c => c.CourseId);

            builder.HasMany(c => c.Tags)
                .WithMany(t => t.Courses)
                .UsingEntity(m => m.ToTable("CourseTags"));
        }
    }
}