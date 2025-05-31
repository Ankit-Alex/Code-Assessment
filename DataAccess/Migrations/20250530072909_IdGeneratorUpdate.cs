using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class IdGeneratorUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Create sequence that starts from 1 and increments by 1
            migrationBuilder.Sql(
                "CREATE SEQUENCE ProductIdSequence " +
                "START WITH 1 " +
                "INCREMENT BY 1 " +
                "MINVALUE 1 " +
                "MAXVALUE 99999 " +  // 5 digits max
                "CYCLE;");  // Will restart from 1 when reaching max value
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP SEQUENCE ProductIdSequence;");
        }
    }
}
