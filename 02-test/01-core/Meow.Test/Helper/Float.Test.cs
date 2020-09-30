using Meow.Helper;
using Xunit;

namespace Meow.Test.Helper
{
    /// <summary>
    /// ����32λ�����Ͳ���
    /// </summary>
    public class FloatTest
    {
        /// <summary>
        /// ת��Ϊ32λ������
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
        [InlineData("12.346", 12.35, 2)]
        public void TestToFloat(object input, float result, int? digits)
        {
            Assert.Equal(result, Float.ToFloat(input, digits));
        }

        /// <summary>
        /// ת��Ϊ32λ�ɿո�����
        /// </summary>
        /// <param name="input">����ֵ</param>
        /// <param name="result">���</param>
        /// <param name="digits">С��λ��</param>
        [Theory]
        [InlineData(null, null, null)]
        [InlineData("", null, null)]
        [InlineData("1A", null, null)]
        [InlineData("0", 0f, null)]
        [InlineData("1", 1f, null)]
        [InlineData("1.2", 1.2f, null)]
        [InlineData("12.346", 12.35f, 2)]
        public void TestToFloatOrNull(object input, float? result, int? digits)
        {
            Assert.Equal(result, Float.ToFloatOrNull(input, digits));
        }
    }
}
