﻿using System.Collections.Generic;
using System.Threading.Tasks;
using AspectCore.DynamicProxy.Parameters;
using Meow.Aspect.Core;
using Meow.Validation;

namespace Meow.Aspect
{
    /// <summary>
    /// 验证拦截器
    /// </summary>
    public class ValidAttribute : ParameterInterceptorBase
    {
        /// <summary>
        /// 执行
        /// </summary>
        public override async Task Invoke(ParameterAspectContext context, ParameterAspectDelegate next)
        {
            Validate(context.Parameter);
            await next(context);
        }

        /// <summary>
        /// 验证
        /// </summary>
        private void Validate(AspectCore.DynamicProxy.Parameters.Parameter parameter)
        {
            if (Meow.Helper.Reflection.IsGenericCollection(parameter.RawType))
            {
                ValidateCollection(parameter);
                return;
            }
            IValidation validation = parameter.Value as IValidation;
            validation?.Validate();
        }

        /// <summary>
        /// 验证集合
        /// </summary>
        private void ValidateCollection(AspectCore.DynamicProxy.Parameters.Parameter parameter)
        {
            if (!(parameter.Value is IEnumerable<IValidation> validations))
                return;
            foreach (var validation in validations)
                validation.Validate();
        }
    }
}