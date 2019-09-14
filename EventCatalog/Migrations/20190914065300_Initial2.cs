using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EventCatalog.Migrations
{
    public partial class Initial2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameSequence(
                name: "Event_Types_hilo",
                newName: "event_types_hilo");

            migrationBuilder.CreateSequence(
                name: "event_category_hilo",
                incrementBy: 10);

            migrationBuilder.CreateSequence(
                name: "event_location_hilo",
                incrementBy: 10);

            migrationBuilder.CreateSequence(
                name: "events_hilo",
                incrementBy: 10);

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "EventTypes",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "EventCategories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EventLocations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    City = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventLocations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Catalog",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    Address = table.Column<string>(maxLength: 250, nullable: false),
                    Description = table.Column<string>(maxLength: 300, nullable: false),
                    StartDateTime = table.Column<DateTime>(nullable: false),
                    EndDateTime = table.Column<DateTime>(nullable: false),
                    EventImageUrl = table.Column<string>(nullable: true),
                    OrganizerName = table.Column<string>(nullable: false),
                    OrganizerDescription = table.Column<string>(maxLength: 150, nullable: false),
                    EventCategoryId = table.Column<int>(nullable: false),
                    EventTypeId = table.Column<int>(nullable: false),
                    EventLocationId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Catalog", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Catalog_EventCategories_EventCategoryId",
                        column: x => x.EventCategoryId,
                        principalTable: "EventCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Catalog_EventLocations_EventLocationId",
                        column: x => x.EventLocationId,
                        principalTable: "EventLocations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Catalog_EventTypes_EventTypeId",
                        column: x => x.EventTypeId,
                        principalTable: "EventTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Catalog_EventCategoryId",
                table: "Catalog",
                column: "EventCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Catalog_EventLocationId",
                table: "Catalog",
                column: "EventLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Catalog_EventTypeId",
                table: "Catalog",
                column: "EventTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Catalog");

            migrationBuilder.DropTable(
                name: "EventCategories");

            migrationBuilder.DropTable(
                name: "EventLocations");

            migrationBuilder.DropSequence(
                name: "event_category_hilo");

            migrationBuilder.DropSequence(
                name: "event_location_hilo");

            migrationBuilder.DropSequence(
                name: "events_hilo");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "EventTypes");

            migrationBuilder.RenameSequence(
                name: "event_types_hilo",
                newName: "Event_Types_hilo");
        }
    }
}
