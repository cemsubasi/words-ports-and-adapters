using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infra.src.main.Context.Migrations {
  /// <inheritdoc />
  public partial class AlterAccountEntityDeletedAt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DeletedAt",
                table: "Accounts",
                type: "datetime(6)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Accounts");
        }
    }
}
