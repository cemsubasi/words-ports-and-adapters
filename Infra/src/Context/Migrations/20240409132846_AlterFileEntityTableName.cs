using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infra.src.main.Context.Migrations
{
    /// <inheritdoc />
    public partial class AlterFileEntityTableName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FileEntity_Accounts_AccountId",
                table: "FileEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FileEntity",
                table: "FileEntity");

            migrationBuilder.RenameTable(
                name: "FileEntity",
                newName: "Files");

            migrationBuilder.RenameIndex(
                name: "IX_FileEntity_AccountId",
                table: "Files",
                newName: "IX_Files_AccountId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Files",
                table: "Files",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Files_Accounts_AccountId",
                table: "Files",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Files_Accounts_AccountId",
                table: "Files");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Files",
                table: "Files");

            migrationBuilder.RenameTable(
                name: "Files",
                newName: "FileEntity");

            migrationBuilder.RenameIndex(
                name: "IX_Files_AccountId",
                table: "FileEntity",
                newName: "IX_FileEntity_AccountId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FileEntity",
                table: "FileEntity",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FileEntity_Accounts_AccountId",
                table: "FileEntity",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
