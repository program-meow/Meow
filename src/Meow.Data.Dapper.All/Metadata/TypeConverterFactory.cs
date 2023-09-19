using Meow.Data.Metadata;
using Meow.Database;

namespace Meow.Data.Dapper.Metadata;

/// <summary>
/// 数据类型转换器工厂
/// </summary>
public class TypeConverterFactory : ITypeConverterFactory {
    /// <summary>
    /// 创建数据库元数据服务
    /// </summary>
    /// <param name="dbType">数据库类型</param>
    public ITypeConverter Create( DatabaseEnum dbType ) {
        switch( dbType ) {
            case DatabaseEnum.SqlServer:
                return new SqlServerTypeConverter();
            case DatabaseEnum.PgSql:
                return new PostgreSqlTypeConverter();
            case DatabaseEnum.MySql:
                return new MySqlTypeConverter();
        }
        throw new NotImplementedException();
    }
}