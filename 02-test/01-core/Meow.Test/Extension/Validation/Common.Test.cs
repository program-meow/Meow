using System;
using Meow.Common.Test.Sample;
using Meow.Common.Test.XUnitHelper;
using Meow.Exception;
using Meow.Extension.Validation;
using Xunit;

namespace Meow.Test.Extension.Validation
{
    /// <summary>
    /// 测试是验证扩展
    /// </summary>
    public class CommonTest
    {
        /// <summary>
        /// 测试样例
        /// </summary>
        private readonly Sample _sample;

        /// <summary>
        /// 测试初始化
        /// </summary>
        public CommonTest()
        {
            _sample = new Sample
            {
                StringValue = "abc",
                TestValue = "http://123@qq.com",
            };
        }

        /// <summary>
        /// 检查空值，不为空则正常执行
        /// </summary>
        [Fact]
        public void TestCheckNull()
        {
            object test = new object();
            test.CheckNull("test");
        }

        /// <summary>
        /// 检查空值，值为null则抛出异常
        /// </summary>
        [Fact]
        public void TestCheckNull_Null_Throw()
        {
            AssertHelper.Throws<ArgumentNullException>(() =>
            {
                object test = new object();
                test = null;
                test.CheckNull("test");
            }, "test");
        }

        /// <summary>
        /// 测试验证
        /// </summary>
        [Fact]
        public void TestValidate_1()
        {
            _sample.StringValue = "  ";
            AssertHelper.Throws<Warning>(() => _sample.Validate(), "不能为空");
        }

        /// <summary>
        /// 测试验证
        /// </summary>
        [Fact]
        public void TestValidate_2()
        {
            _sample.MaxLengthValue = "abcdefg";
            AssertHelper.Throws<Warning>(() => _sample.Validate(), "最大长度是2");
        }
    }
}