using System.Linq;
using Meow.Common.Test.Sample;
using Meow.Validation;
using Xunit;

namespace Meow.Test.Validation
{
    /// <summary>
    /// 测试验证操作
    /// </summary>
    public class DataAnnotationValidationTest
    {
        /// <summary>
        /// 测试样例
        /// </summary>
        private readonly Sample _sample;

        /// <summary>
        /// 测试初始化
        /// </summary>
        public DataAnnotationValidationTest()
        {
            _sample = new Sample();
        }

        /// <summary>
        /// 测试验证
        /// </summary>
        [Fact]
        public void TestValidate_1()
        {
            _sample.TestValue = null;
            _sample.StringValue = "  ";
            var result = DataAnnotationValidation.Validate(_sample);
            Assert.Equal(2, result.Count);
        }

        /// <summary>
        /// 测试验证
        /// </summary>
        [Fact]
        public void TestValidate_2()
        {
            _sample.TestValue = null;
            var result = DataAnnotationValidation.Validate(_sample);
            Assert.True(result.Count(t => t.ErrorMessage == "测试值不能为空") == 1);
        }

        /// <summary>
        /// 测试验证
        /// </summary>
        [Fact]
        public void TestValidate_3()
        {
            _sample.MaxLengthValue = "abcdefg";
            var result = DataAnnotationValidation.Validate(_sample);
            Assert.True(result.Count(t => t.ErrorMessage == "最大长度是2") == 1);
        }
    }
}