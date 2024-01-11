using ApiMicrosservicesProduct.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiMicrosservicesProduct.Context.Configuration;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name).HasMaxLength(100).IsRequired();
        builder.Property(x => x.Images).HasMaxLength(600).IsRequired();
        builder.Property(x => x.Stock).IsRequired();
        builder.Property(x => x.Price).HasPrecision(10, 2).IsRequired();

        var products = new[]
        {
            new Product(
                1,
                "Big Mac®",
                "Ever wondered what\'s on a Big Mac®? The McDonald\'s Big Mac® is a 100% beef burger with a taste like no other. The mouthwatering perfection starts with two 100% pure all beef patties and Big Mac® sauce sandwiched between a sesame seed bun. It’s topped off with pickles, crisp shredded lettuce, finely chopped onion, and a slice of American cheese. It contains no artificial flavors, preservatives, or added colors from artificial sources. Our pickle contains an artificial preservative, so skip it if you like.",
                5,
                15.99m,
                ["https://s7d1.scene7.com/is/image/mcdonalds/Header_BigMac_832x472:product-header-desktop?wid=830&hei=456&dpr=off",
                 "https://s7d1.scene7.com/is/image/mcdonalds/big_mac_bun",
                 "https://s7d1.scene7.com/is/image/mcdonalds/10_1_patty",
                 "https://s7d1.scene7.com/is/image/mcdonalds/shredded_lettuce",
                 "https://s7d1.scene7.com/is/image/mcdonalds/BigMacSauce_180x180",
                 "https://s7d1.scene7.com/is/image/mcdonalds/ingredient_american_cheese_180x180",
                 "https://s7d1.scene7.com/is/image/mcdonalds/pickles",
                 "https://s7d1.scene7.com/is/image/mcdonalds/reconstituted_onions"],
                  1
                ),
               new Product(
                2,
                "10 Piece Chicken McNuggets®",
                "Our tender, juicy Chicken McNuggets® are made with all white meat chicken and no artificial colors, flavors or preservatives. There are 410 calories in a 10 piece Chicken McNuggets®. Pair them with your favorite dipping sauces when you Mobile Order & Pay!*",
                15,
                25.99m,
                ["https://s7d1.scene7.com/is/image/mcdonalds/DC_202208_5280_10McNuggets_Stacked_832x472:product-header-desktop?wid=830&hei=458&dpr=off"],
                  2
                ),
                  new Product(
                3,
                "McFlurry® with OREO® Cookies",
                "The McDonald’s McFlurry® with OREO® Cookies is a popular combination of creamy vanilla soft serve with crunchy pieces of OREO® cookies!  There are 510 calories in a regular size OREO® McFlurry® at McDonald\'s. Check out all the McFlurry® flavors on the McDonald\'s Desserts & Shakes menu. Order a McFlurry® with OREO® Cookies using Mobile Order & Pay* in the McDonald’s app.",
                2,
                5.99m,
                ["https://s7d1.scene7.com/is/image/mcdonalds/DC_202002_3832_OREOMcFlurry_832x472:product-header-desktop?wid=830&hei=458&dpr=off"],
                  3
                )
        };

        builder.HasData(products);
    }
}
