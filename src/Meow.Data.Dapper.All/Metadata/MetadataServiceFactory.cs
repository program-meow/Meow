namespace Meow.Data.Dapper.Metadata;

/// <summary>
/// 数据库元数据服务工厂
/// </summary>
public class MetadataServiceFactory : IMetadataServiceFactory {
    /// <summary>
    /// 服务提供器
    /// </summary>
    private readonly IServiceProvider _serviceProvider;

    /// <summary>
    /// 初始化数据库元数据服务工厂
    /// </summary>
    /// <param name="serviceProvider">服务提供器</param>
    public MetadataServiceFactory( IServiceProvider serviceProvider ) {
        _serviceProvider = serviceProvider;
    }

    /// <summary>
    /// 创建数据库元数据服务
    /// </summary>
    /// <param name="dbType">数据库类型</param>
    /// <param name="connection">数据库连接字符串</param>
    public IMetadataService Create( DatabaseEnum dbType , string connection ) {
        switch( dbType ) {
            case DatabaseEnum.SqlServer:
                return CreateSqlServerMetadataService( connection );
            case DatabaseEnum.PostgreSql:
                return CreatePgSqlMetadataService( connection );
            case DatabaseEnum.MySql:
                return CreateMySqlMetadataService( connection );
        }
        throw new NotImplementedException();
    }

    /// <summary>
    /// 创建Sql Server数据库元数据服务
    /// </summary>
    private IMetadataService CreateSqlServerMetadataService( string connection ) {
        SqlOptions<SqlServerSqlQuery> options = new SqlOptions<SqlServerSqlQuery> {
            ConnectionString = connection
        };
        SqlServerSqlQuery sqlQuery = new SqlServerSqlQuery( _serviceProvider , options );
        return new SqlServerMetadataService( sqlQuery );
    }

    /// <summary>
    /// 创建PostgreSql数据库元数据服务
    /// </summary>
    private IMetadataService CreatePgSqlMetadataService( string connection ) {
        SqlOptions<PostgreSqlQuery> options = new SqlOptions<PostgreSqlQuery> {
            ConnectionString = connection
        };
        PostgreSqlQuery sqlQuery = new PostgreSqlQuery( _serviceProvider , options );
        return new PostgreSqlMetadataService( sqlQuery );
    }

    /// <summary>
    /// 创建MySql数据库元数据服务
    /// </summary>
    private IMetadataService CreateMySqlMetadataService( string connection ) {
        SqlOptions<MySqlQuery> options = new SqlOptions<MySqlQuery> {
            ConnectionString = connection
        };
        MySqlQuery sqlQuery = new MySqlQuery( _serviceProvider , options );
        return new MySqlMetadataService( sqlQuery );
    }
}