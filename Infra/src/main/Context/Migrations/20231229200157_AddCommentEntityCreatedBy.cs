using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infra.src.main.Context.Migrations
{
    /// <inheritdoc />
    public partial class AddCommentEntityCreatedBy : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Comments_CommentId",
                table: "Comments");

            migrationBuilder.RenameColumn(
                name: "Value",
                table: "Comments",
                newName: "CreatedBy");

            migrationBuilder.RenameColumn(
                name: "CommentId",
                table: "Comments",
                newName: "ParentCommentId");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_CommentId",
                table: "Comments",
                newName: "IX_Comments_ParentCommentId");

            migrationBuilder.RenameColumn(
                name: "Value",
                table: "Categories",
                newName: "Category");

            migrationBuilder.AddColumn<string>(
                name: "Comment",
                table: "Comments",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "CreatedAt",
                table: "Comments",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Comments_ParentCommentId",
                table: "Comments",
                column: "ParentCommentId",
                principalTable: "Comments",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Comments_ParentCommentId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "Comment",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Comments");

            migrationBuilder.RenameColumn(
                name: "ParentCommentId",
                table: "Comments",
                newName: "CommentId");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "Comments",
                newName: "Value");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_ParentCommentId",
                table: "Comments",
                newName: "IX_Comments_CommentId");

            migrationBuilder.RenameColumn(
                name: "Category",
                table: "Categories",
                newName: "Value");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Comments_CommentId",
                table: "Comments",
                column: "CommentId",
                principalTable: "Comments",
                principalColumn: "Id");
        }
    }
}
