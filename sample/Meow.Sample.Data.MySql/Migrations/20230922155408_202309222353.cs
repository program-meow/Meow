using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Meow.Sample.Data.MySql.Migrations
{
    /// <inheritdoc />
    public partial class _202309222353 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Sample",
                columns: table => new
                {
                    SampleId = table.Column<Guid>(type: "char(36)", nullable: false, comment: "样本标识", collation: "ascii_general_ci"),
                    Name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, comment: "名称")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreationTime = table.Column<DateTime>(type: "datetime(6)", nullable: true, comment: "创建时间"),
                    CreatorId = table.Column<Guid>(type: "char(36)", nullable: true, comment: "创建人标识", collation: "ascii_general_ci"),
                    LastModificationTime = table.Column<DateTime>(type: "datetime(6)", nullable: true, comment: "最后修改时间"),
                    LastModifierId = table.Column<Guid>(type: "char(36)", nullable: true, comment: "最后修改人标识", collation: "ascii_general_ci"),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false, comment: "是否删除"),
                    TenantId = table.Column<string>(type: "longtext", nullable: true, comment: "租户标识")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Version = table.Column<byte[]>(type: "longblob", nullable: true, comment: "版本号")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sample", x => x.SampleId);
                },
                comment: "样本")
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Sample");
        }
    }
}
