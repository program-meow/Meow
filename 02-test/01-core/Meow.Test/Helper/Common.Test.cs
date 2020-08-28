using Meow.Helper;
using Meow.Test.Sample;
using Xunit;
using DateTime = System.DateTime;
using Guid = System.Guid;

namespace Meow.Test.Helper
{
    /// <summary>
    /// 测试公共操作
    /// </summary>
    public class CommonTest
    {

        /// <summary>
        /// 通用泛型转换
        /// </summary>
        [Fact]
        public void TestTo()
        {
            Assert.Null(Common.To<string>(""));
            Assert.Equal("1A", Common.To<string>("1A"));
            Assert.Equal(0, Common.To<int>(null));
            Assert.Equal(0, Common.To<int>(""));
            Assert.Equal(0, Common.To<int>("2A"));
            Assert.Equal(1, Common.To<int>("1"));
            Assert.Null(Common.To<int?>(null));
            Assert.Null(Common.To<int?>(""));
            Assert.Null(Common.To<int?>("3A"));
            Assert.Equal(Guid.Empty, Common.To<Guid>(""));
            Assert.Equal(Guid.Empty, Common.To<Guid>("4A"));
            Assert.Equal(new Guid("B9EB56E9-B720-40B4-9425-00483D311DDC"), Common.To<Guid>("B9EB56E9-B720-40B4-9425-00483D311DDC"));
            Assert.Equal(new Guid("B9EB56E9-B720-40B4-9425-00483D311DDC"), Common.To<Guid?>("B9EB56E9-B720-40B4-9425-00483D311DDC"));
            Assert.Equal(12.5, Common.To<double>("12.5"));
            Assert.Equal(12.5, Common.To<double?>("12.5"));
            Assert.Equal(12.5M, Common.To<decimal>("12.5"));
            Assert.True(Common.To<bool>("true"));
            Assert.Equal(new DateTime(2000, 1, 1), Common.To<DateTime>("2000-1-1"));
            Assert.Equal(new DateTime(2000, 1, 1), Common.To<DateTime?>("2000-1-1"));
            var guid = Guid.NewGuid();
            Assert.Equal(guid.ToString(), Common.To<string>(guid));
            Assert.Equal(SampleEnum.C, Common.To<SampleEnum>("c"));
        }
    }
}
