using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace PosterCMS.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Posters",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    Author = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EditDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Sub1 = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true),
                    Text1 = table.Column<string>(type: "character varying(870)", maxLength: 870, nullable: true),
                    Sub2 = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    Text2 = table.Column<string>(type: "character varying(560)", maxLength: 560, nullable: true),
                    Sub3 = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    Text3 = table.Column<string>(type: "character varying(560)", maxLength: 560, nullable: true),
                    ImageUrl1 = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    ImageUrl2 = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    ImageUrl3 = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posters", x => x.ID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Posters");
        }
    }
}
