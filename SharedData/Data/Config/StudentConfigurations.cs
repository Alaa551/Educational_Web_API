using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SharedData.Data.Models;

namespace SharedData.Data.Config
{
    public class StudentConfigurations : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
           builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .HasColumnType("NVARCHAR")
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(x => x.Score)
                .HasColumnType("int")
                .HasDefaultValue(0);

            builder.ToTable("Students");

        }
    }


}
