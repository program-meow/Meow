using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace Meow.Helper
{
    /// <summary>
    /// 反射操作
    /// </summary>
    public static class Reflection
    {
        #region GetDescription  [获取描述]

        /// <summary>
        /// 获取类型描述，使用DescriptionAttribute设置描述
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        public static string GetDescription<T>()
        {
            return GetDescription(Common.GetType<T>());
        }

        /// <summary>
        /// 获取类型成员描述，使用DescriptionAttribute设置描述
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="memberName">成员名称</param>
        public static string GetDescription<T>(string memberName)
        {
            return GetDescription(Common.GetType<T>(), memberName);
        }

        /// <summary>
        /// 获取类型成员描述，使用DescriptionAttribute设置描述
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="memberName">成员名称</param>
        public static string GetDescription(System.Type type, string memberName)
        {
            if (type == null)
                return string.Empty;
            if (Validation.IsEmpty(memberName))
                return string.Empty;
            return GetDescription(type.GetTypeInfo().GetMember(memberName).FirstOrDefault());
        }

        /// <summary>
        /// 获取类型成员描述，使用DescriptionAttribute设置描述
        /// </summary>
        /// <param name="member">成员</param>
        public static string GetDescription(MemberInfo member)
        {
            if (member == null)
                return string.Empty;
            return member.GetCustomAttribute<DescriptionAttribute>() is { } attribute ? attribute.Description : member.Name;
        }

        #endregion

        #region GetDisplayName  [获取显示名称]

        /// <summary>
        /// 获取显示名称，使用DisplayNameAttribute设置显示名称
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        public static string GetDisplayName<T>()
        {
            return GetDisplayName(Common.GetType<T>());
        }

        /// <summary>
        /// 获取显示名称，使用DisplayAttribute或DisplayNameAttribute设置显示名称
        /// </summary>
        public static string GetDisplayName(MemberInfo member)
        {
            if (member == null)
                return string.Empty;
            if (member.GetCustomAttribute<DisplayAttribute>() is { } displayAttribute)
                return displayAttribute.Name;
            if (member.GetCustomAttribute<DisplayNameAttribute>() is { } displayNameAttribute)
                return displayNameAttribute.DisplayName;
            return string.Empty;
        }

        #endregion

        #region GetDisplayNameOrDescription  [获取显示名称或描述]

        /// <summary>
        /// 获取显示名称或描述,使用DisplayNameAttribute设置显示名称,使用DescriptionAttribute设置描述
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        public static string GetDisplayNameOrDescription<T>()
        {
            return GetDisplayNameOrDescription(Common.GetType<T>());
        }

        /// <summary>
        /// 获取属性显示名称或描述,使用DisplayAttribute或DisplayNameAttribute设置显示名称,使用DescriptionAttribute设置描述
        /// </summary>
        public static string GetDisplayNameOrDescription(MemberInfo member)
        {
            string result = GetDisplayName(member);
            return Validation.IsEmpty(result) ? GetDescription(member) : result;
        }

        #endregion

        #region CreateInstance  [动态创建实例]

        /// <summary>
        /// 动态创建实例
        /// </summary>
        /// <typeparam name="T">目标类型</typeparam>
        /// <param name="type">类型</param>
        /// <param name="parameters">传递给构造函数的参数</param>        
        public static T CreateInstance<T>(System.Type type, params object[] parameters)
        {
            return Convert.To<T>(Activator.CreateInstance(type, parameters));
        }

        #endregion

        #region FindImplementTypes  [查找实现类型列表]

        /// <summary>
        /// 在指定的程序集中查找实现类型列表
        /// </summary>
        /// <typeparam name="TFind">查找类型</typeparam>
        /// <param name="assemblies">待查找的程序集列表</param>
        public static List<System.Type> FindImplementTypes<TFind>(params Assembly[] assemblies)
        {
            return FindImplementTypes(typeof(TFind), assemblies);
        }

        /// <summary>
        /// 在指定的程序集中查找实现类型列表
        /// </summary>
        /// <param name="findType">查找类型</param>
        /// <param name="assemblies">待查找的程序集列表</param>
        public static List<System.Type> FindImplementTypes(System.Type findType, params Assembly[] assemblies)
        {
            List<System.Type> result = new List<System.Type>();
            foreach (Assembly assembly in assemblies)
                result.AddRange(GetTypes(findType, assembly));
            return result.Distinct().ToList();
        }

        /// <summary>
        /// 获取类型列表
        /// </summary>
        private static List<System.Type> GetTypes(System.Type findType, Assembly assembly)
        {
            List<System.Type> result = new List<System.Type>();
            if (assembly == null)
                return result;
            System.Type[] types;
            try
            {
                types = assembly.GetTypes();
            }
            catch (ReflectionTypeLoadException)
            {
                return result;
            }
            foreach (System.Type type in types)
                AddType(result, findType, type);
            return result;
        }

        /// <summary>
        /// 添加类型
        /// </summary>
        private static void AddType(List<System.Type> result, System.Type findType, System.Type type)
        {
            if (type.IsInterface || type.IsAbstract)
                return;
            if (findType.IsAssignableFrom(type) == false && MatchGeneric(findType, type) == false)
                return;
            result.Add(type);
        }

        /// <summary>
        /// 泛型匹配
        /// </summary>
        private static bool MatchGeneric(System.Type findType, System.Type type)
        {
            if (findType.IsGenericTypeDefinition == false)
                return false;
            System.Type definition = findType.GetGenericTypeDefinition();
            foreach (System.Type implementedInterface in type.FindInterfaces((filter, criteria) => true, null))
            {
                if (implementedInterface.IsGenericType == false)
                    continue;
                return definition.IsAssignableFrom(implementedInterface.GetGenericTypeDefinition());
            }
            return false;
        }

        #endregion

        #region GetDirectInterfaceTypes  [获取直接接口类型列表]

        /// <summary>
        /// 获取直接接口类型列表
        /// </summary>
        /// <typeparam name="T">在该类型上查找接口</typeparam>
        /// <param name="baseInterfaceTypes">基接口类型列表,只返回继承了基接口的直接接口</param>
        public static List<System.Type> GetDirectInterfaceTypes<T>(params System.Type[] baseInterfaceTypes)
        {
            return GetDirectInterfaceTypes(typeof(T), baseInterfaceTypes);
        }

        /// <summary>
        /// 获取直接接口类型列表
        /// </summary>
        /// <param name="type">在该类型上查找接口</param>
        /// <param name="baseInterfaceTypes">基接口类型列表,只返回继承了基接口的直接接口</param>
        public static List<System.Type> GetDirectInterfaceTypes(System.Type type, params System.Type[] baseInterfaceTypes)
        {
            System.Type[] interfaceTypes = type.GetInterfaces();
            List<System.Type> directInterfaceTypes = interfaceTypes.Except(interfaceTypes.SelectMany(t => t.GetInterfaces())).ToList();
            if (baseInterfaceTypes == null || baseInterfaceTypes.Length == 0)
                return directInterfaceTypes;
            List<System.Type> result = new List<System.Type>();
            foreach (System.Type interfaceType in directInterfaceTypes)
            {
                if (interfaceType.GetInterfaces().Any(baseInterfaceTypes.Contains) == false)
                    continue;
                if (interfaceType.IsGenericType && !interfaceType.IsGenericTypeDefinition && interfaceType.FullName == null)
                {
                    result.Add(interfaceType.GetGenericTypeDefinition());
                    continue;
                }
                result.Add(interfaceType);
            }
            return result;
        }

        #endregion

        #region IsCollection  [是否集合]

        /// <summary>
        /// 是否集合
        /// </summary>
        /// <param name="type">类型</param>
        public static bool IsCollection(System.Type type)
        {
            if (type.IsArray)
                return true;
            return IsGenericCollection(type);
        }

        #endregion

        #region IsGenericCollection  [是否泛型集合]

        /// <summary>
        /// 是否泛型集合
        /// </summary>
        /// <param name="type">类型</param>
        public static bool IsGenericCollection(System.Type type)
        {
            if (!type.IsGenericType)
                return false;
            var typeDefinition = type.GetGenericTypeDefinition();
            return typeDefinition == typeof(IEnumerable<>)
                   || typeDefinition == typeof(IReadOnlyCollection<>)
                   || typeDefinition == typeof(IReadOnlyList<>)
                   || typeDefinition == typeof(ICollection<>)
                   || typeDefinition == typeof(IList<>)
                   || typeDefinition == typeof(List<>);
        }

        #endregion

        #region IsBool  [是否布尔类型]

        /// <summary>
        /// 是否布尔类型
        /// </summary>
        /// <param name="member">成员</param>
        public static bool IsBool(MemberInfo member)
        {
            if (member == null)
                return false;
            switch (member.MemberType)
            {
                case MemberTypes.TypeInfo:
                    return member.ToString() == Meow.Type.TypeFullName.Bool;
                case MemberTypes.Property:
                    return IsBool((PropertyInfo)member);
            }
            return false;
        }

        /// <summary>
        /// 是否布尔类型
        /// </summary>
        private static bool IsBool(PropertyInfo property)
        {
            return property.PropertyType == typeof(bool) || property.PropertyType == typeof(bool?);
        }

        #endregion

        #region IsEnum  [是否枚举类型]

        /// <summary>
        /// 是否枚举类型
        /// </summary>
        /// <param name="member">成员</param>
        public static bool IsEnum(MemberInfo member)
        {
            if (member == null)
                return false;
            switch (member.MemberType)
            {
                case MemberTypes.TypeInfo:
                    return ((TypeInfo)member).IsEnum;
                case MemberTypes.Property:
                    return IsEnum((PropertyInfo)member);
            }
            return false;
        }

        /// <summary>
        /// 是否枚举类型
        /// </summary>
        private static bool IsEnum(PropertyInfo property)
        {
            if (property.PropertyType.GetTypeInfo().IsEnum)
                return true;
            var value = Nullable.GetUnderlyingType(property.PropertyType);
            if (value == null)
                return false;
            return value.GetTypeInfo().IsEnum;
        }

        #endregion

        #region IsDate  [是否日期类型]

        /// <summary>
        /// 是否日期类型
        /// </summary>
        /// <param name="member">成员</param>
        public static bool IsDate(MemberInfo member)
        {
            if (member == null)
                return false;
            switch (member.MemberType)
            {
                case MemberTypes.TypeInfo:
                    return member.ToString() == Meow.Type.TypeFullName.DateTime;
                case MemberTypes.Property:
                    return IsDate((PropertyInfo)member);
            }
            return false;
        }

        /// <summary>
        /// 是否日期类型
        /// </summary>
        private static bool IsDate(PropertyInfo property)
        {
            if (property.PropertyType == typeof(DateTime))
                return true;
            if (property.PropertyType == typeof(DateTime?))
                return true;
            return false;
        }

        #endregion

        #region IsInt  [是否整型]

        /// <summary>
        /// 是否整型
        /// </summary>
        /// <param name="member">成员</param>
        public static bool IsInt(MemberInfo member)
        {
            if (member == null)
                return false;
            switch (member.MemberType)
            {
                case MemberTypes.TypeInfo:
                    return member.ToString() == Meow.Type.TypeFullName.Int || member.ToString() == Meow.Type.TypeFullName.Short || member.ToString() == Meow.Type.TypeFullName.Long;
                case MemberTypes.Property:
                    return IsInt((PropertyInfo)member);
            }
            return false;
        }

        /// <summary>
        /// 是否整型
        /// </summary>
        private static bool IsInt(PropertyInfo property)
        {
            if (property.PropertyType == typeof(int))
                return true;
            if (property.PropertyType == typeof(int?))
                return true;
            if (property.PropertyType == typeof(short))
                return true;
            if (property.PropertyType == typeof(short?))
                return true;
            if (property.PropertyType == typeof(long))
                return true;
            if (property.PropertyType == typeof(long?))
                return true;
            return false;
        }

        #endregion

        #region IsNumber  [是否数值类型]

        /// <summary>
        /// 是否浮点型
        /// </summary>
        /// <param name="member">成员</param>
        public static bool IsNumber(MemberInfo member)
        {
            if (member == null)
                return false;
            if (IsInt(member))
                return true;
            switch (member.MemberType)
            {
                case MemberTypes.TypeInfo:
                    return member.ToString() == Meow.Type.TypeFullName.Double || member.ToString() == Meow.Type.TypeFullName.Decimal || member.ToString() == Meow.Type.TypeFullName.Float;
                case MemberTypes.Property:
                    return IsNumber((PropertyInfo)member);
            }
            return false;
        }

        /// <summary>
        /// 是否数值类型
        /// </summary>
        private static bool IsNumber(PropertyInfo property)
        {
            if (property.PropertyType == typeof(double))
                return true;
            if (property.PropertyType == typeof(double?))
                return true;
            if (property.PropertyType == typeof(decimal))
                return true;
            if (property.PropertyType == typeof(decimal?))
                return true;
            if (property.PropertyType == typeof(float))
                return true;
            if (property.PropertyType == typeof(float?))
                return true;
            return false;
        }

        #endregion

        #region GetElementType  [获取元素类型]

        /// <summary>
        /// 获取元素类型，如果是集合，返回集合的元素类型
        /// </summary>
        /// <param name="type">类型</param>
        public static System.Type GetElementType(System.Type type)
        {
            if (IsCollection(type) == false)
                return type;
            if (type.IsArray)
                return type.GetElementType();
            System.Type[] genericArgumentsTypes = type.GetTypeInfo().GetGenericArguments();
            if (genericArgumentsTypes == null || genericArgumentsTypes.Length == 0)
                throw new ArgumentException(nameof(genericArgumentsTypes));
            return genericArgumentsTypes[0];
        }

        #endregion

        #region GetTopBaseType  [获取顶级基类]

        /// <summary>
        /// 获取顶级基类
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        public static System.Type GetTopBaseType<T>()
        {
            return GetTopBaseType(typeof(T));
        }

        /// <summary>
        /// 获取顶级基类
        /// </summary>
        /// <param name="type">类型</param>
        public static System.Type GetTopBaseType(System.Type type)
        {
            if (type == null)
                return null;
            if (type.IsInterface)
                return type;
            if (type.BaseType == typeof(object))
                return type;
            return GetTopBaseType(type.BaseType);
        }

        #endregion

        #region GetTypeEnum  [获取类型枚举]

        /// <summary>
        /// 获取类型枚举
        /// </summary>
        /// <param name="type">类型</param>
        public static Meow.Type.TypeEnum? GetTypeEnum(object type)
        {
            if (type == null)
                return null;
            return GetTypeEnumByType(type.GetType());
        }

        /// <summary>
        /// 获取类型枚举
        /// </summary>
        /// <param name="type">类型</param>
        public static Meow.Type.TypeEnum? GetTypeEnumByType(System.Type type)
        {
            if (type == null)
                return null;
            if (IsCollection(type))
                return Meow.Type.TypeEnum.List;
            return GetTypeEnumByMemberInfo(type.GetTypeInfo());
        }

        /// <summary>
        /// 获取类型枚举
        /// </summary>
        /// <param name="memberInfo">成员信息</param>
        private static Meow.Type.TypeEnum? GetTypeEnumByMemberInfo(MemberInfo memberInfo)
        {
            switch (memberInfo?.MemberType)
            {
                case MemberTypes.TypeInfo:
                    return GetTypeEnumByTypeInfo((TypeInfo)memberInfo);
                case MemberTypes.Property:
                    return GetTypeEnumByPropertyInfo((PropertyInfo)memberInfo);
                default:
                    return null;
            }
        }

        /// <summary>
        /// 获取类型枚举
        /// </summary>
        /// <param name="typeInfo">类型信息</param>
        private static Meow.Type.TypeEnum? GetTypeEnumByTypeInfo(TypeInfo typeInfo)
        {
            if (typeInfo.IsEnum)
                return Meow.Type.TypeEnum.Enum;
            if (typeInfo.IsClass)
                return Meow.Type.TypeEnum.Objects;
            switch (typeInfo.ToString())
            {
                case Meow.Type.TypeFullName.Sbyte:
                    return Meow.Type.TypeEnum.Sbyte;
                case Meow.Type.TypeFullName.Byte:
                    return Meow.Type.TypeEnum.Byte;
                case Meow.Type.TypeFullName.Short:
                    return Meow.Type.TypeEnum.Short;
                case Meow.Type.TypeFullName.Ushort:
                    return Meow.Type.TypeEnum.Ushort;
                case Meow.Type.TypeFullName.Int:
                    return Meow.Type.TypeEnum.Int;
                case Meow.Type.TypeFullName.Uint:
                    return Meow.Type.TypeEnum.Uint;
                case Meow.Type.TypeFullName.Long:
                    return Meow.Type.TypeEnum.Long;
                case Meow.Type.TypeFullName.Ulong:
                    return Meow.Type.TypeEnum.Ulong;
                case Meow.Type.TypeFullName.Nint:
                    return Meow.Type.TypeEnum.Nint;
                case Meow.Type.TypeFullName.Nuint:
                    return Meow.Type.TypeEnum.Nuint;
                case Meow.Type.TypeFullName.Float:
                    return Meow.Type.TypeEnum.Float;
                case Meow.Type.TypeFullName.Double:
                    return Meow.Type.TypeEnum.Double;
                case Meow.Type.TypeFullName.Decimal:
                    return Meow.Type.TypeEnum.Decimal;
                case Meow.Type.TypeFullName.Bool:
                    return Meow.Type.TypeEnum.Bool;
                case Meow.Type.TypeFullName.Char:
                    return Meow.Type.TypeEnum.Char;
                case Meow.Type.TypeFullName.String:
                    return Meow.Type.TypeEnum.String;
                case Meow.Type.TypeFullName.Guid:
                    return Meow.Type.TypeEnum.Guid;
                case Meow.Type.TypeFullName.DateTime:
                    return Meow.Type.TypeEnum.DateTime;
                default:
                    return null;
            }
        }

        /// <summary>
        /// 获取类型枚举
        /// </summary>
        /// <param name="propertyInfo">属性信息</param>
        private static Meow.Type.TypeEnum? GetTypeEnumByPropertyInfo(PropertyInfo propertyInfo)
        {
            if (IsEnum(propertyInfo))
                return Meow.Type.TypeEnum.Enum;
            if (IsObjects(propertyInfo))
                return Meow.Type.TypeEnum.Objects;
            if (propertyInfo.PropertyType == typeof(sbyte) || propertyInfo.PropertyType == typeof(sbyte?))
                return Meow.Type.TypeEnum.Sbyte;
            if (propertyInfo.PropertyType == typeof(byte) || propertyInfo.PropertyType == typeof(byte?))
                return Meow.Type.TypeEnum.Byte;
            if (propertyInfo.PropertyType == typeof(short) || propertyInfo.PropertyType == typeof(short?))
                return Meow.Type.TypeEnum.Short;
            if (propertyInfo.PropertyType == typeof(ushort) || propertyInfo.PropertyType == typeof(ushort?))
                return Meow.Type.TypeEnum.Ushort;
            if (propertyInfo.PropertyType == typeof(int) || propertyInfo.PropertyType == typeof(int?))
                return Meow.Type.TypeEnum.Int;
            if (propertyInfo.PropertyType == typeof(uint) || propertyInfo.PropertyType == typeof(uint?))
                return Meow.Type.TypeEnum.Uint;
            if (propertyInfo.PropertyType == typeof(long) || propertyInfo.PropertyType == typeof(long?))
                return Meow.Type.TypeEnum.Long;
            if (propertyInfo.PropertyType == typeof(ulong) || propertyInfo.PropertyType == typeof(ulong?))
                return Meow.Type.TypeEnum.Ulong;
            if (propertyInfo.PropertyType == typeof(nint) || propertyInfo.PropertyType == typeof(nint?))
                return Meow.Type.TypeEnum.Nint;
            if (propertyInfo.PropertyType == typeof(nuint) || propertyInfo.PropertyType == typeof(nuint?))
                return Meow.Type.TypeEnum.Nuint;
            if (propertyInfo.PropertyType == typeof(float) || propertyInfo.PropertyType == typeof(float?))
                return Meow.Type.TypeEnum.Float;
            if (propertyInfo.PropertyType == typeof(double) || propertyInfo.PropertyType == typeof(double?))
                return Meow.Type.TypeEnum.Double;
            if (propertyInfo.PropertyType == typeof(decimal) || propertyInfo.PropertyType == typeof(decimal?))
                return Meow.Type.TypeEnum.Decimal;
            if (propertyInfo.PropertyType == typeof(bool) || propertyInfo.PropertyType == typeof(bool?))
                return Meow.Type.TypeEnum.Bool;
            if (propertyInfo.PropertyType == typeof(char) || propertyInfo.PropertyType == typeof(char?))
                return Meow.Type.TypeEnum.Char;
            if (propertyInfo.PropertyType == typeof(string))
                return Meow.Type.TypeEnum.String;
            if (propertyInfo.PropertyType == typeof(Guid) || propertyInfo.PropertyType == typeof(Guid?))
                return Meow.Type.TypeEnum.Guid;
            if (propertyInfo.PropertyType == typeof(DateTime) || propertyInfo.PropertyType == typeof(DateTime?))
                return Meow.Type.TypeEnum.DateTime;
            return null;
        }

        /// <summary>
        /// 是否对象类型
        /// </summary>
        /// <param name="propertyInfo">属性信息</param>
        private static bool IsObjects(PropertyInfo propertyInfo)
        {
            if (propertyInfo.PropertyType.GetTypeInfo().IsClass)
                return true;
            System.Type value = Nullable.GetUnderlyingType(propertyInfo.PropertyType);
            if (value == null)
                return false;
            return value.GetTypeInfo().IsClass;
        }

        #endregion
    }
}
