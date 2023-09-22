using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Meow.Sample.Data.Oracle.Migrations
{
    /// <inheritdoc />
    public partial class _202309222353 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Sample",
                columns: table => new
                {
                    SampleId = table.Column<Guid>(type: "RAW(16)", nullable: false, comment: "样本标识"),
                    Name = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false, comment: "名称"),
                    CreationTime = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: true, comment: "创建时间"),
                    CreatorId = table.Column<Guid>(type: "RAW(16)", nullable: true, comment: "创建人标识"),
                    LastModificationTime = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: true, comment: "最后修改时间"),
                    LastModifierId = table.Column<Guid>(type: "RAW(16)", nullable: true, comment: "最后修改人标识"),
                    IsDeleted = table.Column<bool>(type: "NUMBER(1)", nullable: false, comment: "是否删除"),
                    TenantId = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true, comment: "租户标识"),
                    Version = table.Column<byte[]>(type: "RAW(2000)", nullable: true, comment: "版本号")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sample", x => x.SampleId);
                },
                comment: "样本");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Sample");
        }
    }
}
