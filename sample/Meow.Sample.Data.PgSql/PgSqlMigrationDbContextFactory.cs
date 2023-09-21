using Meow.Data.EntityFrameworkCore;

namespace Meow.Sample.Data.PgSql;

/// <summary>
/// 迁移设计时数据上下文工厂
/// </summary>
public class PgSqlMigrationDbContextFactory : PgSqlMigrationDbContextFactoryBase<SampleUnitOfWork> {
    /// <summary>
    /// 获取连接字符串
    /// </summary>
    public override string GetConnectionStringName() {
        return "PgSqlConnection";
    }
}
