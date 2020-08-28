using Meow.Helper;
using Xunit;

namespace Meow.Test.Helper
{
    /// <summary>
    /// ���Բ����Ͳ���
    /// </summary>
    public class BoolTest
    {
        /// <summary>
        /// ת��Ϊ������
        /// </summary>
        /// <param name="input">����ֵ</param>
        /// <param name="result">���</param>
        [Theory]
        [InlineData(null, false)]
        [InlineData("", false)]
        [InlineData("1A", false)]
        [InlineData("0", false)]
        [InlineData("��", false)]
        [InlineData("��", false)]
        [InlineData("no", false)]
        [InlineData("No", false)]
        [InlineData("false", false)]
        [InlineData("fail", false)]
        [InlineData("1", true)]
        [InlineData("��", true)]
        [InlineData("yes", true)]
        [InlineData("true", true)]
        [InlineData("ok", true)]
        public void TestToBool(object input, bool result)
        {
            Assert.Equal(result, Bool.ToBool(input));
        }

        /// <summary>
        /// ת��Ϊ�ɿղ�����
        /// </summary>
        /// <param name="input">����ֵ</param>
        /// <param name="result">���</param>
        [Theory]
        [InlineData(null, null)]
        [InlineData("", null)]
        [InlineData("1A", null)]
        [InlineData("0", false)]
        [InlineData("��", false)]
        [InlineData("��", false)]
        [InlineData("no", false)]
        [InlineData("No", false)]
        [InlineData("false", false)]
        [InlineData("fail", false)]
        [InlineData("1", true)]
        [InlineData("��", true)]
        [InlineData("yes", true)]
        [InlineData("true", true)]
        [InlineData("ok", true)]
        public void TestToBoolOrNull(object input, bool? result)
        {
            Assert.Equal(result, Bool.ToBoolOrNull(input));
        }
    }
}
