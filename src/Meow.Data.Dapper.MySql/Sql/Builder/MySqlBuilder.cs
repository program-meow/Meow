﻿namespace Meow.Data.Dapper.Sql.Builder;

/// <summary>
/// MySql Sql生成器
/// </summary>
public class MySqlBuilder : SqlBuilderBase {
    /// <summary>
    /// 初始化MySql Sql生成器
    /// </summary>
    /// <param name="parameterManager">Sql参数管理器</param>
    public MySqlBuilder( IParameterManager parameterManager = null )
        : base( parameterManager ) {
    }

    /// <inheritdoc />
    protected override IDialect CreateDialect() {
        return MySqlDialect.Instance;
    }

    /// <inheritdoc />
    protected override IColumnCache CreateColumnCache() {
        return MySqlColumnCache.Instance;
    }

    /// <inheritdoc />
    public override ISqlBuilder New() {
        return new MySqlBuilder( ParameterManager );
    }

    /// <inheritdoc />
    public override ISqlBuilder Clone() {
        MySqlBuilder result = new MySqlBuilder();
        result.Clone( this );
        return result;
    }
}