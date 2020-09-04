using System;
using Meow.Common.Test.Sample;
using Meow.Common.Test.XUnitHelper;
using Meow.Exception;
using Meow.Extension.Validation;
using Xunit;

namespace Meow.Test.Extension.Validation
{
    /// <summary>
    /// ��������֤��չ
    /// </summary>
    public class CommonTest
    {
        /// <summary>
        /// ��������
        /// </summary>
        private readonly Sample _sample;

        /// <summary>
        /// ���Գ�ʼ��
        /// </summary>
        public CommonTest()
        {
            _sample = new Sample
            {
                StringValue = "abc",
                TestValue = "http://123@qq.com",
            };
        }

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
            AssertHelper.Throws<ArgumentNullException>(() =>
            {
                object test = new object();
                test = null;
                test.CheckNull("test");
            }, "test");
        }

        /// <summary>
        /// ������֤
        /// </summary>
        [Fact]
        public void TestValidate_1()
        {
            _sample.StringValue = "  ";
            AssertHelper.Throws<Warning>(() => _sample.Validate(), "����Ϊ��");
        }

        /// <summary>
        /// ������֤
        /// </summary>
        [Fact]
        public void TestValidate_2()
        {
            _sample.MaxLengthValue = "abcdefg";
            AssertHelper.Throws<Warning>(() => _sample.Validate(), "��󳤶���2");
        }
    }
}