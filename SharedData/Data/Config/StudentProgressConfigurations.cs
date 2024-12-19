using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SharedData.Data.Models;

namespace SharedData.Data.Config
{
    public class StudentProgressConfigurations : IEntityTypeConfiguration<StudentProgress>
    {
        public void Configure(EntityTypeBuilder<StudentProgress> builder)
        {
            builder.HasKey( primary => 
                new { primary.LessonId, primary.StudentId });

            builder.Property(x => x.Progress)
               .HasColumnType("int")
               .HasDefaultValue(0);


            //relationship with student
            builder.HasOne(x => x.Student)
                .WithMany(x => x.studentProgresses)
                .HasForeignKey(x => x.StudentId)
                .OnDelete(DeleteBehavior.Cascade);


            //relationship with Lesson
            builder.HasOne(x => x.Lesson)
                .WithMany(x => x.studentProgresses)
                .HasForeignKey(x => x.LessonId)
                .OnDelete(DeleteBehavior.Cascade);


            ////relationship with Subject
            //builder.HasOne(x => x.Subject)
            //    .WithMany(x => x.studentProgresses)
            //    .HasForeignKey(x => x.SubjectId)
            //    .OnDelete(DeleteBehavior.Cascade);  
            

            builder.ToTable("StudentProgress");



        }
    }


}
