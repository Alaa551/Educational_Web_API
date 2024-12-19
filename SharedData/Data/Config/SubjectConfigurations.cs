using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SharedData.Data.Models;

namespace SharedData.Data.Config
{
    public class SubjectConfigurations : IEntityTypeConfiguration<Subject>
    {
        public void Configure(EntityTypeBuilder<Subject> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.SubjectName)
                .HasColumnType("NVARCHAR")
                .HasMaxLength(255)
                .IsRequired();

            builder.ToTable("Subjects");



        }
    }


}
