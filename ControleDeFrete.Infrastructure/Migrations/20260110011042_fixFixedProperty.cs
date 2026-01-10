using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ControleDeFrete.API.Migrations
{
    /// <inheritdoc />
    public partial class fixFixedProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Documento",
                table: "Motoristas",
                type: "nvarchar(14)",
                maxLength: 14,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nchar(14)",
                oldFixedLength: true,
                oldMaxLength: 14);

            migrationBuilder.AlterColumn<string>(
                name: "Documento",
                table: "Clientes",
                type: "nvarchar(14)",
                maxLength: 14,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nchar(14)",
                oldFixedLength: true,
                oldMaxLength: 14);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Documento",
                table: "Motoristas",
                type: "nchar(14)",
                fixedLength: true,
                maxLength: 14,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(14)",
                oldMaxLength: 14);

            migrationBuilder.AlterColumn<string>(
                name: "Documento",
                table: "Clientes",
                type: "nchar(14)",
                fixedLength: true,
                maxLength: 14,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(14)",
                oldMaxLength: 14);
        }
    }
}
