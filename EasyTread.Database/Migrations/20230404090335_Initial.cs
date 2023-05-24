using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EasyTread.Database.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CustomerDetails",
                columns: table => new
                {
                    CustomerDetailsId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LicensePlate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmailAdress = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerDetails", x => x.CustomerDetailsId);
                });

            migrationBuilder.CreateTable(
                name: "LicensePlate",
                columns: table => new
                {
                    LicensePlateId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Plate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PlateConfidence = table.Column<int>(type: "int", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CountryConfidence = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LicensePlate", x => x.LicensePlateId);
                });

            migrationBuilder.CreateTable(
                name: "ShoulderWear",
                columns: table => new
                {
                    ShoulderWearId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Info = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Value = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoulderWear", x => x.ShoulderWearId);
                });

            migrationBuilder.CreateTable(
                name: "WearPattern",
                columns: table => new
                {
                    WearPatternId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Info = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Value = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WearPattern", x => x.WearPatternId);
                });

            migrationBuilder.CreateTable(
                name: "Crossing",
                columns: table => new
                {
                    CrossingId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DealerNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Valid = table.Column<bool>(type: "bit", nullable: false),
                    VehicleRating = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReportLink = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CrossingDirection = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LicensePlateId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Crossing", x => x.CrossingId);
                    table.ForeignKey(
                        name: "FK_Crossing_LicensePlate_LicensePlateId",
                        column: x => x.LicensePlateId,
                        principalTable: "LicensePlate",
                        principalColumn: "LicensePlateId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PropertySet",
                columns: table => new
                {
                    PropertySetId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DeepGrove = table.Column<bool>(type: "bit", nullable: false),
                    WearPatternId = table.Column<int>(type: "int", nullable: false),
                    ShoulderWearId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertySet", x => x.PropertySetId);
                    table.ForeignKey(
                        name: "FK_PropertySet_ShoulderWear_ShoulderWearId",
                        column: x => x.ShoulderWearId,
                        principalTable: "ShoulderWear",
                        principalColumn: "ShoulderWearId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PropertySet_WearPattern_WearPatternId",
                        column: x => x.WearPatternId,
                        principalTable: "WearPattern",
                        principalColumn: "WearPatternId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tire",
                columns: table => new
                {
                    TireId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Valid = table.Column<bool>(type: "bit", nullable: false),
                    TirePosition = table.Column<int>(type: "int", nullable: false),
                    PropertySetId = table.Column<int>(type: "int", nullable: false),
                    CrossingId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tire", x => x.TireId);
                    table.ForeignKey(
                        name: "FK_Tire_Crossing_CrossingId",
                        column: x => x.CrossingId,
                        principalTable: "Crossing",
                        principalColumn: "CrossingId");
                    table.ForeignKey(
                        name: "FK_Tire_PropertySet_PropertySetId",
                        column: x => x.PropertySetId,
                        principalTable: "PropertySet",
                        principalColumn: "PropertySetId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Region",
                columns: table => new
                {
                    RegionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RegionPosition = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    VehicleRating = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TireId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Region", x => x.RegionId);
                    table.ForeignKey(
                        name: "FK_Region_Tire_TireId",
                        column: x => x.TireId,
                        principalTable: "Tire",
                        principalColumn: "TireId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Crossing_LicensePlateId",
                table: "Crossing",
                column: "LicensePlateId");

            migrationBuilder.CreateIndex(
                name: "IX_PropertySet_ShoulderWearId",
                table: "PropertySet",
                column: "ShoulderWearId");

            migrationBuilder.CreateIndex(
                name: "IX_PropertySet_WearPatternId",
                table: "PropertySet",
                column: "WearPatternId");

            migrationBuilder.CreateIndex(
                name: "IX_Region_TireId",
                table: "Region",
                column: "TireId");

            migrationBuilder.CreateIndex(
                name: "IX_Tire_CrossingId",
                table: "Tire",
                column: "CrossingId");

            migrationBuilder.CreateIndex(
                name: "IX_Tire_PropertySetId",
                table: "Tire",
                column: "PropertySetId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomerDetails");

            migrationBuilder.DropTable(
                name: "Region");

            migrationBuilder.DropTable(
                name: "Tire");

            migrationBuilder.DropTable(
                name: "Crossing");

            migrationBuilder.DropTable(
                name: "PropertySet");

            migrationBuilder.DropTable(
                name: "LicensePlate");

            migrationBuilder.DropTable(
                name: "ShoulderWear");

            migrationBuilder.DropTable(
                name: "WearPattern");
        }
    }
}
