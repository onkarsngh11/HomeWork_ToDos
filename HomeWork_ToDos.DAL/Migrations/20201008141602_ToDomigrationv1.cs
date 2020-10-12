using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HomeWork_ToDos.DAL.Migrations
{
    public partial class ToDomigrationv1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    UserName = table.Column<string>(nullable: true),
                    UserRole = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Labels",
                columns: table => new
                {
                    LabelId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(nullable: true),
                    CreatedBy = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Labels", x => x.LabelId);
                    table.ForeignKey(
                        name: "FK_Labels_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ToDoLists",
                columns: table => new
                {
                    ToDoListId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(nullable: true),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    UpdationDate = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ToDoLists", x => x.ToDoListId);
                    table.ForeignKey(
                        name: "FK_ToDoLists_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MapLabelsToLists",
                columns: table => new
                {
                    ListMappingId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LabelId = table.Column<long>(nullable: false),
                    ToDoListId = table.Column<long>(nullable: false),
                    CreatedBy = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MapLabelsToLists", x => x.ListMappingId);
                    table.ForeignKey(
                        name: "FK_MapLabelsToLists_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MapLabelsToLists_Labels_LabelId",
                        column: x => x.LabelId,
                        principalTable: "Labels",
                        principalColumn: "LabelId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MapLabelsToLists_ToDoLists_ToDoListId",
                        column: x => x.ToDoListId,
                        principalTable: "ToDoLists",
                        principalColumn: "ToDoListId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ToDoItems",
                columns: table => new
                {
                    ToDoItemId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Notes = table.Column<string>(nullable: true),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    UpdationDate = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<long>(nullable: false),
                    ToDoListId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ToDoItems", x => x.ToDoItemId);
                    table.ForeignKey(
                        name: "FK_ToDoItems_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ToDoItems_ToDoLists_ToDoListId",
                        column: x => x.ToDoListId,
                        principalTable: "ToDoLists",
                        principalColumn: "ToDoListId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MapLabelsToItems",
                columns: table => new
                {
                    ItemMappingId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LabelId = table.Column<long>(nullable: false),
                    ToDoItemId = table.Column<long>(nullable: false),
                    CreatedBy = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MapLabelsToItems", x => x.ItemMappingId);
                    table.ForeignKey(
                        name: "FK_MapLabelsToItems_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MapLabelsToItems_Labels_LabelId",
                        column: x => x.LabelId,
                        principalTable: "Labels",
                        principalColumn: "LabelId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MapLabelsToItems_ToDoItems_ToDoItemId",
                        column: x => x.ToDoItemId,
                        principalTable: "ToDoItems",
                        principalColumn: "ToDoItemId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "FirstName", "LastName", "Password", "UserName", "UserRole" },
                values: new object[] { 1L, "Onkar", "Singh", "MTIz", "Onkar", "Admin" });

            migrationBuilder.InsertData(
                table: "Labels",
                columns: new[] { "LabelId", "CreatedBy", "Description" },
                values: new object[,]
                {
                    { 1L, 1L, "Review" },
                    { 2L, 1L, "Buy" },
                    { 3L, 1L, "Explore" },
                    { 4L, 1L, "Discover" }
                });

            migrationBuilder.InsertData(
                table: "ToDoLists",
                columns: new[] { "ToDoListId", "CreatedBy", "CreationDate", "Description", "UpdationDate" },
                values: new object[,]
                {
                    { 1L, 1L, new DateTime(2020, 10, 8, 19, 46, 1, 528, DateTimeKind.Local).AddTicks(3791), "List of phones to buy", null },
                    { 2L, 1L, new DateTime(2020, 10, 8, 19, 46, 1, 552, DateTimeKind.Local).AddTicks(5414), "List of phones to review", null },
                    { 3L, 1L, new DateTime(2020, 10, 8, 19, 46, 1, 552, DateTimeKind.Local).AddTicks(5454), "List of things to explore", null },
                    { 4L, 1L, new DateTime(2020, 10, 8, 19, 46, 1, 552, DateTimeKind.Local).AddTicks(5457), "List of places to travel", null },
                    { 5L, 1L, new DateTime(2020, 10, 8, 19, 46, 1, 552, DateTimeKind.Local).AddTicks(5459), "List of games", null }
                });

            migrationBuilder.InsertData(
                table: "ToDoItems",
                columns: new[] { "ToDoItemId", "CreatedBy", "CreationDate", "Notes", "ToDoListId", "UpdationDate" },
                values: new object[,]
                {
                    { 1L, 1L, new DateTime(2020, 10, 8, 19, 46, 1, 562, DateTimeKind.Local).AddTicks(6686), "Buy IPhone 11", 1L, null },
                    { 2L, 1L, new DateTime(2020, 10, 8, 19, 46, 1, 562, DateTimeKind.Local).AddTicks(7112), "Buy Pixel 4a", 1L, null },
                    { 3L, 1L, new DateTime(2020, 10, 8, 19, 46, 1, 562, DateTimeKind.Local).AddTicks(7127), "Review Pixel 4a", 2L, null },
                    { 4L, 1L, new DateTime(2020, 10, 8, 19, 46, 1, 562, DateTimeKind.Local).AddTicks(7129), "Review IPhone 11", 2L, null },
                    { 5L, 1L, new DateTime(2020, 10, 8, 19, 46, 1, 562, DateTimeKind.Local).AddTicks(7132), "Explore animal kingdom", 3L, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Labels_CreatedBy",
                table: "Labels",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_MapLabelsToItems_CreatedBy",
                table: "MapLabelsToItems",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_MapLabelsToItems_LabelId",
                table: "MapLabelsToItems",
                column: "LabelId");

            migrationBuilder.CreateIndex(
                name: "IX_MapLabelsToItems_ToDoItemId",
                table: "MapLabelsToItems",
                column: "ToDoItemId");

            migrationBuilder.CreateIndex(
                name: "IX_MapLabelsToLists_CreatedBy",
                table: "MapLabelsToLists",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_MapLabelsToLists_LabelId",
                table: "MapLabelsToLists",
                column: "LabelId");

            migrationBuilder.CreateIndex(
                name: "IX_MapLabelsToLists_ToDoListId",
                table: "MapLabelsToLists",
                column: "ToDoListId");

            migrationBuilder.CreateIndex(
                name: "IX_ToDoItems_CreatedBy",
                table: "ToDoItems",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ToDoItems_ToDoListId",
                table: "ToDoItems",
                column: "ToDoListId");

            migrationBuilder.CreateIndex(
                name: "IX_ToDoLists_CreatedBy",
                table: "ToDoLists",
                column: "CreatedBy");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MapLabelsToItems");

            migrationBuilder.DropTable(
                name: "MapLabelsToLists");

            migrationBuilder.DropTable(
                name: "ToDoItems");

            migrationBuilder.DropTable(
                name: "Labels");

            migrationBuilder.DropTable(
                name: "ToDoLists");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
