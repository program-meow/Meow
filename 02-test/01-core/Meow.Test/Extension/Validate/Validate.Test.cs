using System;
using Meow.Extension.Validate;
using Meow.Test.XUnitHelper;
using Xunit;

namespace Meow.Test.Extension.Validate
{
    /// <summary>
    /// ��������֤��չ
    /// </summary>
    public class ValidateTest
    {
        /// <summary>
        /// ����ֵ����Ϊ��������ִ��
        /// </summary>
        [Fact]
        public void TestCheckNull()
        {
            object test = new object();
            test.CheckNull("test");
        }

        /// <summary>
        /// ����ֵ��ֵΪnull���׳��쳣
        /// </summary>
        [Fact]
        public void TestCheckNull_Null_Throw()
        {
            AssertHelper.Throws<ArgumentNullException>(() => {
                object test = new object();
                test = null;
                test.CheckNull("test");
            }, "test");
        }
    }
}
