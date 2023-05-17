using Meow.Data.Sql.Builder;
using Meow.Data.Sql.Builder.Core;

namespace Meow.Data.Dapper.MySql.Sql.Builder;

/// <summary>
/// MySql方言
/// </summary>
public class MySqlDialect : DialectBase
{
    /// <summary>
    /// 封闭构造方法
    /// </summary>
    private MySqlDialect()
    {
    }

    /// <summary>
    /// MySql方言实例
    /// </summary>
    public static readonly IDialect Instance = new MySqlDialect();

    /// <inheritdoc />
    public override string GetOpeningIdentifier()
    {
        return "`";
    }

    /// <inheritdoc />
    public override string GetClosingIdentifier()
    {
        return "`";
    }

    /// <inheritdoc />
    public override string GetPrefix()
    {
        return "@";
    }
}