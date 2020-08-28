using Meow.Helper;
using Xunit;

namespace Meow.Test.Helper
{
    /// <summary>
    /// ����128λ�����Ͳ���
    /// </summary>
    public class DecimalTest
    {
        /// <summary>
        /// ת��Ϊ128λ������
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
        public void TestToDecimal(object input, decimal result, int? digits)
        {
            Assert.Equal(result, Decimal.ToDecimal(input, digits));
        }

        /// <summary>
        /// ת��Ϊ128λ�ɿո����ͣ���֤
        /// </summary>
        /// <param name="input">����ֵ</param>
        /// <param name="result">���</param>
        /// <param name="digits">С��λ��</param>
        [Theory]
        [InlineData(null, null, null)]
        [InlineData("", null, null)]
        [InlineData("1A", null, null)]
        [InlineData("1A", null, 2)]
        public void TestToDecimalOrNull_Validate(object input, decimal? result, int? digits)
        {
            Assert.Equal(result, Decimal.ToDecimalOrNull(input, digits));
        }

        /// <summary>
        /// ת��Ϊ128λ�ɿո����ͣ�����ֵΪ"0"
        /// </summary>
        [Fact]
        public void TestToDecimalOrNull()
        {
            Assert.Equal(0M, Decimal.ToDecimalOrNull("0"));
            Assert.Equal(1.2M, Decimal.ToDecimalOrNull("1.2"));
            Assert.Equal(23.46M, Decimal.ToDecimalOrNull("23.456", 2));
        }
    }
}
