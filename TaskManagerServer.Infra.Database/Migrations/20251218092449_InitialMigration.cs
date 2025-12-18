using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TaskManagerServer.Infra.Database.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TaskTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false, comment: "Идентификатор записи")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false, comment: "Название типа"),
                    CreatedBy = table.Column<int>(type: "integer", nullable: false, comment: "Id пользователя создавшего запись"),
                    UpdatedBy = table.Column<int>(type: "integer", nullable: false, comment: "Id пользователя внесшего последние изменения"),
                    Created = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, comment: "Время создания записи"),
                    Updated = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, comment: "Время последнего изменения записи"),
                    Deleted = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true, comment: "Дата и время удаления записи")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TaskEntities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false, comment: "Идентификатор записи")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    TaskTypeId = table.Column<int>(type: "integer", nullable: false, comment: "FK на таблицу TaskTypes"),
                    Status = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false, defaultValue: "InProcess", comment: "Статус операции"),
                    CreatedBy = table.Column<int>(type: "integer", nullable: false, comment: "Id пользователя создавшего запись"),
                    UpdatedBy = table.Column<int>(type: "integer", nullable: false, comment: "Id пользователя внесшего последние изменения"),
                    Created = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, comment: "Время создания записи"),
                    Updated = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, comment: "Время последнего изменения записи"),
                    Deleted = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true, comment: "Дата и время удаления записи")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskEntities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaskEntities_TaskTypes_TaskTypeId",
                        column: x => x.TaskTypeId,
                        principalTable: "TaskTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "TaskTypes",
                columns: new[] { "Id", "Created", "CreatedBy", "Deleted", "Name", "Updated", "UpdatedBy" },
                values: new object[,]
                {
                    { 1, new DateTimeOffset(new DateTime(2025, 12, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)), -1, null, "Авто", new DateTimeOffset(new DateTime(2025, 12, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)), -1 },
                    { 2, new DateTimeOffset(new DateTime(2025, 12, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)), -1, null, "Авиа", new DateTimeOffset(new DateTime(2025, 12, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)), -1 },
                    { 3, new DateTimeOffset(new DateTime(2025, 12, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)), -1, null, "ЖД", new DateTimeOffset(new DateTime(2025, 12, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)), -1 },
                    { 4, new DateTimeOffset(new DateTime(2025, 12, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)), -1, null, "ТХ", new DateTimeOffset(new DateTime(2025, 12, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)), -1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_TaskEntities_TaskTypeId",
                table: "TaskEntities",
                column: "TaskTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TaskEntities");

            migrationBuilder.DropTable(
                name: "TaskTypes");
        }
    }
}
