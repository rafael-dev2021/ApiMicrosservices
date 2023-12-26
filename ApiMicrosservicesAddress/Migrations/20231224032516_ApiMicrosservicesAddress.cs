using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiMicrosservicesAddress.Migrations
{
    /// <inheritdoc />
    public partial class ApiMicrosservicesAddress : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AddressDtos",
                columns: table => new
                {
                    Cep = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    Logradouro = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Complemento = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Bairro = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Localidade = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Uf = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ibge = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ddd = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Siafi = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AddressDtos", x => x.Cep);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AddressDtos");
        }
    }
}
