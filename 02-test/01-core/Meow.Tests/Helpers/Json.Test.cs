using System;
using System.Text;
using Meow.Helpers;
using Meow.Tests.Samples;
using Meow.Tests.XUnitHelpers;
using Newtonsoft.Json;
using Xunit;

namespace Meow.Tests.Helpers
{
    /// <summary>
    /// ����JSON����
    /// </summary>
    public class JsonTest
    {
        /// <summary>
        /// ����ѭ���������л�
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
            AssertHelper.Throws<JsonSerializationException>(() => Json.ToJson(c));
        }

        /// <summary>
        /// ת��Json,��֤��
        /// </summary>
        [Fact]
        public void TestToJson_Null()
        {
            Assert.Empty(Json.ToJson(null));
        }

        /// <summary>
        /// ����ת��Json
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
            Assert.Equal(result.ToString(), Json.ToJson(actualData));
        }

        /// <summary>
        /// ����ת��Json����˫����ת�ɵ�����
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
            Assert.Equal(result.ToString(), Json.ToJson(actualData, "'"));
        }

        /// <summary>
        /// ����ת�ɶ���
        /// </summary>
        [Fact]
        public void TestToObject()
        {
            var customer = Json.ToObject<JsonTestSample>("{\"Name\":\"a\"}");
            Assert.Equal("a", customer.Name);
        }
    }
}
