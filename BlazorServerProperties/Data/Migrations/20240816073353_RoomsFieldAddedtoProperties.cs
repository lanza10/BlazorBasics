﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlazorServerProperties.Data.Migrations
{
    /// <inheritdoc />
    public partial class RoomsFieldAddedtoProperties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Rooms",
                table: "Properties",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rooms",
                table: "Properties");
        }
    }
}
