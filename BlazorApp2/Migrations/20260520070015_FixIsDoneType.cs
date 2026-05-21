using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BlazorApp2.Migrations
{
    /// <inheritdoc />
    public partial class FixIsDoneType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Todos",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.Sql(
                "ALTER TABLE \"Todos\" ALTER COLUMN \"IsDone\" TYPE boolean USING \"IsDone\"::boolean;"
            );

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Todos",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);
        }


        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Todos",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<int>(
                name: "IsDone",
                table: "Todos",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "boolean");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Todos",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);
        }
    }
}
