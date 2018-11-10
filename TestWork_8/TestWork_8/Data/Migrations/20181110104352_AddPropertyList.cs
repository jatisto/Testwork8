using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace TestWork_8.Data.Migrations
{
    public partial class AddPropertyList : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ThemsId",
                table: "Themses",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Themses_ThemsId",
                table: "Themses",
                column: "ThemsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Themses_Themses_ThemsId",
                table: "Themses",
                column: "ThemsId",
                principalTable: "Themses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Themses_Themses_ThemsId",
                table: "Themses");

            migrationBuilder.DropIndex(
                name: "IX_Themses_ThemsId",
                table: "Themses");

            migrationBuilder.DropColumn(
                name: "ThemsId",
                table: "Themses");
        }
    }
}
