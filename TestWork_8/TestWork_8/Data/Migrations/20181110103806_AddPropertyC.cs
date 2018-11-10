using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace TestWork_8.Data.Migrations
{
    public partial class AddPropertyC : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Themses_ThemsId1",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_ThemsId1",
                table: "Comments");

            migrationBuilder.RenameColumn(
                name: "ThemsId1",
                table: "Comments",
                newName: "NameThemsComment");

            migrationBuilder.AlterColumn<string>(
                name: "ThemsId",
                table: "Comments",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<string>(
                name: "NameThemsComment",
                table: "Comments",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Comments_ThemsId",
                table: "Comments",
                column: "ThemsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Themses_ThemsId",
                table: "Comments",
                column: "ThemsId",
                principalTable: "Themses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Themses_ThemsId",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_ThemsId",
                table: "Comments");

            migrationBuilder.RenameColumn(
                name: "NameThemsComment",
                table: "Comments",
                newName: "ThemsId1");

            migrationBuilder.AlterColumn<int>(
                name: "ThemsId",
                table: "Comments",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ThemsId1",
                table: "Comments",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Comments_ThemsId1",
                table: "Comments",
                column: "ThemsId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Themses_ThemsId1",
                table: "Comments",
                column: "ThemsId1",
                principalTable: "Themses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
