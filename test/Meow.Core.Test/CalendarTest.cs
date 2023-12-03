using System;
using Meow.Calendar;
using Meow.Calendar.Extension;
using Meow.Extension;
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


            var dd = "2023/1007 12:22:10".ToDateTime();


            var gg = "20230929".ToDateTime().ToCalendar();
        }
    }
}
