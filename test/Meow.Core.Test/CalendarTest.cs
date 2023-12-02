using System;
using Meow.Calendar;
using Xunit;

namespace Meow.Core.Test {
    /// <summary>
    /// Web操作 - 测试
    /// </summary>
    public class CalendarTest {
        /// <summary>
        /// Http客户端
        /// </summary>
        [Fact]
        public void Test() {
            var aa = Meow.Calendar.Holiday.Get( 2023 , 10 , 7 );
            var aa1 = Meow.Calendar.Holiday.Get( "20231007" );

            var aa2 = Meow.Calendar.Holiday.Get( new DateTime( 2023 , 10 , 1 ) , DateTime.Now );

            var cc = Meow.Calendar.Lunar.FromDate( DateTime.Now );
        }
    }
}
