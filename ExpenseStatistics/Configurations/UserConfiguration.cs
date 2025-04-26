using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ExpenseStatistics.Domain.Entities;

namespace ExpenseStatistics.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);
            builder.Property(u => u.Email).IsRequired().HasMaxLength(100);
            builder.Property(u => u.PasswordHash).IsRequired();
            builder.HasMany(u => u.Transactions).WithOne(t => t.User).HasForeignKey(t => t.UserId);
            builder.HasMany(u => u.Categories).WithOne(c => c.User).HasForeignKey(c => c.UserId);
        }
    }
}