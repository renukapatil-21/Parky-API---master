﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Parky_API.Migrations
{
    public partial class AddImageToNationalPark : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Picture",
                table: "NationalParks",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Picture",
                table: "NationalParks");
        }
    }
}
