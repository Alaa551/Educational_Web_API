using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SharedData.Data.Models;

namespace SharedData.Data.Config
{
    public class AccountConfigurations : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {

            builder.Property(x => x.FullName)
                .HasColumnType("NVARCHAR")
                .HasMaxLength(255);

            builder.HasOne(x => x.Student)
                .WithOne(x => x.Account)
                .HasForeignKey<Student>(x => x.AccountId)
                .IsRequired();

            builder.ToTable("Accounts");
        }
    }


}
