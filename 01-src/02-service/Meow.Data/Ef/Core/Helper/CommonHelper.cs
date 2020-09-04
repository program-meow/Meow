using System;
using System.Text;
using Meow.Domain.Core.Model;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Meow.Data.Ef.Core.Helper
{
    /// <summary>
    /// 公共工具操作
    /// </summary>
    internal static class CommonHelper
    {
        /// <summary>
        /// 初始化版本号
        /// </summary>
        public static void InitVersion(EntityEntry entry)
        {
            if (!(entry.Entity is IVersion entity))
                return;
            entity.Version = Encoding.UTF8.GetBytes(Guid.NewGuid().ToString());
        }
    }
}