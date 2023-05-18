﻿using System;
using System.Threading.Tasks;
using AspectCore.DynamicProxy.Parameters;
using Meow.Aop;

namespace Meow.Validation.Validator;

/// <summary>
/// 验证参数不能为null
/// </summary>
public class NotNullAttribute : ParameterInterceptorBase
{
    /// <summary>
    /// 执行
    /// </summary>
    public override Task Invoke(ParameterAspectContext context, ParameterAspectDelegate next)
    {
        if (context.Parameter.Value == null)
            throw new ArgumentNullException(context.Parameter.Name);
        return next(context);
    }
}