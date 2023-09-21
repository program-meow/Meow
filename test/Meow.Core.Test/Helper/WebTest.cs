using System.Threading.Tasks;
using Meow.Response;
using Xunit;

namespace Meow.Core.Test.Helper {
    /// <summary>
    /// Web操作 - 测试
    /// </summary>
    public class WebTest {
        /// <summary>
        /// Http客户端
        /// </summary>
        [Fact]
        public async Task HttpClientTest() {
            Result<string> aa = await Meow.Helper.Web.HttpClient.Get( "http://localhost:49655/api/httpTest/get" ).Query( "id" , "001" ).RetryTimes().GetResultAsync();
            Result<string> bb = await Meow.Helper.Web.HttpClient.Get( "http://localhost:49655/api/httpTest/file" ).Query( "id" , "001" ).GetResultAsync();
        }
    }
}
