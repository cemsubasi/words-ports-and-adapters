using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infra.src.main.Context.Migrations {
  /// <inheritdoc />
  public partial class AlterCommentEntityCreatedBy : Migration {
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder) {
      /* migrationBuilder.AlterColumn<Guid>( */
      /*     name: "CreatedBy", */
      /*     table: "Comments", */
      /*     type: "char(36)", */
      /*     nullable: true, */
      /*     collation: "ascii_general_ci", */
      /*     oldClrType: typeof(string), */
      /*     oldType: "longtext", */
      /*     oldNullable: true) */
      /*     .OldAnnotation("MySql:CharSet", "utf8mb4"); */
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder) {
      migrationBuilder.AlterColumn<string>(
          name: "CreatedBy",
          table: "Comments",
          type: "longtext",
          nullable: true,
          oldClrType: typeof(Guid),
          oldType: "char(36)",
          oldNullable: true)
          .Annotation("MySql:CharSet", "utf8mb4")
          .OldAnnotation("Relational:Collation", "ascii_general_ci");
    }
  }
}
