using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Meow.Extension.Helper;
using Xunit;

namespace Meow.Common.Test
{
    /// <summary>
    /// 测试
    /// </summary>
    public class TestA
    {

        /// <summary>
        /// 测试将集合连接为带分隔符的字符串
        /// </summary>
        [Fact]
        public void TestFun()
        {
            var a = new Sample.Sample
            {
                TestValue = "测试"
            };

            var bb = new List<Sample.Sample>();

            // var b = Test.ToStr(a);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public static class Test
    {

        #region 反射为字符串

        /// <summary>
        /// 反射为字符串
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="object">泛型对象</param>
        /// <param name="differentiate">区分符</param>
        /// <param name="separator">分隔符</param>
        public static string ToStr<T>(List<T> @object, char differentiate = '=', char separator = '&')
            where T : new()
        {
            var result = string.Empty;
            var template = "{0}" + differentiate + "{1}" + separator;
            result = ToListStrByTemplate(@object, template).Aggregate(result, (current, item) => current + item);
            return result.IsEmpty() ?
                result :
                result.Remove(result.Length - 1, 1);
        }

        /// <summary>
        /// 反射为字符串
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="object">泛型对象</param>
        /// <param name="differentiate">区分符</param>
        /// <param name="separator">分隔符</param>
        public static string ToStr<T>(T @object, char differentiate = '=', char separator = '&')
            where T : new()
        {
            var result = string.Empty;
            var template = "{0}" + differentiate + "{1}" + separator;
            result = ToListStrByTemplate(@object, template).Aggregate(result, (current, item) => current + item);
            return result.IsEmpty() ?
                result :
                result.Remove(result.Length - 1, 1);
        }


        #endregion

        #region 根据模范反射为字符串集合

        /// <summary>
        /// 根据模范反射为字符串集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="object">泛型对象</param>
        /// <param name="template">模板 
        /// <para> {0}：字段名 </para>
        /// <para> {1}：值 </para>
        /// <para> 例：aaa{0}bbbb{1}</para>
        /// </param>
        public static List<string> ToListStrByTemplate<T>(List<T> @object, string template)
            where T : new()
        {
            var result = new List<string>();
            if (@object.Count == 0)
                return result;
            for (var i = 0; i < @object.Count; i++)
                result.AddRange(ToListStrByTemplate(@object[i], null, i, template));
            return result;
        }

        /// <summary>
        /// 根据模范反射为字符串集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="object">泛型对象</param>
        /// <param name="template">模板 
        /// <para> {0}：字段名 </para>
        /// <para> {1}：值 </para>
        /// <para> 例：aaa{0}bbbb{1}</para>
        /// </param>
        public static List<string> ToListStrByTemplate<T>(T @object, string template)
            where T : new()
        {
            return @object.IsNull() ?
                new List<string>() :
             ToListStrByTemplate(@object, null, null, template);
        }


        /// <summary>
        /// 根据模范反射为字符串
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="object">泛型对象</param>
        /// <param name="parentName">父名</param>
        /// <param name="count">下角标</param>
        /// <param name="template">模板</param>
        private static List<string> ToListStrByTemplate<T>(T @object, string parentName, int? count, string template)
            where T : new()
        {
            var result = new List<string>();
            if (@object.IsNull())
                return result;
            var type = @object.GetType();
            if (type.IsValueType || type.Name.StartsWith("String"))
            {
                result.Add(string.Format(template, parentName, @object));
                return result;
            }
            var properties = type.GetProperties(BindingFlags.Instance | BindingFlags.Public);
            if (properties.Length <= 0)
                return result;
            var cacheParentName = parentName.IsEmpty() ? string.Empty : $"{parentName}.";
            foreach (var item in properties)
            {
                var name = count.IsNull() ? item.Name : $"{item.Name}[{count}]";
                var value = item.GetValue(@object, null);
                if (item.PropertyType.Name.Contains("[]") || item.PropertyType.Name.StartsWith("List"))
                {
                    var listCache = (IList)value;
                    for (var i = 0; i < listCache.Count; i++) result.AddRange(ToListStrByTemplate(listCache[i], $"{cacheParentName}{name}[{i}]", count, template));
                    continue;
                }
                if (item.PropertyType.IsValueType || item.PropertyType.Name.StartsWith("String"))
                {
                    result.Add(string.Format(template, cacheParentName + name, value));
                    continue;
                }
                result.AddRange(ToListStrByTemplate(value, parentName + name, count, template));
            }
            return result;
        }


        #endregion
    }


}
