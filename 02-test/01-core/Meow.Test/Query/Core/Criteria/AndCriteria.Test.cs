using Meow.Common.Test.Sample;
using Meow.Query.Core.Criteria;
using Xunit;

namespace Meow.Test.Query.Core.Criteria {
    /// <summary>
    /// 测试与查询条件
    /// </summary>
    public class AndCriteriaTest {
        /// <summary>
        /// 测试获取查询条件
        /// </summary>
        [Fact]
        public void TestGetPredicate() {
            var criteria = new AndCriteria<SampleAggregateRoot>( t => t.Name == "a",t => t.Name != "b" );
            Assert.Equal( "t => ((t.Name == \"a\") AndAlso (t.Name != \"b\"))", criteria.GetPredicate().ToString() );
        }
    }
}
