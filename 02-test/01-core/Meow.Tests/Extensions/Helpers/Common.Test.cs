using Meow.Extensions.Helpers;
using Xunit;

namespace Meow.Tests.Extensions.Helpers
{
    /// <summary>
    /// ���Թ�������
    /// </summary>
    public class CommonTest
    {
        /// <summary>
        /// ���԰�ȫ��ȡֵ
        /// </summary>
        [Fact]
        public void TestSafeValue()
        {
            int? number = null;
            Assert.Equal(0, number.SafeValue());
            number = 1;
            Assert.Equal(1, number.SafeValue());
        }

        /// <summary>
        /// ��ȫת��Ϊ�ַ���
        /// </summary>
        /// <param name="input">����ֵ</param>
        /// <param name="result">���</param>
        [Theory]
        [InlineData(null, "")]
        [InlineData("  ", "")]
        [InlineData(1, "1")]
        [InlineData(" 1 ", "1")]
        public void TestSafeString(object input, string result)
        {
            Assert.Equal(result, input.SafeString());
        }
    }
}
