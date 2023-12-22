using ApiMicrosservicesProduct.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiMicrosservicesProduct.Context.Configuration
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).HasMaxLength(100).IsRequired();
            builder.Property(x => x.Description).HasMaxLength(10000).IsRequired();
            builder.Property(x => x.Price).HasPrecision(18, 2).IsRequired();
            builder.Property(x => x.Stock).IsRequired();
            builder.HasOne(x => x.Category).WithMany(x => x.Products).HasForeignKey(x => x.CategoryId);

            var products = new[]
            {
                new Product(
                    1,
                    "The Kerwin Frost Box 10 piece Chicken McNuggets® Meal",
                    ["https://s7d1.scene7.com/is/image/mcdonalds/Header_Menu_KerwinFrost_10Nuggetsv3_832x472:product-header-desktop?wid=830&hei=456&dpr=off"],
                    "The McDonald\'s 10 piece Chicken McNuggets® Combo Meal you love, now inside designer packaging, plus a McNugget Buddy collectible outfitted by Kerwin Frost. Featuring 10 tender and delicious Chicken McNuggets® made with all white meat chicken—plus our World Famous Fries® and your choice of a medium McDonald’s drink, There are 940 calories in a Chicken McNuggets® Combo Meal with a medium Coca-Cola® and medium Fries. Sauces each add 30-110 calories.",
                    69.99m,
                    50,
                    1
                ),
                 new Product(
                    2,
                    "Bacon, Egg & Cheese Biscuit",
                    ["https://s7d1.scene7.com/is/image/mcdonalds/DC_202211_0085_BaconEggCheeseBiscuit_832x472:product-header-desktop?wid=830&hei=458&dpr=off","https://s7d1.scene7.com/is/image/mcdonalds/biscuit_ingredient","https://s7d1.scene7.com/is/image/mcdonalds/folded_egg","https://s7d1.scene7.com/is/image/mcdonalds/ingredient_american_cheese_180x180","https://s7d1.scene7.com/is/image/mcdonalds/applewood_bacon","https://s7d1.scene7.com/is/image/mcdonalds/butter_salted","https://s7d1.scene7.com/is/image/mcdonalds/clarified_butter"],
                    "The McDonald\'s Bacon, Egg & Cheese Biscuit breakfast sandwich features a warm, buttermilk biscuit brushed with real butter, thick cut Applewood smoked bacon, a fluffy folded egg, and a slice of melty American cheese. There are 460 calories in a Bacon, Egg & Cheese Biscuit at McDonald\'s. Try one today with a Premium Roast Coffee and order with Mobile Order & Pay on the McDonald\'s App!",
                    15.99m,
                    75,
                    2
                ),
                  new Product(
                    3,
                    "Big Mac®",
                    ["https://s7d1.scene7.com/is/image/mcdonalds/Header_BigMac_832x472:product-header-desktop?wid=830&hei=456&dpr=off","https://s7d1.scene7.com/is/image/mcdonalds/big_mac_bun","https://s7d1.scene7.com/is/image/mcdonalds/10_1_patty","https://s7d1.scene7.com/is/image/mcdonalds/shredded_lettuce","https://s7d1.scene7.com/is/image/mcdonalds/BigMacSauce_180x180","https://s7d1.scene7.com/is/image/mcdonalds/ingredient_american_cheese_180x180","https://s7d1.scene7.com/is/image/mcdonalds/pickles","https://s7d1.scene7.com/is/image/mcdonalds/reconstituted_onions"],
                    "Ever wondered what\'s on a Big Mac®? The McDonald\'s Big Mac® is a 100% beef burger with a taste like no other. The mouthwatering perfection starts with two 100% pure all beef patties and Big Mac® sauce sandwiched between a sesame seed bun. It’s topped off with pickles, crisp shredded lettuce, finely chopped onion, and a slice of American cheese. It contains no artificial flavors, preservatives, or added colors from artificial sources. Our pickle contains an artificial preservative, so skip it if you like.",
                    5.99m,
                    85,
                    3
                )
            };

            builder.HasData(products);
        }
    }
}