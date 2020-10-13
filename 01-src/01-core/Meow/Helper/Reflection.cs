using Meow.Extension.Helper;
using Meow.Extension.Parameter.Enum;
using Meow.Extension.Parameter.Object;
using Meow.Parameter.Enum;
using Meow.Parameter.Object;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Reflection;
using MicrosoftSystem = System;

namespace Meow.Helper
{
    /// <summary>
    /// 反射操作
    /// </summary>
    public static class Reflection
    {
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
        public static string GetDescription(MicrosoftSystem.Type type, string memberName)
        {
            if (type == null)
                return string.Empty;
            if (string.IsNullOrWhiteSpace(memberName))
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
            return member.GetCustomAttribute<DescriptionAttribute>() is DescriptionAttribute attribute ? attribute.Description : member.Name;
        }

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
            if (member.GetCustomAttribute<DisplayAttribute>() is DisplayAttribute displayAttribute)
                return displayAttribute.Name;
            if (member.GetCustomAttribute<DisplayNameAttribute>() is DisplayNameAttribute displayNameAttribute)
                return displayNameAttribute.DisplayName;
            return string.Empty;
        }

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
            var result = GetDisplayName(member);
            return string.IsNullOrWhiteSpace(result) ? GetDescription(member) : result;
        }

        /// <summary>
        /// 查找类型列表
        /// </summary>
        /// <typeparam name="TFind">查找类型</typeparam>
        /// <param name="assemblies">待查找的程序集列表</param>
        public static List<MicrosoftSystem.Type> FindTypes<TFind>(params Assembly[] assemblies)
        {
            var findType = typeof(TFind);
            return FindTypes(findType, assemblies);
        }

        /// <summary>
        /// 查找类型列表
        /// </summary>
        /// <param name="findType">查找类型</param>
        /// <param name="assemblies">待查找的程序集列表</param>
        public static List<MicrosoftSystem.Type> FindTypes(MicrosoftSystem.Type findType, params Assembly[] assemblies)
        {
            var result = new List<MicrosoftSystem.Type>();
            foreach (var assembly in assemblies)
                result.AddRange(GetTypes(findType, assembly));
            return result.Distinct().ToList();
        }

        /// <summary>
        /// 获取类型列表
        /// </summary>
        private static List<MicrosoftSystem.Type> GetTypes(MicrosoftSystem.Type findType, Assembly assembly)
        {
            var result = new List<MicrosoftSystem.Type>();
            if (assembly == null)
                return result;
            MicrosoftSystem.Type[] types;
            try
            {
                types = assembly.GetTypes();
            }
            catch (ReflectionTypeLoadException)
            {
                return result;
            }
            foreach (var type in types)
                AddType(result, findType, type);
            return result;
        }

        /// <summary>
        /// 添加类型
        /// </summary>
        private static void AddType(List<MicrosoftSystem.Type> result, MicrosoftSystem.Type findType, MicrosoftSystem.Type type)
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
        private static bool MatchGeneric(MicrosoftSystem.Type findType, MicrosoftSystem.Type type)
        {
            if (findType.IsGenericTypeDefinition == false)
                return false;
            var definition = findType.GetGenericTypeDefinition();
            foreach (var implementedInterface in type.FindInterfaces((filter, criteria) => true, null))
            {
                if (implementedInterface.IsGenericType == false)
                    continue;
                return definition.IsAssignableFrom(implementedInterface.GetGenericTypeDefinition());
            }
            return false;
        }

        /// <summary>
        /// 获取实现了接口的所有实例
        /// </summary>
        /// <typeparam name="TInterface">接口类型</typeparam>
        /// <param name="assemblies">待查找的程序集列表</param>
        public static List<TInterface> GetInstancesByInterface<TInterface>(params Assembly[] assemblies)
        {
            return FindTypes<TInterface>(assemblies)
                .Select(t => CreateInstance<TInterface>(t)).ToList();
        }

        /// <summary>
        /// 动态创建实例
        /// </summary>
        /// <typeparam name="T">目标类型</typeparam>
        /// <param name="type">类型</param>
        /// <param name="parameters">传递给构造函数的参数</param>        
        public static T CreateInstance<T>(MicrosoftSystem.Type type, params object[] parameters)
        {
            return Meow.Helper.Common.To<T>(MicrosoftSystem.Activator.CreateInstance(type, parameters));
        }

        /// <summary>
        /// 获取程序集
        /// </summary>
        /// <param name="assemblyName">程序集名称</param>
        public static Assembly GetAssembly(string assemblyName)
        {
            return Assembly.Load(new AssemblyName(assemblyName));
        }

        /// <summary>
        /// 是否布尔类型
        /// </summary>
        /// <param name="member">成员</param>
        public static bool IsBool(MemberInfo member)
        {
            if (member == null)
                return false;
            return member.MemberType switch
            {
                MemberTypes.TypeInfo => (member.ToString() == "System.Boolean"),
                MemberTypes.Property => IsBool((PropertyInfo)member),
                _ => false
            };
        }

        /// <summary>
        /// 是否布尔类型
        /// </summary>
        private static bool IsBool(PropertyInfo property)
        {
            return property.PropertyType == typeof(bool) || property.PropertyType == typeof(bool?);
        }

        /// <summary>
        /// 是否枚举类型
        /// </summary>
        /// <param name="member">成员</param>
        public static bool IsEnum(MemberInfo member)
        {
            if (member == null)
                return false;
            return member.MemberType switch
            {
                MemberTypes.TypeInfo => ((TypeInfo)member).IsEnum,
                MemberTypes.Property => IsEnum((PropertyInfo)member),
                _ => false
            };
        }

        /// <summary>
        /// 是否枚举类型
        /// </summary>
        private static bool IsEnum(PropertyInfo property)
        {
            if (property.PropertyType.GetTypeInfo().IsEnum)
                return true;
            var value = MicrosoftSystem.Nullable.GetUnderlyingType(property.PropertyType);
            if (value == null)
                return false;
            return value.GetTypeInfo().IsEnum;
        }

        /// <summary>
        /// 是否日期类型
        /// </summary>
        /// <param name="member">成员</param>
        public static bool IsDate(MemberInfo member)
        {
            if (member == null)
                return false;
            return member.MemberType switch
            {
                MemberTypes.TypeInfo => (member.ToString() == "System.DateTime"),
                MemberTypes.Property => IsDate((PropertyInfo)member),
                _ => false
            };
        }

        /// <summary>
        /// 是否日期类型
        /// </summary>
        private static bool IsDate(PropertyInfo property)
        {
            if (property.PropertyType == typeof(MicrosoftSystem.DateTime))
                return true;
            if (property.PropertyType == typeof(MicrosoftSystem.DateTime?))
                return true;
            return false;
        }

        /// <summary>
        /// 是否整型
        /// </summary>
        /// <param name="member">成员</param>
        public static bool IsInt(MemberInfo member)
        {
            if (member == null)
                return false;
            return member.MemberType switch
            {
                MemberTypes.TypeInfo => (member.ToString() == "System.Int32" || member.ToString() == "System.Int16" || member.ToString() == "System.Int64"),
                MemberTypes.Property => IsInt((PropertyInfo)member),
                _ => false
            };
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

        /// <summary>
        /// 是否数值类型
        /// </summary>
        /// <param name="member">成员</param>
        public static bool IsNumber(MemberInfo member)
        {
            if (member == null)
                return false;
            if (IsInt(member))
                return true;
            return member.MemberType switch
            {
                MemberTypes.TypeInfo => (member.ToString() == "System.Double" || member.ToString() == "System.Decimal" || member.ToString() == "System.Single"),
                MemberTypes.Property => IsNumber((PropertyInfo)member),
                _ => false
            };
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

        /// <summary>
        /// 是否集合
        /// </summary>
        /// <param name="type">类型</param>
        public static bool IsCollection(MicrosoftSystem.Type type)
        {
            if (type.IsArray)
                return true;
            return IsGenericCollection(type);
        }

        /// <summary>
        /// 是否泛型集合
        /// </summary>
        /// <param name="type">类型</param>
        public static bool IsGenericCollection(MicrosoftSystem.Type type)
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

        /// <summary>
        /// 从目录中获取所有程序集
        /// </summary>
        /// <param name="directoryPath">目录绝对路径</param>
        public static List<Assembly> GetAssemblies(string directoryPath)
        {
            return Directory.GetFiles(directoryPath, "*.*", SearchOption.AllDirectories).ToList()
                .Where(t => t.EndsWith(".exe") || t.EndsWith(".dll"))
                .Select(path => Assembly.Load(new AssemblyName(path))).ToList();
        }

        /// <summary>
        /// 获取公共属性列表
        /// </summary>
        /// <param name="instance">实例</param>
        public static List<Item> GetPublicProperties(object instance)
        {
            var properties = instance.GetType().GetProperties();
            return properties.ToList().Select(t => new Item(t.Name, t.GetValue(instance))).ToList();
        }

        /// <summary>
        /// 获取顶级基类
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        public static MicrosoftSystem.Type GetTopBaseType<T>()
        {
            return GetTopBaseType(typeof(T));
        }

        /// <summary>
        /// 获取顶级基类
        /// </summary>
        /// <param name="type">类型</param>
        public static MicrosoftSystem.Type GetTopBaseType(MicrosoftSystem.Type type)
        {
            if (type == null)
                return null;
            if (type.IsInterface)
                return type;
            if (type.BaseType == typeof(object))
                return type;
            return GetTopBaseType(type.BaseType);
        }

        /// <summary>
        /// 获取元素类型，如果是集合，返回集合的元素类型
        /// </summary>
        /// <param name="type">类型</param>
        public static MicrosoftSystem.Type GetElementType(MicrosoftSystem.Type type)
        {
            if (IsCollection(type) == false)
                return type;
            if (type.IsArray)
                return type.GetElementType();
            var genericArgumentsTypes = type.GetTypeInfo().GetGenericArguments();
            if (genericArgumentsTypes == null || genericArgumentsTypes.Length == 0)
                throw new MicrosoftSystem.ArgumentException("泛型类型参数不能为空");
            return genericArgumentsTypes[0];
        }

        #region 解析对象

        /// <summary>
        /// 解析对象
        /// </summary>
        /// <typeparam name="T">对象元素类型</typeparam>
        /// <param name="value">值  暂不支持Dictionary、List&lt;List&lt;object&gt;&gt;、object[][]类型</param>
        public static List<ItemObjectTree> Analyzing<T>(T value) where T : class
        {
            if (value.IsNull())
                return new List<ItemObjectTree>();
            if (value.GetTypeMediumPrecisionEnum() == TypeMediumPrecision.Collection)
                return new List<ItemObjectTree>();
            return AnalyzingObject(value);
        }

        /// <summary>
        /// 解析对象
        /// </summary>
        /// <typeparam name="T">集合元素类型</typeparam>
        /// <param name="value">值  暂不支持Dictionary、List&lt;List&lt;object&gt;&gt;、object[][]类型</param>
        public static List<ItemObjectTree> Analyzing<T>(IEnumerable<T> value) where T : class
        {
            if (value.IsEmpty())
                return new List<ItemObjectTree>();
            var result = new List<ItemObjectTree>();
            var count = 1;
            foreach (var item in value)
            {
                var itemResult = Analyzing(item);
                if (itemResult.IsEmpty())
                    continue;
                result.Add(new ItemObjectTree(null, item.GetTypeHighPrecisionEnum(), null, itemResult, count));
                count += 1;
            }
            return result;
        }

        #region 解析对象实现

        /// <summary>
        /// 解析对象
        /// </summary>
        /// <param name="value">值  暂不支持Dictionary、List&lt;List&lt;object&gt;&gt;、object[][]类型</param>
        private static List<ItemObjectTree> AnalyzingObject(object value)
        {
            if (value.IsNull())
                return new List<ItemObjectTree>();
            if (value.IsSingleType())
                return new List<ItemObjectTree> { new ItemObjectTree(null, value.GetTypeHighPrecisionEnum(), value) };
            var properties = value.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);
            if (properties.IsEmpty())
                return new List<ItemObjectTree>();
            return AnalyzingObject(value, properties);
        }

        /// <summary>
        /// 解析对象
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="properties">属性信息集合</param>
        private static List<ItemObjectTree> AnalyzingObject(object value, PropertyInfo[] properties)
        {
            var result = new List<ItemObjectTree>();
            var count = 1;
            foreach (var item in properties)
            {
                var itemResult = AnalyzingObject(value, item, count);
                if (itemResult.IsNull())
                    continue;
                result.Add(itemResult);
                count += 1;
            }
            return result;
        }

        /// <summary>
        /// 解析对象
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="property">属性信息</param>
        /// <param name="sortId">排序号</param>
        private static ItemObjectTree AnalyzingObject(object value, PropertyInfo property, int sortId)
        {
            if (property.IsNull())
                return null;
            if (property.IsNestingCollectionType())
                return null;
            if (property.IsDictionaryType())
                return null;
            var itemValue = property.GetValue(value, null);
            if (property.IsCollectionType())
                return AnalyzingCollectionType(itemValue, property, sortId);
            if (property.IsSingleType())
                return AnalyzingSingleType(itemValue, property, sortId);
            return AnalyzingObjectType(itemValue, property, sortId);
        }

        /// <summary>
        /// 解析集合类型
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="property">属性信息</param>
        /// <param name="sortId">排序号</param>
        private static ItemObjectTree AnalyzingCollectionType(object value, PropertyInfo property, int sortId)
        {
            var subsets = new List<ItemObjectTree>();
            var listCache = (IList)value ?? new List<object>();
            var count = 1;
            foreach (var item in listCache)
            {
                var itemResult = AnalyzingCollectionType(item, count);
                if (itemResult.IsNull())
                    continue;
                subsets.Add(itemResult);
                count += 1;
            }
            if (subsets.IsEmpty())
                return null;
            return new ItemObjectTree(property.Name, property.GetTypeHighPrecisionEnum(), null, subsets, sortId);
        }

        /// <summary>
        /// 解析集合类型
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="sortId">排序号</param>
        private static ItemObjectTree AnalyzingCollectionType(object value, int sortId)
        {
            var subsets = AnalyzingObject(value);
            if (subsets.IsEmpty())
                return null;
            return new ItemObjectTree(null, value.GetTypeHighPrecisionEnum(), null, subsets, sortId);
        }

        /// <summary>
        /// 解析单类型
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="property">属性信息</param>
        /// <param name="sortId">排序号</param>
        private static ItemObjectTree AnalyzingSingleType(object value, PropertyInfo property, int sortId)
        {
            if (value.SafeString().IsEmpty())
                return null;
            return new ItemObjectTree(property.Name, property.GetTypeHighPrecisionEnum(), value, sortId);
        }

        /// <summary>
        /// 解析对象类型
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="property">属性信息</param>
        /// <param name="sortId">排序号</param>
        private static ItemObjectTree AnalyzingObjectType(object value, PropertyInfo property, int sortId)
        {
            var subsets = AnalyzingObject(value);
            if (subsets.IsEmpty())
                return null;
            return new ItemObjectTree(property.Name, property.GetTypeHighPrecisionEnum(), null, subsets, sortId);
        }

        #endregion

        #endregion

        #region 解析集合对象到列表集合

        /// <summary>
        /// 解析对象到列表集合
        /// </summary>
        /// <typeparam name="T">对象元素类型</typeparam>
        /// <param name="value">值  暂不支持Dictionary、List&lt;List&lt;object&gt;&gt;、object[][]类型</param>
        public static List<Item> AnalyzingToItems<T>(T value) where T : class
        {
            var result = new List<Item>();
            var objectTrees = Analyzing(value);
            foreach (var item in objectTrees)
            {
                var itemResult = AnalyzingToItems(item, item.Type.IsSingleType() ? null : item.Text);
                result.AddNoNull(itemResult);
            }
            return result.DefaultSortId();
        }

        /// <summary>
        /// 解析集合对象到列表集合
        /// </summary>
        /// <typeparam name="T">集合元素类型</typeparam>
        /// <param name="value">值  暂不支持Dictionary、List&lt;List&lt;object&gt;&gt;、object[][]类型</param>
        public static List<Item> AnalyzingToItems<T>(IEnumerable<T> value) where T : class
        {
            var result = new List<Item>();
            var objectTrees = Analyzing(value);
            var count = 0;
            foreach (var item in objectTrees)
            {
                var itemResult = AnalyzingToItems(item, count);
                if (itemResult.IsEmpty())
                    continue;
                result.AddRange(itemResult);
                count += 1;
            }
            return result.DefaultSortId();
        }

        #region 解析集合对象到列表集合实现

        /// <summary>
        /// 解析集合对象到列表集合
        /// </summary>
        /// <param name="item">项</param>
        /// <param name="count">计数</param>
        private static List<Item> AnalyzingToItems(ItemObjectTree item, int count)
        {
            var result = new List<Item>();
            foreach (var subset in item.Subsets)
            {
                subset.Text = $"{subset.Text}[{count}]";
                var subsetResult = AnalyzingToItems(subset, subset.Type.IsSingleType() ? null : subset.Text);
                result.AddRange(subsetResult);
            }
            return result;
        }

        /// <summary>
        /// 解析对象到列表集合
        /// </summary>
        /// <param name="item">项</param>
        /// <param name="parentName">父名称</param>
        private static List<Item> AnalyzingToItems(ItemObjectTree item, string parentName)
        {
            return item.Type.ToMedium() switch
            {
                TypeMediumPrecision.Null => null,
                TypeMediumPrecision.Collection => AnalyzingToCollectionItems(item.Subsets, parentName),
                TypeMediumPrecision.Object => AnalyzingToObjectItems(item.Subsets, parentName),
                _ => AnalyzingToSingleItems(item, parentName)
            };
        }

        /// <summary>
        /// 解析对象到集合列表集合
        /// </summary>
        /// <param name="list">集合</param>
        /// <param name="parentName">父名称</param>
        private static List<Item> AnalyzingToCollectionItems(List<ItemObjectTree> list, string parentName)
        {
            var result = new List<Item>();
            var count = 0;
            foreach (var item in list)
            {
                var itemResult = AnalyzingToCollectionItems(item, $"{parentName}[{count}]");
                if (itemResult.IsEmpty())
                    continue;
                result.AddNoNull(itemResult);
                count += 1;
            }
            return result;
        }

        /// <summary>
        /// 解析对象到集合列表集合
        /// </summary>
        /// <param name="item">项</param>
        /// <param name="itemName">项名称</param>
        private static List<Item> AnalyzingToCollectionItems(ItemObjectTree item, string itemName)
        {
            if (item.Subsets.IsEmpty())
                return new List<Item>();
            return item.Type.ToMedium() switch
            {
                TypeMediumPrecision.Null => new List<Item>(),
                TypeMediumPrecision.Collection => AnalyzingToItems(item, itemName).Where(t => !t.Value.SafeString().IsEmpty()).ToList(),
                TypeMediumPrecision.Object => AnalyzingToItems(item, itemName).Where(t => !t.Value.SafeString().IsEmpty()).ToList(),
                _ => (item.Subsets[0].Value.SafeString().IsEmpty() ? new List<Item>() : new List<Item> { new Item(itemName, item.Subsets[0].Value) })
            };
        }

        /// <summary>
        /// 解析对象到对象列表集合
        /// </summary>
        /// <param name="list">集合</param>
        /// <param name="parentName">父名称</param>
        private static List<Item> AnalyzingToObjectItems(List<ItemObjectTree> list, string parentName)
        {
            var result = new List<Item>();
            foreach (var item in list)
            {
                var itemResult = AnalyzingToObjectItem(item, parentName);
                if (item.Type != TypeHighPrecision.Object)
                    result.AddNoNull(itemResult);
                if (item.Type.IsSingleType())
                    continue;
                var subsets = AnalyzingToItems(item, itemResult.Text);
                result.AddNoNull(subsets);
            }
            return result;
        }

        /// <summary>
        /// 解析对象到对象
        /// </summary>
        /// <param name="item">项</param>
        /// <param name="parentName">父名称</param>
        private static Item AnalyzingToObjectItem(ItemObjectTree item, string parentName)
        {
            var name = parentName.IsEmpty() ? item.Text : $"{parentName}.{item.Text}";
            return new Item(name, item.Value);
        }

        /// <summary>
        /// 解析对象到单类型列表集合
        /// </summary>
        /// <param name="item">项</param>
        /// <param name="parentName">父名称</param>
        private static List<Item> AnalyzingToSingleItems(ItemObjectTree item, string parentName)
        {
            if (item.Value.SafeString().IsEmpty())
                return new List<Item>();
            var name = parentName.IsEmpty() ? item.Text : $"{parentName}.{item.Text}";
            return new List<Item>
            {
                new Item(name, item.Value)
            };
        }

        #endregion

        #endregion
    }
}