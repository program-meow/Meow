using Xunit;

namespace Meow.Tests.Helpers
{
    /// <summary>
    /// ���Ը����Ͳ���
    /// </summary>
    public class DoubleTest
    {
        /// <summary>
        /// ת��Ϊ64λ������
        /// </summary>
        /// <param name="input">����ֵ</param>
        /// <param name="result">���</param>
        /// <param name="digits">С��λ��</param>
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
            Assert.Equal(result, Meow.Helpers.Double.ToDouble(input, digits));
        }

        /// <summary>
        /// ת��Ϊ64λ�ɿո�����
        /// </summary>
        /// <param name="input">����ֵ</param>
        /// <param name="result">���</param>
        /// <param name="digits">С��λ��</param>
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
            Assert.Equal(result, Meow.Helpers.Double.ToDoubleOrNull(input, digits));
        }
    }
}
