using Xunit;

namespace Meow.Test.Helper
{
    /// <summary>
    /// ²âÊÔ×Ö·û´®²Ù×÷
    /// </summary>
    public class StringTest
    {
        /// <summary>
        /// ·ºÐÍ¼¯ºÏ×ª»»
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
        /// ²âÊÔ»ñÈ¡Æ´Òô¼òÂë
        /// </summary>
        [Theory]
        [InlineData(null, "")]
        [InlineData("", "")]
        [InlineData("ÖÐ¹ú", "zg")]
        [InlineData("a1±¦²Øb2", "a1bcb2")]
        [InlineData("÷Ò÷Ñ", "tt")]
        [InlineData(" Œ", "y")]
        public void TestPinYin(string input, string result)
        {
            Assert.Equal(result, Meow.Helper.String.PinYin(input));
        }

        /// <summary>
        /// Ê××ÖÄ¸Ð¡Ð´
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
        /// Ê××ÖÄ¸´óÐ´
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
        /// ÒÆ³ýÄ©Î²×Ö·û´®
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
        /// ·Ö¸ô´Ê×é
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
        /// ²âÊÔÑ¹ËõºÍ½âÑ¹
        /// </summary>
        [Theory]
        [InlineData(null, "")]
        [InlineData("", "")]
        [InlineData(" ", "")]
        [InlineData("¹þ¹þ¹þ¹þ¹þ¹þ¹þ¹þ¹þ¹þ¹þ¹þ¹þ¹þ¹þ¹þ¹þ", "¹þ¹þ¹þ¹þ¹þ¹þ¹þ¹þ¹þ¹þ¹þ¹þ¹þ¹þ¹þ¹þ¹þ")]
        public void TestCompressAndDecompress(string value, string result)
        {
            var compressValue = Meow.Helper.String.Compress(value);
            var decompressValue = Meow.Helper.String.Decompress(compressValue);
            Assert.Equal(result, decompressValue);
        }
    }
}