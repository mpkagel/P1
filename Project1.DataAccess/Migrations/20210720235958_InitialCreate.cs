using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Project1.DataAccess.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Project1");

            migrationBuilder.CreateTable(
                name: "Cupcake",
                schema: "Project1",
                columns: table => new
                {
                    CupcakeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Cost = table.Column<decimal>(type: "decimal(8,2)", nullable: false, defaultValueSql: "((6.00))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cupcake", x => x.CupcakeId);
                });

            migrationBuilder.CreateTable(
                name: "Ingredient",
                schema: "Project1",
                columns: table => new
                {
                    IngredientId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Units = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingredient", x => x.IngredientId);
                });

            migrationBuilder.CreateTable(
                name: "Location",
                schema: "Project1",
                columns: table => new
                {
                    LocationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Location", x => x.LocationId);
                });

            migrationBuilder.CreateTable(
                name: "RecipeItem",
                schema: "Project1",
                columns: table => new
                {
                    RecipeItemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CupcakeID = table.Column<int>(type: "int", nullable: false),
                    IngredientID = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(10,6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipeItem", x => x.RecipeItemId);
                    table.ForeignKey(
                        name: "FK_Recipe_Cupcake",
                        column: x => x.CupcakeID,
                        principalSchema: "Project1",
                        principalTable: "Cupcake",
                        principalColumn: "CupcakeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Recipe_Ingredient",
                        column: x => x.IngredientID,
                        principalSchema: "Project1",
                        principalTable: "Ingredient",
                        principalColumn: "IngredientId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Customer",
                schema: "Project1",
                columns: table => new
                {
                    CustomerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DefaultLocation = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.CustomerId);
                    table.ForeignKey(
                        name: "FK_Default_Location",
                        column: x => x.DefaultLocation,
                        principalSchema: "Project1",
                        principalTable: "Location",
                        principalColumn: "LocationId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LocationInventory",
                schema: "Project1",
                columns: table => new
                {
                    LocationInventoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LocationID = table.Column<int>(type: "int", nullable: false),
                    IngredientID = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(10,6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocationInventory", x => x.LocationInventoryId);
                    table.ForeignKey(
                        name: "FK_Ingredient",
                        column: x => x.IngredientID,
                        principalSchema: "Project1",
                        principalTable: "Ingredient",
                        principalColumn: "IngredientId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Location",
                        column: x => x.LocationID,
                        principalSchema: "Project1",
                        principalTable: "Location",
                        principalColumn: "LocationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CupcakeOrder",
                schema: "Project1",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LocationID = table.Column<int>(type: "int", nullable: false),
                    CustomerID = table.Column<int>(type: "int", nullable: false),
                    OrderTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__CupcakeO__C3905BCFC5A85BD9", x => x.OrderId);
                    table.ForeignKey(
                        name: "FK_Order_Customer",
                        column: x => x.CustomerID,
                        principalSchema: "Project1",
                        principalTable: "Customer",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Order_Location",
                        column: x => x.LocationID,
                        principalSchema: "Project1",
                        principalTable: "Location",
                        principalColumn: "LocationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CupcakeOrderItem",
                schema: "Project1",
                columns: table => new
                {
                    CupcakeOrderItemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderID = table.Column<int>(type: "int", nullable: false),
                    CupcakeID = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CupcakeOrderItem", x => x.CupcakeOrderItemId);
                    table.ForeignKey(
                        name: "FK_OrderItem_Cupcake",
                        column: x => x.CupcakeID,
                        principalSchema: "Project1",
                        principalTable: "Cupcake",
                        principalColumn: "CupcakeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderItem_CupcakeOrder",
                        column: x => x.OrderID,
                        principalSchema: "Project1",
                        principalTable: "CupcakeOrder",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "UQ__Cupcake__F9B8A48BBF33A5C9",
                schema: "Project1",
                table: "Cupcake",
                column: "Type",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CupcakeOrder_CustomerID",
                schema: "Project1",
                table: "CupcakeOrder",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_CupcakeOrder_LocationID",
                schema: "Project1",
                table: "CupcakeOrder",
                column: "LocationID");

            migrationBuilder.CreateIndex(
                name: "IX_CupcakeOrderItem_CupcakeID",
                schema: "Project1",
                table: "CupcakeOrderItem",
                column: "CupcakeID");

            migrationBuilder.CreateIndex(
                name: "OrderToCupcake",
                schema: "Project1",
                table: "CupcakeOrderItem",
                columns: new[] { "OrderID", "CupcakeID" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Customer_DefaultLocation",
                schema: "Project1",
                table: "Customer",
                column: "DefaultLocation");

            migrationBuilder.CreateIndex(
                name: "UQ__Ingredie__F9B8A48BFB6A0D37",
                schema: "Project1",
                table: "Ingredient",
                column: "Type",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "InventoryIngredient",
                schema: "Project1",
                table: "LocationInventory",
                columns: new[] { "LocationID", "IngredientID" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LocationInventory_IngredientID",
                schema: "Project1",
                table: "LocationInventory",
                column: "IngredientID");

            migrationBuilder.CreateIndex(
                name: "CupcakeIngredient",
                schema: "Project1",
                table: "RecipeItem",
                columns: new[] { "CupcakeID", "IngredientID" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RecipeItem_IngredientID",
                schema: "Project1",
                table: "RecipeItem",
                column: "IngredientID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CupcakeOrderItem",
                schema: "Project1");

            migrationBuilder.DropTable(
                name: "LocationInventory",
                schema: "Project1");

            migrationBuilder.DropTable(
                name: "RecipeItem",
                schema: "Project1");

            migrationBuilder.DropTable(
                name: "CupcakeOrder",
                schema: "Project1");

            migrationBuilder.DropTable(
                name: "Cupcake",
                schema: "Project1");

            migrationBuilder.DropTable(
                name: "Ingredient",
                schema: "Project1");

            migrationBuilder.DropTable(
                name: "Customer",
                schema: "Project1");

            migrationBuilder.DropTable(
                name: "Location",
                schema: "Project1");
        }
    }
}
