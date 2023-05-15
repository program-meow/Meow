using System;
using Meow.Data.Query;
using Meow.Data.Sql.Builder.Condition;
using Meow.Data.Sql.Builder.Param;
using Meow.Math;

namespace Meow.Data.Sql.Builder;

/// <summary>
/// Sql条件工厂
/// </summary>
public class SqlConditionFactory : IConditionFactory
{
    /// <summary>
    /// Sql参数管理器
    /// </summary>
    private readonly IParameterManager _parameterManager;

    /// <summary>
    /// 初始化Sql条件工厂
    /// </summary>
    /// <param name="parameterManager">Sql参数管理器</param>
    public SqlConditionFactory(IParameterManager parameterManager)
    {
        _parameterManager = parameterManager ?? throw new ArgumentNullException(nameof(parameterManager));
    }

    /// <inheritdoc />
    public virtual ISqlCondition Create(string column, object value, OperatorEnum @operator, bool isParameterization = true)
    {
        if (IsInCondition(@operator, value))
            return new InCondition(_parameterManager, column, value, isParameterization);
        if (IsNotInCondition(@operator, value))
            return new NotInCondition(_parameterManager, column, value, isParameterization);
        switch (@operator)
        {
            case OperatorEnum.Equal:
                return new EqualCondition(_parameterManager, column, value, isParameterization);
            case OperatorEnum.NotEqual:
                return new NotEqualCondition(_parameterManager, column, value, isParameterization);
            case OperatorEnum.Greater:
                return new GreaterCondition(_parameterManager, column, value, isParameterization);
            case OperatorEnum.GreaterEqual:
                return new GreaterEqualCondition(_parameterManager, column, value, isParameterization);
            case OperatorEnum.Less:
                return new LessCondition(_parameterManager, column, value, isParameterization);
            case OperatorEnum.LessEqual:
                return new LessEqualCondition(_parameterManager, column, value, isParameterization);
            case OperatorEnum.Contains:
                return new ContainsCondition(_parameterManager, column, value, isParameterization);
            case OperatorEnum.Starts:
                return new StartsCondition(_parameterManager, column, value, isParameterization);
            case OperatorEnum.Ends:
                return new EndsCondition(_parameterManager, column, value, isParameterization);
            default:
                throw new NotImplementedException();
        }
    }

    /// <summary>
    /// 是否In条件
    /// </summary>
    private bool IsInCondition(OperatorEnum @operator, object value)
    {
        if (@operator == OperatorEnum.In)
            return true;
        return false;
    }

    /// <summary>
    /// 是否Not In条件
    /// </summary>
    private bool IsNotInCondition(OperatorEnum @operator, object value)
    {
        if (@operator == OperatorEnum.NotIn)
            return true;
        return false;
    }

    /// <inheritdoc />
    public virtual ISqlCondition Create(string column, object minValue, object maxValue, BoundaryEnum boundary, bool isParameterization = true)
    {
        return new SegmentCondition(_parameterManager, column, minValue, maxValue, boundary, isParameterization);
    }
}