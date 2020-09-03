using System;
using Meow.Exception;
using Meow.Extension.Validation;
using Meow.Test.XUnitHelper;
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
        private readonly Sample.Sample _sample;

        /// <summary>
        /// ���Գ�ʼ��
        /// </summary>
        public CommonTest()
        {
            _sample = new Sample.Sample();
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
            AssertHelper.Throws<ArgumentNullException>(() => {
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
            _sample.TestValue = null;
            _sample.StringValue = "  ";
            AssertHelper.Throws<Warning> ( () => _sample.Validate(), "����ֵ����Ϊ��" );
        }

        /// <summary>
        /// ������֤
        /// </summary>
        [Fact]
        public void TestValidate_2()
        {
            _sample.TestValue = "http://123@qq.com";
            _sample.MaxLengthValue = "abcdefg";
            AssertHelper.Throws<Warning> ( () => _sample.Validate(), "��󳤶���2");
        }
    }
}