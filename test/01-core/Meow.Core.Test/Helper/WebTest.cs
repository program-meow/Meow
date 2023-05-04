using System.Threading.Tasks;
using Meow.Extension;
using Xunit;

namespace Meow.Core.Test.Helper
{
    /// <summary>
    /// Web操作 - 测试
    /// </summary>
    public class WebTest
    {
        /// <summary>
        /// Http客户端
        /// </summary>
        [Fact]
        public async Task HttpClientTest()
        {
            //var aa = await Meow.Helper.Web.HttpClient.Get("http://localhost:49655/api/httpTest/get").Query("id", "001").RetryTimes().GetResultAsync();
            var bb = await Meow.Helper.Web.HttpClient.Get("http://localhost:49655/api/httpTest/file").Query("id", "001").GetResultAsync();
        }
    }
}
