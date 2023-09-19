namespace Meow.Data.Sql.Builder.Operation;

/// <summary>
/// Sql查询操作
/// </summary>
public interface ISqlQueryOperation : IStart, ISelect, IFrom, IJoin, IWhere, IGroupBy, IOrderBy, IEnd, ISqlParameter, ISet {
}