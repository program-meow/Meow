﻿using Meow.Exception;
using Meow.Parameter.Enum;
using Meow.Extension.Helper;
using Meow.Extension.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Meow.Application.Data.Core.Connection
{
    /// <summary>
    /// 连接对象
    /// </summary>
    public class Connection
    {
        /// <summary>
        /// 初始化连接对象
        /// </summary>
        /// <param name="type">数据库类型</param>
        /// <param name="server">服务端地址</param>
        /// <param name="database">数据库名称</param>
        /// <param name="userId">用户</param>
        /// <param name="password">密码</param>
        /// <param name="port">端口</param>
        public Connection(string type, string server, string database, string userId, string password, int? port = null)
        : this(type.ToEnum<Database>(), server, database, userId, password, port)
        {
        }

        /// <summary>
        /// 初始化连接对象
        /// </summary>
        /// <param name="type">数据库类型</param>
        /// <param name="server">服务端地址</param>
        /// <param name="database">数据库名称</param>
        /// <param name="userId">用户</param>
        /// <param name="password">密码</param>
        /// <param name="port">端口</param>
        public Connection(Database type, string server, string database, string userId, string password, int? port = null)
        {
            Type = type;
            Server = server;
            Database = database;
            UserId = userId;
            Password = password;
            Port = port;
        }

        /// <summary>
        /// 数据库类型
        /// </summary>
        [DisplayName("数据库类型")]
        [Required(ErrorMessage = "数据库类型不能为空")]
        public Database? Type { get; set; }
        /// <summary>
        /// 服务端地址
        /// </summary>
        [DisplayName("服务端地址")]
        [Required(ErrorMessage = "服务端地址不能为空")]
        public string Server { get; set; }
        /// <summary>
        /// 数据库名称
        /// </summary>
        [DisplayName("数据库名称")]
        [Required(ErrorMessage = "数据库名称不能为空")]
        public string Database { get; set; }
        /// <summary>
        /// 用户
        /// </summary>
        [DisplayName("用户")]
        [Required(ErrorMessage = "用户不能为空")]
        public string UserId { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        [DisplayName("密码")]
        [Required(ErrorMessage = "密码不能为空")]
        public string Password { get; set; }
        /// <summary>
        /// 端口
        /// </summary>
        [DisplayName("端口")]
        public int? Port { get; set; }

        /// <summary>
        /// 转换连接字符串
        /// </summary>
        public override string ToString()
        {
            this.Validate();
            switch (Type)
            {
                case Parameter.Enum.Database.SqlServer:
                    return $"Server={Server};Database={Database};uid={UserId};pwd={Password};MultipleActiveResultSets=true";
                case Parameter.Enum.Database.MySql:
                    return $"server={Server};{(Port.IsNull() ? "" : $"port={Port};")}database={Database};user id={UserId};password={Password};CharSet=utf8;";
                case Parameter.Enum.Database.PgSql:
                    return $"server={Server};{(Port.IsNull() ? "" : $"port={Port};")}database={Database};User Id={UserId};password={Password};";
                case Parameter.Enum.Database.Oracle:
                    throw new Warning("暂不支持该Oracle数据库，后续支持");
                    return $"Data Source=localhost/ORCL;User Id=system;Password=admin;";
                default:
                    throw new Warning("不支持该数据库类型");
            }
        }
    }
}