using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CompanyService.Domain.Entities.EntityConfigurations
{
    public class CompanyConfiguration
    {
        internal static void Build(ModelBuilder builder)
        {
            builder.Entity<Company>(b =>
                                    {
                                        b.Property(p => p.Id).HasDefaultValueSql("newid()")
                                         .IsRequired();

                                        b.Property(p => p.CompanyName)
                                         .IsRequired();

                                        b.Property(p => p.Ceo)
                                         .IsRequired();

                                        b.Property(p => p.Logo)
                                         .IsRequired();

                                        b.Property(p => p.Status)
                                         .IsRequired()
                                         .HasDefaultValue(0);

                                        b.Property(p => p.NoOfEmployee)
                                         .IsRequired()
                                         .HasDefaultValue(0);

                                        b.Property(p => p.UserId)
                                         .IsRequired();

                                        b.Property(p => p.CreatedAtUtc).HasDefaultValueSql("getutcdate()");
                                        b.Property(p => p.CreatedAtUtc).Metadata.SetBeforeSaveBehavior(PropertySaveBehavior.Throw);
                                        b.Property(p => p.CreatedAtUtc).Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Throw);
                                        b.Property(p => p.LastModifiedAtUtc).Metadata.SetBeforeSaveBehavior(PropertySaveBehavior.Throw);

                                        b.HasKey(k => k.Id);
                                    });
        }
    }
}