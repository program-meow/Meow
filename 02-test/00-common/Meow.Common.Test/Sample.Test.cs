using Xunit;

namespace Meow.Common.Test
{
    /// <summary>
    /// �������
    /// </summary>
    public class SampleTest
    {
        /// <summary>
        /// ����������Ϣ
        /// </summary>
        [Fact]
        public void TestDefault()
        {
            var sample = new Sample.Sample();
            Assert.Equal("Ĭ��ֵ", sample.DefaultValue);
            sample.DefaultValue = "�ı�ֵ";
            Assert.Equal("�ı�ֵ", sample.DefaultValue);
        }
    }
}