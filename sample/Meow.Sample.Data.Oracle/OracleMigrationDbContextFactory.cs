using Meow.Data.EntityFrameworkCore;

namespace Meow.Sample.Data.Oracle;

/// <summary>
/// 迁移设计时数据上下文工厂
/// </summary>
public class OracleMigrationDbContextFactory : OracleMigrationDbContextFactoryBase<SampleUnitOfWork> {
    /// <summary>
    /// 获取连接字符串
    /// </summary>
    public override string GetConnectionStringName() {
        return "Oracle";
    }
}
