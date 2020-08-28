using System;
using Meow.Extensions.Helpers;
using Xunit;

namespace Meow.Tests.Extensions.Helpers
{
    /// <summary>
    /// 测试类型转换操作
    /// </summary>
    public class ConvertTest
    {
        /// <summary>
        /// 转换为布尔型
        /// </summary>
        /// <param name="input">输入值</param>
        /// <param name="result">结果</param>
        [Theory]
        [InlineData(null, false)]
        [InlineData("", false)]
        [InlineData("1A", false)]
        [InlineData("0", false)]
        [InlineData("否", false)]
        [InlineData("不", false)]
        [InlineData("no", false)]
        [InlineData("No", false)]
        [InlineData("false", false)]
        [InlineData("fail", false)]
        [InlineData("1", true)]
        [InlineData("是", true)]
        [InlineData("yes", true)]
        [InlineData("true", true)]
        [InlineData("ok", true)]
        public void TestToBool(object input, bool result)
        {
            Assert.Equal(result, input.ToBool());
        }

        /// <summary>
        /// 转换为可空布尔型
        /// </summary>
        /// <param name="input">输入值</param>
        /// <param name="result">结果</param>
        [Theory]
        [InlineData(null, null)]
        [InlineData("", null)]
        [InlineData("1A", null)]
        [InlineData("0", false)]
        [InlineData("否", false)]
        [InlineData("不", false)]
        [InlineData("no", false)]
        [InlineData("No", false)]
        [InlineData("false", false)]
        [InlineData("fail", false)]
        [InlineData("1", true)]
        [InlineData("是", true)]
        [InlineData("yes", true)]
        [InlineData("true", true)]
        [InlineData("ok", true)]
        public void TestToBoolOrNull(object input, bool? result)
        {
            Assert.Equal(result, input.ToBoolOrNull());
        }

        /// <summary>
        /// 转换为整型
        /// </summary>
        /// <param name="input">输入值</param>
        /// <param name="result">结果</param>
        [Theory]
        [InlineData(null, 0)]
        [InlineData("", 0)]
        [InlineData("1A", 0)]
        [InlineData("0", 0)]
        [InlineData("1", 1)]
        [InlineData("1778019.78", 1778020)]
        [InlineData("1778019.7801684", 1778020)]
        public void TestToInt(object input, int result)
        {
            Assert.Equal(result, input.ToInt());
        }

        /// <summary>
        /// 转换为可空整型
        /// </summary>
        /// <param name="input">输入值</param>
        /// <param name="result">结果</param>
        [Theory]
        [InlineData(null, null)]
        [InlineData("", null)]
        [InlineData("1A", null)]
        [InlineData("0", 0)]
        [InlineData("1", 1)]
        [InlineData("1778019.78", 1778020)]
        [InlineData("1778019.7801684", 1778020)]
        public void TestToIntOrNull(object input, int? result)
        {
            Assert.Equal(result, input.ToIntOrNull());
        }

        /// <summary>
        /// 转换为64位整型
        /// </summary>
        /// <param name="input">输入值</param>
        /// <param name="result">结果</param>
        [Theory]
        [InlineData(null, 0)]
        [InlineData("", 0)]
        [InlineData("1A", 0)]
        [InlineData("0", 0)]
        [InlineData("1", 1)]
        [InlineData("1778019.7801684", 1778020)]
        [InlineData("177801978016841234", 177801978016841234)]
        public void TestToLong(object input, long result)
        {
            Assert.Equal(result, input.ToLong());
        }

        /// <summary>
        /// 转换为64位可空整型
        /// </summary>
        /// <param name="input">输入值</param>
        /// <param name="result">结果</param>
        [Theory]
        [InlineData(null, null)]
        [InlineData("", null)]
        [InlineData("1A", null)]
        [InlineData("0", 0L)]
        [InlineData("1", 1L)]
        [InlineData("1778019.7801684", 1778020L)]
        [InlineData("177801978016841234", 177801978016841234L)]
        public void TestToLongOrNull(object input, long? result)
        {
            Assert.Equal(result, input.ToLongOrNull());
        }

        /// <summary>
        /// 转换为64位浮点型
        /// </summary>
        /// <param name="input">输入值</param>
        /// <param name="result">结果</param>
        /// <param name="digits">小数位数</param>
        [Theory]
        [InlineData(null, 0, null)]
        [InlineData("", 0, null)]
        [InlineData("1A", 0, null)]
        [InlineData("0", 0, null)]
        [InlineData("1", 1, null)]
        [InlineData("1.2", 1.2, null)]
        [InlineData("12.235", 12.24, 2)]
        [InlineData("12.345", 12.34, 2)]
        [InlineData("12.3451", 12.35, 2)]
        [InlineData("12.346", 12.35, 2)]
        public void TestToDouble(object input, double result, int? digits)
        {
            Assert.Equal(result, input.ToDouble(digits));
        }

        /// <summary>
        /// 转换为64位可空浮点型
        /// </summary>
        /// <param name="input">输入值</param>
        /// <param name="result">结果</param>
        /// <param name="digits">小数位数</param>
        [Theory]
        [InlineData(null, null, null)]
        [InlineData("", null, null)]
        [InlineData("1A", null, null)]
        [InlineData("0", 0d, null)]
        [InlineData("1", 1d, null)]
        [InlineData("1.2", 1.2, null)]
        [InlineData("12.355", 12.36, 2)]
        public void TestToDoubleOrNull(object input, double? result, int? digits)
        {
            Assert.Equal(result, input.ToDoubleOrNull(digits));
        }

        /// <summary>
        /// 转换为128位浮点型
        /// </summary>
        /// <param name="input">输入值</param>
        /// <param name="result">结果</param>
        /// <param name="digits">小数位数</param>
        [Theory]
        [InlineData(null, 0, null)]
        [InlineData("", 0, null)]
        [InlineData("1A", 0, null)]
        [InlineData("0", 0, null)]
        [InlineData("1", 1, null)]
        [InlineData("1.2", 1.2, null)]
        [InlineData("12.235", 12.24, 2)]
        [InlineData("12.345", 12.34, 2)]
        [InlineData("12.3451", 12.35, 2)]
        [InlineData("12.346", 12.35, 2)]
        public void TestToDecimal(object input, decimal result, int? digits)
        {
            Assert.Equal(result, input.ToDecimal(digits));
        }

        /// <summary>
        /// 转换为128位可空浮点型，验证
        /// </summary>
        /// <param name="input">输入值</param>
        /// <param name="result">结果</param>
        /// <param name="digits">小数位数</param>
        [Theory]
        [InlineData(null, null, null)]
        [InlineData("", null, null)]
        [InlineData("1A", null, null)]
        [InlineData("1A", null, 2)]
        public void TestToDecimalOrNull_Validate(object input, decimal? result, int? digits)
        {
            Assert.Equal(result, input.ToDecimalOrNull(digits));
        }

        /// <summary>
        /// 转换为128位可空浮点型，输入值为"0"
        /// </summary>
        [Fact]
        public void TestToDecimalOrNull()
        {
            Assert.Equal(0M, "0".ToDecimalOrNull());
            Assert.Equal(1.2M, "1.2".ToDecimalOrNull());
            Assert.Equal(23.46M, "23.456".ToDecimalOrNull(2));
        }

        /// <summary>
        /// 转换为日期，验证
        /// </summary>
        [Fact]
        public void TestToDate_Validate()
        {
            Assert.Equal(DateTime.MinValue, "".ToDate());
            Assert.Equal(DateTime.MinValue, "1A".ToDate());
        }

        /// <summary>
        /// 转换为日期
        /// </summary>
        [Fact]
        public void TestToDate()
        {
            Assert.Equal(new DateTime(2000, 1, 1), "2000-1-1".ToDate());
        }

        /// <summary>
        /// 转换为可空日期，验证
        /// </summary>
        /// <param name="input">输入值</param>
        /// <param name="result">结果</param>
        [Theory]
        [InlineData(null, null)]
        [InlineData("", null)]
        [InlineData("1A", null)]
        public void TestToDateOrNull_Validate(object input, DateTime? result)
        {
            Assert.Equal(result, input.ToDateOrNull());
        }

        /// <summary>
        /// 转换为可空日期
        /// </summary>
        [Fact]
        public void TestToDateOrNull()
        {
            Assert.Equal(new DateTime(2000, 1, 1), "2000-1-1".ToDateOrNull());
        }

        /// <summary>
        /// 转换为Guid，验证
        /// </summary>
        [Fact]
        public void TestToGuid_Validate()
        {
            Assert.Equal(Guid.Empty, "".ToGuid());
            Assert.Equal(Guid.Empty, "1A".ToGuid());
        }

        /// <summary>
        /// 转换为Guid
        /// </summary>
        [Fact]
        public void TestToGuid()
        {
            Assert.Equal(new Guid("B9EB56E9-B720-40B4-9425-00483D311DDC"), "B9EB56E9-B720-40B4-9425-00483D311DDC".ToGuid());
        }

        /// <summary>
        /// 转换为可空Guid，验证
        /// </summary>
        /// <param name="input">输入值</param>
        /// <param name="result">结果</param>
        [Theory]
        [InlineData(null, null)]
        [InlineData("", null)]
        [InlineData("1A", null)]
        public void TestToGuidOrNull_Validate(object input, Guid? result)
        {
            Assert.Equal(result, input.ToGuidOrNull());
        }

        /// <summary>
        /// 转换为可空Guid
        /// </summary>
        [Fact]
        public void TestToGuidOrNull()
        {
            Assert.Equal(new Guid("B9EB56E9-B720-40B4-9425-00483D311DDC"), "B9EB56E9-B720-40B4-9425-00483D311DDC".ToGuidOrNull());
        }

        /// <summary>
        /// 转换为Guid集合
        /// </summary>
        [Fact]
        public void TestToGuidList()
        {
            Assert.Empty("".ToGuidList());

            const string guid = "83B0233C-A24F-49FD-8083-1337209EBC9A";
            Assert.Single(guid.ToGuidList());
            Assert.Equal(new Guid(guid), guid.ToGuidList()[0]);

            const string guid2 = "83B0233C-A24F-49FD-8083-1337209EBC9A,EAB523C6-2FE7-47BE-89D5-C6D440C3033A";
            Assert.Equal(2, guid2.ToGuidList().Count);
            Assert.Equal(new Guid("83B0233C-A24F-49FD-8083-1337209EBC9A"), guid2.ToGuidList()[0]);
            Assert.Equal(new Guid("EAB523C6-2FE7-47BE-89D5-C6D440C3033A"), guid2.ToGuidList()[1]);
        }

        /// <summary>
        /// 转换为Guid集合
        /// </summary>
        [Fact]
        public void TestToGuidList_2()
        {
            const string guid = "83B0233C-A24F-49FD-8083-1337209EBC9A,,EAB523C6-2FE7-47BE-89D5-C6D440C3033A,";
            Assert.Equal(2, guid.ToGuidList().Count);
            Assert.Equal(new Guid("83B0233C-A24F-49FD-8083-1337209EBC9A"), guid.ToGuidList()[0]);
            Assert.Equal(new Guid("EAB523C6-2FE7-47BE-89D5-C6D440C3033A"), guid.ToGuidList()[1]);
        }
    }
}
