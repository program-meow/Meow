using System;
using Meow.Tests.Samples;
using Xunit;

namespace Meow.Tests.Helpers
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
            Assert.Null(Meow.Helpers.Common.To<string>(""));
            Assert.Equal("1A", Meow.Helpers.Common.To<string>("1A"));
            Assert.Equal(0, Meow.Helpers.Common.To<int>(null));
            Assert.Equal(0, Meow.Helpers.Common.To<int>(""));
            Assert.Equal(0, Meow.Helpers.Common.To<int>("2A"));
            Assert.Equal(1, Meow.Helpers.Common.To<int>("1"));
            Assert.Null(Meow.Helpers.Common.To<int?>(null));
            Assert.Null(Meow.Helpers.Common.To<int?>(""));
            Assert.Null(Meow.Helpers.Common.To<int?>("3A"));
            Assert.Equal(Guid.Empty, Meow.Helpers.Common.To<Guid>(""));
            Assert.Equal(Guid.Empty, Meow.Helpers.Common.To<Guid>("4A"));
            Assert.Equal(new Guid("B9EB56E9-B720-40B4-9425-00483D311DDC"), Meow.Helpers.Common.To<Guid>("B9EB56E9-B720-40B4-9425-00483D311DDC"));
            Assert.Equal(new Guid("B9EB56E9-B720-40B4-9425-00483D311DDC"), Meow.Helpers.Common.To<Guid?>("B9EB56E9-B720-40B4-9425-00483D311DDC"));
            Assert.Equal(12.5, Meow.Helpers.Common.To<double>("12.5"));
            Assert.Equal(12.5, Meow.Helpers.Common.To<double?>("12.5"));
            Assert.Equal(12.5M, Meow.Helpers.Common.To<decimal>("12.5"));
            Assert.True(Meow.Helpers.Common.To<bool>("true"));
            Assert.Equal(new DateTime(2000, 1, 1), Meow.Helpers.Common.To<DateTime>("2000-1-1"));
            Assert.Equal(new DateTime(2000, 1, 1), Meow.Helpers.Common.To<DateTime?>("2000-1-1"));
            var guid = Guid.NewGuid();
            Assert.Equal(guid.ToString(), Meow.Helpers.Common.To<string>(guid));
            Assert.Equal(EnumSample.C, Meow.Helpers.Common.To<EnumSample>("c"));
        }
    }
}
