using Meow.Common.Test.Sample;
using Meow.Query.Core.Criteria;
using Xunit;

namespace Meow.Test.Query.Core.Criteria {
    /// <summary>
    /// 测试默认查询条件
    /// </summary>
    public class DefaultCriteriaTest {
        /// <summary>
        /// 测试获取查询条件
        /// </summary>
        [Fact]
        public void TestGetPredicate() {
            var criteria = new DefaultCriteria<SampleAggregateRoot>( t => t.Name == "a" );
            Assert.Equal( "t => (t.Name == \"a\")", criteria.GetPredicate().ToString() );
        }
    }
}
