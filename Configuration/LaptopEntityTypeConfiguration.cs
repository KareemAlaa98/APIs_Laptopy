using Laptopy_APIs.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Laptopy_APIs.Configuration
{
    public class LaptopEntityTypeConfiguration : IEntityTypeConfiguration<Laptop>
    {
        public void Configure(EntityTypeBuilder<Laptop> builder)
        {
            builder.Property(e => e.Name).IsRequired();
            builder.Property(e => e.Price).IsRequired();
            builder.Property(e => e.DiscountPercentage).HasDefaultValue(0);
            builder.Property(e => e.Rating).HasDefaultValue(0.0);

            builder.HasCheckConstraint("CHK_LaptopRating", "[Rating] >= 0 AND [Rating] <= 5");
            builder.HasCheckConstraint("CHK_Discount_lt_100", "[DiscountPercentage] <= 99");
        }
    }
}
