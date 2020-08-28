using Xunit;

namespace Meow.Tests.Helpers
{
    /// <summary>
    /// ����32λ���Ͳ���
    /// </summary>
    public class InitTest
    {
        /// <summary>
        /// ת��Ϊ����
        /// </summary>
        /// <param name="input">����ֵ</param>
        /// <param name="result">���</param>
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
            Assert.Equal(result, Meow.Helpers.Init.ToInt(input));
        }

        /// <summary>
        /// ת��Ϊ�ɿ�����
        /// </summary>
        /// <param name="input">����ֵ</param>
        /// <param name="result">���</param>
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
            Assert.Equal(result, Meow.Helpers.Init.ToIntOrNull(input));
        }
    }
}
