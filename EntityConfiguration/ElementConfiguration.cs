namespace pickql;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class LegoElementConfiguration : IEntityTypeConfiguration<LegoElement>
{
    public void Configure(EntityTypeBuilder<LegoElement> builder)
    {
        builder.ToTable("elements");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasColumnName("element_id");
        builder.Property(x => x.DesignNumber).HasColumnName("design_number");
        builder.Property(x => x.ColorNumber).HasColumnName("color_number");
        builder.Property(x => x.PurchaseType).HasColumnName("purchase_type");
        builder.Property(x => x.Available);
        builder.Property(x => x.Json).HasColumnType("TEXT");
    }
}
