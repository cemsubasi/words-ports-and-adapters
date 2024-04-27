using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infra.src.main.Context.Migrations {
  /// <inheritdoc />
  public partial class AlterAccountEntityCreatedBy : Migration {
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder) {
      migrationBuilder.AddColumn<DateTimeOffset>(
          name: "CreatedAt",
          table: "Accounts",
          type: "datetime(6)",
          nullable: false,
          defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

      migrationBuilder.AddColumn<Guid>(
          name: "CreatedBy",
          table: "Accounts",
          type: "char(36)",
          nullable: true,
          collation: "ascii_general_ci");

      migrationBuilder.AddColumn<DateTimeOffset>(
          name: "UpdatedAt",
          table: "Accounts",
          type: "datetime(6)",
          nullable: false,
          defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

      migrationBuilder.CreateIndex(
          name: "IX_Comments_CreatedBy",
          table: "Comments",
          column: "CreatedBy");

      migrationBuilder.CreateIndex(
          name: "IX_Accounts_CreatedBy",
          table: "Accounts",
          column: "CreatedBy");

      migrationBuilder.AddForeignKey(
          name: "FK_Accounts_Accounts_CreatedBy",
          table: "Accounts",
          column: "CreatedBy",
          principalTable: "Accounts",
          principalColumn: "Id");

      migrationBuilder.AddForeignKey(
          name: "FK_Comments_Accounts_CreatedBy",
          table: "Comments",
          column: "CreatedBy",
          principalTable: "Accounts",
          principalColumn: "Id");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder) {
      migrationBuilder.DropForeignKey(
          name: "FK_Accounts_Accounts_CreatedBy",
          table: "Accounts");

      migrationBuilder.DropForeignKey(
          name: "FK_Comments_Accounts_CreatedBy",
          table: "Comments");

      migrationBuilder.DropIndex(
          name: "IX_Comments_CreatedBy",
          table: "Comments");

      migrationBuilder.DropIndex(
          name: "IX_Accounts_CreatedBy",
          table: "Accounts");

      migrationBuilder.DropColumn(
          name: "CreatedAt",
          table: "Accounts");

      migrationBuilder.DropColumn(
          name: "CreatedBy",
          table: "Accounts");

      migrationBuilder.DropColumn(
          name: "UpdatedAt",
          table: "Accounts");
    }
  }
}
