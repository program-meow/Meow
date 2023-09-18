using SystemExpression = System.Linq.Expressions.Expression;

namespace Meow.Expression;

/// <summary>
/// 参数重绑定操作
/// </summary>
public class ParameterRebinder : ExpressionVisitor {
    /// <summary>
    /// 参数字典
    /// </summary>
    private readonly Dictionary<ParameterExpression , ParameterExpression> _map;

    /// <summary>
    /// 初始化参数重绑定操作
    /// </summary>
    /// <param name="map">参数字典</param>
    public ParameterRebinder( Dictionary<ParameterExpression , ParameterExpression> map ) {
        _map = map ?? new Dictionary<ParameterExpression , ParameterExpression>();
    }

    /// <summary>
    /// 替换参数
    /// </summary>
    /// <param name="map">参数字典</param>
    /// <param name="exp">表达式</param>
    public static SystemExpression ReplaceParameters( Dictionary<ParameterExpression , ParameterExpression> map , SystemExpression exp ) {
        return new ParameterRebinder( map ).Visit( exp );
    }

    /// <summary>
    /// 访问参数
    /// </summary>
    /// <param name="parameterExpression">参数</param>
    protected override SystemExpression VisitParameter( ParameterExpression parameterExpression ) {
        if( _map.TryGetValue( parameterExpression , out ParameterExpression replacement ) )
            parameterExpression = replacement;
        return base.VisitParameter( parameterExpression );
    }
}