namespace Meow.Microservice.Dapr.StateManagements.Queries;

/// <summary>
/// 状态查询辅助操作
/// </summary>
public static class StateQueryHelper {
    /// <summary>
    /// 获取属性名
    /// </summary>
    public static string GetProperty<T>( Expression<Func<T , object>> expression ) {
        string property = Meow.Helper.Expression.GetName( expression );
        return property.IsEmpty() ? null : property.Split( '.' ).Select( Meow.Helper.String.FirstLowerCase ).Join( separator: "." );
    }
}