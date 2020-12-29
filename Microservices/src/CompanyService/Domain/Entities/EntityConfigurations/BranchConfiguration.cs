using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CompanyService.Domain.Entities.EntityConfigurations
{
    public class BranchConfiguration
    {
        internal static void Build(ModelBuilder builder)
        {
            builder.Entity<Branch>(b =>
                                   {
                                       b.Property(p => p.Id).HasDefaultValueSql("newid()")
                                        .IsRequired();

                                       b.Property(p => p.Name)
                                        .IsRequired();

                                       b.Property(p => p.Location)
                                        .IsRequired();

                                       b.Property(p => p.CompanyId)
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