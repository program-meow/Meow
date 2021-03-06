﻿using System;
using System.Threading.Tasks;
using AspectCore.DynamicProxy.Parameters;
using Meow.Aspect.Core;
using Meow.Extension.Helper;

namespace Meow.Aspect
{
    /// <summary>
    /// 验证不能为空
    /// </summary>
    public class NotEmptyAttribute : ParameterInterceptorBase
    {
        /// <summary>
        /// 执行
        /// </summary>
        public override Task Invoke(ParameterAspectContext context, ParameterAspectDelegate next)
        {
            if (context.Parameter.Value.SafeString().IsEmpty())
                throw new ArgumentNullException(context.Parameter.Name);
            return next(context);
        }
    }
}