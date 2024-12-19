using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SharedData.Data.Models;

namespace SharedData.Data.Config
{
    public class LessonConfigurations : IEntityTypeConfiguration<Lesson>
    {
        public void Configure(EntityTypeBuilder<Lesson> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.LessonName)
                .HasColumnType("NVARCHAR")
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(x => x.Content)
                .HasColumnType("NVARCHAR");


            //relations
            builder.HasOne(x => x.Subject)
                .WithMany(x => x.Lessons)
                .HasForeignKey(x => x.SubjectId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.ToTable("Lessons");


        }
    }




}
