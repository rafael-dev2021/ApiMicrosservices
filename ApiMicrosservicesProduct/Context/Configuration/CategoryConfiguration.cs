using ApiMicrosservicesProduct.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiMicrosservicesProduct.Context.Configuration;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name).HasMaxLength(100).IsRequired();
        builder.Property(x => x.Image).HasMaxLength(600).IsRequired();

        builder.HasData(
            new Category(1, "The Kerwin Frost Box", "https://s7d1.scene7.com/is/image/mcdonalds/nav_menu_kerwinfrost_160x160-1:menu-category?resmode=sharp2"),
            new Category(2, "McNuggets® and Meals", "https://s7d1.scene7.com/is/image/mcdonalds/nav_mcnuggetscombo_160x160:menu-category?resmode=sharp2"),
            new Category(3, "Sweets & Treats", "https://s7d1.scene7.com/is/image/mcdonalds/desserts_shakes_300x300:menu-category?resmode=sharp2"),
            new Category(4, "Breakfast", "https://s7d1.scene7.com/is/image/mcdonalds/breakfast_300x300:menu-category?resmode=sharp2"),
            new Category(5, "Fries® & Slides", "https://s7d1.scene7.com/is/image/mcdonalds/snacks_sides_300x300:menu-category?resmode=sharp2"),
            new Category(6, "Beverages", "https://s7d1.scene7.com/is/image/mcdonalds/drinks_300x300:menu-category?resmode=sharp2"),
            new Category(7, "Burgers", "https://s7d1.scene7.com/is/image/mcdonalds/burgers_300x300:menu-category?resmode=sharp2"),
            new Category(8, "Happy Meal®", "https://s7d1.scene7.com/is/image/mcdonalds/nav_happy_meal_160x160:menu-category?resmode=sharp2"),
            new Category(9, "$1 $2 $3 Dollar Menu*", "https://s7d1.scene7.com/is/image/mcdonalds/D123_160x160:menu-category?resmode=sharp2"),
            new Category(10, "Chicken & Fish Sandwiches", "https://s7d1.scene7.com/is/image/mcdonalds/nav_chickenfishsandwiches_160x160:menu-category?resmode=sharp2"),
            new Category(11, "McCafé® Coffees", "https://s7d1.scene7.com/is/image/mcdonalds/mccafe_300x300:menu-category?resmode=sharp2")
            );
    }
}
