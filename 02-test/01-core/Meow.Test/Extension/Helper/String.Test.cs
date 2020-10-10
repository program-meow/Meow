using Meow.Extension.Helper;
using Xunit;

namespace Meow.Test.Extension.Helper
{
    /// <summary>
    /// �����ַ�������
    /// </summary>
    public class StringTest
    {
        /// <summary>
        /// �����Ƿ��ֵ - �ַ���
        /// </summary>
        [Theory]
        [InlineData(null, true)]
        [InlineData("", true)]
        [InlineData(" ", true)]
        [InlineData("a", false)]
        public void TestIsEmpty_String(string value, bool result)
        {
            Assert.Equal(value.IsEmpty(), result);
        }

        /// <summary>
        /// ���ͼ���ת��
        /// </summary>
        [Fact]
        public void TestToList()
        {
            string value = null;
            Assert.Empty(value.ToList<string>());
            Assert.Single("1".ToList<string>());
            Assert.Equal(2, "1,2".ToList<string>().Count);
            Assert.Equal(2, "1,2".ToList<int>()[1]);
        }

        /// <summary>
        /// ���Ի�ȡƴ������
        /// </summary>
        [Theory]
        [InlineData(null, "")]
        [InlineData("", "")]
        [InlineData("�й�", "zg")]
        [InlineData("a1����b2", "a1bcb2")]
        [InlineData("����", "tt")]
        [InlineData("��", "y")]
        public void TestPinYin(string input, string result)
        {
            Assert.Equal(result, input.PinYin());
        }

        /// <summary>
        /// ����ĸСд
        /// </summary>
        [Theory]
        [InlineData(null, "")]
        [InlineData("", "")]
        [InlineData(" ", "")]
        [InlineData("a", "a")]
        [InlineData("A", "a")]
        [InlineData("Ab", "ab")]
        [InlineData("AB", "aB")]
        [InlineData("Abc", "abc")]
        public void TestFirstLowerCase(string value, string result)
        {
            Assert.Equal(result, value.FirstLowerCase());
        }

        /// <summary>
        /// ����ĸ��д
        /// </summary>
        [Theory]
        [InlineData(null, "")]
        [InlineData("", "")]
        [InlineData(" ", "")]
        [InlineData("a", "A")]
        [InlineData("A", "A")]
        [InlineData("ab", "Ab")]
        [InlineData("AB", "AB")]
        [InlineData("abC", "AbC")]
        public void TestFirstUpperCase(string value, string result)
        {
            Assert.Equal(result, value.FirstUpperCase());
        }

        /// <summary>
        /// �Ƴ�ĩβ�ַ���
        /// </summary>
        [Theory]
        [InlineData(null, "", "")]
        [InlineData("", "", "")]
        [InlineData(" ", " ", "")]
        [InlineData("AaA", "A", "Aa")]
        [InlineData("AA", "A", "A")]
        [InlineData("ABC", "C", "AB")]
        [InlineData("NetCore", "Core", "Net")]
        public void TestRemoveEnd(string value, string removeValue, string result)
        {
            Assert.Equal(result, value.RemoveEnd(removeValue));
        }

        /// <summary>
        /// �ָ�����
        /// </summary>
        [Theory]
        [InlineData(null, "")]
        [InlineData("", "")]
        [InlineData(" ", "")]
        [InlineData("AaA", "aa-a")]
        [InlineData("AA", "aa")]
        [InlineData("ABC", "abc")]
        [InlineData("NetCore", "net-core")]
        public void TestSplitWordGroup(string value, string result)
        {
            Assert.Equal(result, value.SplitWordGroup());
        }

        /// <summary>
        /// ����ѹ���ͽ�ѹ
        /// </summary>
        [Theory]
        [InlineData(null, "")]
        [InlineData("", "")]
        [InlineData(" ", "")]
        [InlineData("����������������������������������", "����������������������������������")]
        public void TestCompressAndDecompress(string value, string result)
        {
            var compressValue = value.Compress();
            var decompressValue = compressValue.Decompress();
            Assert.Equal(result, decompressValue);
        }

        /// <summary>
        /// �����ظ�����
        /// </summary>
        [Theory]
        [InlineData(null, 0, false)]
        [InlineData("a", 4, false)]
        [InlineData("b", 3, false)]
        [InlineData("c", 2, false)]
        [InlineData("d", 1, false)]
        [InlineData("NetCore", 0, false)]
        [InlineData("a", 5, true)]
        [InlineData("b", 4, true)]
        [InlineData("c", 3, true)]
        [InlineData("d", 2, true)]
        public void TestRepeat(string repeat, int count, bool isFuzzy)
        {
            var value = "ABCDabcdabcaba";
            Assert.Equal(count, value.Repeat(repeat, isFuzzy));
        }
    }
}