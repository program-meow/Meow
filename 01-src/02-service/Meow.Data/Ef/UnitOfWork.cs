using System;
using System.Collections.Generic;
using System.Reflection;
using Meow.Data.Ef.Core;
using Meow.Data.Ef.Core.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Meow.Data.Ef
{
    /// <summary>
    /// PgSql工作单元
    /// </summary>
    public abstract class UnitOfWork : UnitOfWorkBase
    {
        /// <summary>
        /// 初始化PgSql工作单元
        /// </summary>
        /// <param name="options">配置</param>
        /// <param name="serviceProvider">服务提供器</param>
        protected UnitOfWork(DbContextOptions options, IServiceProvider serviceProvider = null)
            : base(options, serviceProvider)
        {
        }

        /// <summary>
        /// 获取映射实例列表
        /// </summary>
        /// <param name="assembly">程序集</param>
        protected override IEnumerable<IMap> GetMapInstances(Assembly assembly)
        {
            return Meow.Helper.Reflection.GetInstancesByInterface<IMap>(assembly);
        }

        /// <summary>
        /// 拦截添加操作
        /// </summary>
        protected override void InterceptAddedOperation(EntityEntry entry)
        {
            base.InterceptAddedOperation(entry);
            Meow.Data.Ef.Internal.Helper.InitVersion(entry);
        }

        /// <summary>
        /// 拦截修改操作
        /// </summary>
        protected override void InterceptModifiedOperation(EntityEntry entry)
        {
            base.InterceptModifiedOperation(entry);
            Meow.Data.Ef.Internal.Helper.InitVersion(entry);
        }
    }
}