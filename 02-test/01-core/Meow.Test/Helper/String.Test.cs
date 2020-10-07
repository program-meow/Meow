using Xunit;

namespace Meow.Test.Helper
{
    /// <summary>
    /// �����ַ�������
    /// </summary>
    public class StringTest
    {
        /// <summary>
        /// ���ͼ���ת��
        /// </summary>
        [Fact]
        public void TestToList()
        {
            Assert.Empty(Meow.Helper.String.ToList<string>(null));
            Assert.Single(Meow.Helper.String.ToList<string>("1"));
            Assert.Equal(2, Meow.Helper.String.ToList<string>("1,2").Count);
            Assert.Equal(2, Meow.Helper.String.ToList<int>("1,2")[1]);
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
            Assert.Equal(result, Meow.Helper.String.PinYin(input));
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
            Assert.Equal(result, Meow.Helper.String.FirstLowerCase(value));
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
            Assert.Equal(result, Meow.Helper.String.FirstUpperCase(value));
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
            Assert.Equal(result, Meow.Helper.String.RemoveEnd(value, removeValue));
        }

        /// <summary>s
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
            Assert.Equal(result, Meow.Helper.String.SplitWordGroup(value));
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
            var compressValue = Meow.Helper.String.Compress(value);
            var decompressValue = Meow.Helper.String.Decompress(compressValue);
            Assert.Equal(result, decompressValue);
        }
    }
}