using Xunit;

namespace Meow.Common.Test
{
    /// <summary>
    /// 对象测试
    /// </summary>
    public class SampleTest
    {
        /// <summary>
        /// 测试设置消息
        /// </summary>
        [Fact]
        public void TestDefault()
        {
            var sample = new Sample.Sample();
            Assert.Equal("默认值", sample.DefaultValue);
            sample.DefaultValue = "改变值";
            Assert.Equal("改变值", sample.DefaultValue);
        }
    }
}