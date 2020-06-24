using System;
using System.Text;
using Meow.Extensions.Helpers;
using Meow.Tests.Samples;
using Meow.Tests.XUnitHelpers;
using Newtonsoft.Json;
using Xunit;

namespace Meow.Tests.Extensions.Helpers
{
    /// <summary>
    /// 测试JSON操作扩展
    /// </summary>
    public class JsonTest
    {
        /// <summary>
        /// 测试循环引用序列化
        /// </summary>
        [Fact]
        public void TestLoop()
        {
            A a = new A { Name = "a" };
            B b = new B { Name = "b" };
            C c = new C { Name = "c" };
            a.B = b;
            b.C = c;
            c.A = a;
            AssertHelper.Throws<JsonSerializationException>(() => c.ToJson());
        }

        /// <summary>
        /// 测试转成Json
        /// </summary>
        [Fact]
        public void TestToJson()
        {
            var result = new StringBuilder();
            result.Append("{");
            result.Append("\"Name\":\"a\",");
            result.Append("\"nickname\":\"b\",");
            result.Append("\"Value\":null,");
            result.Append("\"Date\":\"2012/1/1 0:00:00\",");
            result.Append("\"Age\":1,");
            result.Append("\"isShow\":true");
            result.Append("}");
            var actualData = JsonTestSample.Create();
            actualData.Date = DateTime.Parse(actualData.Date).ToString("yyyy/M/d 0:00:00");
            Assert.Equal(result.ToString(), actualData.ToJson());
        }

        /// <summary>
        /// 测试转成Json，将双引号转成单引号
        /// </summary>
        [Fact]
        public void TestToJson_ToSingleQuotes()
        {
            var result = new StringBuilder();
            result.Append("{");
            result.Append("'Name':'a',");
            result.Append("'nickname':'b',");
            result.Append("'Value':null,");
            result.Append("'Date':'2012/1/1 0:00:00',");
            result.Append("'Age':1,");
            result.Append("'isShow':true");
            result.Append("}");

            var actualData = JsonTestSample.Create();
            actualData.Date = DateTime.Parse(actualData.Date).ToString("yyyy/M/d 0:00:00");
            Assert.Equal(result.ToString(), actualData.ToJson( "'"));
        }

        /// <summary>
        /// 测试转成对象
        /// </summary>
        [Fact]
        public void TestToObject()
        {
            var customer = "{\"Name\":\"a\"}".ToObject<JsonTestSample>();
            Assert.Equal("a", customer.Name);
        }
    }
}
