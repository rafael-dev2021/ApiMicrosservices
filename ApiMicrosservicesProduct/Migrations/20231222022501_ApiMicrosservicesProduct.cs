using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ApiMicrosservicesProduct.Migrations
{
    /// <inheritdoc />
    public partial class ApiMicrosservicesProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Image = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Images = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", maxLength: 10000, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Stock = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Image", "Name" },
                values: new object[,]
                {
                    { 1, "https://s7d1.scene7.com/is/image/mcdonalds/nav_menu_kerwinfrost_160x160-1:menu-category?resmode=sharp2", "The Kerwin Frost Box" },
                    { 2, "https://s7d1.scene7.com/is/image/mcdonalds/nav_mcnuggetscombo_160x160:menu-category?resmode=sharp2", "McNuggets® and Meals" },
                    { 3, "https://s7d1.scene7.com/is/image/mcdonalds/desserts_shakes_300x300:menu-category?resmode=sharp2", "Sweets & Treats" },
                    { 4, "https://s7d1.scene7.com/is/image/mcdonalds/breakfast_300x300:menu-category?resmode=sharp2", "Breakfast" },
                    { 5, "https://s7d1.scene7.com/is/image/mcdonalds/snacks_sides_300x300:menu-category?resmode=sharp2", "Fries® & Slides" },
                    { 6, "https://s7d1.scene7.com/is/image/mcdonalds/drinks_300x300:menu-category?resmode=sharp2", "Beverages" },
                    { 7, "https://s7d1.scene7.com/is/image/mcdonalds/burgers_300x300:menu-category?resmode=sharp2", "Burgers" },
                    { 8, "https://s7d1.scene7.com/is/image/mcdonalds/nav_happy_meal_160x160:menu-category?resmode=sharp2", "Happy Meal®" },
                    { 9, "https://s7d1.scene7.com/is/image/mcdonalds/D123_160x160:menu-category?resmode=sharp2", "$1 $2 $3 Dollar Menu*" },
                    { 10, "https://s7d1.scene7.com/is/image/mcdonalds/nav_chickenfishsandwiches_160x160:menu-category?resmode=sharp2", "Chicken & Fish Sandwiches" },
                    { 11, "https://s7d1.scene7.com/is/image/mcdonalds/mccafe_300x300:menu-category?resmode=sharp2", "McCafé® Coffees" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "Description", "Images", "Name", "Price", "Stock" },
                values: new object[,]
                {
                    { 1, 1, "The McDonald's 10 piece Chicken McNuggets® Combo Meal you love, now inside designer packaging, plus a McNugget Buddy collectible outfitted by Kerwin Frost. Featuring 10 tender and delicious Chicken McNuggets® made with all white meat chicken—plus our World Famous Fries® and your choice of a medium McDonald’s drink, There are 940 calories in a Chicken McNuggets® Combo Meal with a medium Coca-Cola® and medium Fries. Sauces each add 30-110 calories.", "[\"https://s7d1.scene7.com/is/image/mcdonalds/Header_Menu_KerwinFrost_10Nuggetsv3_832x472:product-header-desktop?wid=830\\u0026hei=456\\u0026dpr=off\"]", "The Kerwin Frost Box 10 piece Chicken McNuggets® Meal", 69.99m, 50 },
                    { 2, 2, "The McDonald's Bacon, Egg & Cheese Biscuit breakfast sandwich features a warm, buttermilk biscuit brushed with real butter, thick cut Applewood smoked bacon, a fluffy folded egg, and a slice of melty American cheese. There are 460 calories in a Bacon, Egg & Cheese Biscuit at McDonald's. Try one today with a Premium Roast Coffee and order with Mobile Order & Pay on the McDonald's App!", "[\"https://s7d1.scene7.com/is/image/mcdonalds/DC_202211_0085_BaconEggCheeseBiscuit_832x472:product-header-desktop?wid=830\\u0026hei=458\\u0026dpr=off\",\"https://s7d1.scene7.com/is/image/mcdonalds/biscuit_ingredient\",\"https://s7d1.scene7.com/is/image/mcdonalds/folded_egg\",\"https://s7d1.scene7.com/is/image/mcdonalds/ingredient_american_cheese_180x180\",\"https://s7d1.scene7.com/is/image/mcdonalds/applewood_bacon\",\"https://s7d1.scene7.com/is/image/mcdonalds/butter_salted\",\"https://s7d1.scene7.com/is/image/mcdonalds/clarified_butter\"]", "Bacon, Egg & Cheese Biscuit", 15.99m, 75 },
                    { 3, 3, "Ever wondered what's on a Big Mac®? The McDonald's Big Mac® is a 100% beef burger with a taste like no other. The mouthwatering perfection starts with two 100% pure all beef patties and Big Mac® sauce sandwiched between a sesame seed bun. It’s topped off with pickles, crisp shredded lettuce, finely chopped onion, and a slice of American cheese. It contains no artificial flavors, preservatives, or added colors from artificial sources. Our pickle contains an artificial preservative, so skip it if you like.", "[\"https://s7d1.scene7.com/is/image/mcdonalds/Header_BigMac_832x472:product-header-desktop?wid=830\\u0026hei=456\\u0026dpr=off\",\"https://s7d1.scene7.com/is/image/mcdonalds/big_mac_bun\",\"https://s7d1.scene7.com/is/image/mcdonalds/10_1_patty\",\"https://s7d1.scene7.com/is/image/mcdonalds/shredded_lettuce\",\"https://s7d1.scene7.com/is/image/mcdonalds/BigMacSauce_180x180\",\"https://s7d1.scene7.com/is/image/mcdonalds/ingredient_american_cheese_180x180\",\"https://s7d1.scene7.com/is/image/mcdonalds/pickles\",\"https://s7d1.scene7.com/is/image/mcdonalds/reconstituted_onions\"]", "Big Mac®", 5.99m, 85 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
