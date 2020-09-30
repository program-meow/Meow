using System;
using Meow.Exception;
using Xunit;

namespace Meow.Test.Exception
{
    /// <summary>
    /// Ӧ�ó����쳣����
    /// </summary>
    public class WarningTest
    {
        /// <summary>
        /// ����������Ϣ
        /// </summary>
        [Fact]
        public void TestMessage()
        {
            Warning warning = new Warning("A");
            Assert.Equal("A", warning.Message);
        }

        /// <summary>
        /// ������ϢΪ��
        /// </summary>
        [Fact]
        public void TestMessage_Null()
        {
            Warning warning = new Warning(null, "A");
            Assert.Equal(string.Empty, warning.Message);
        }

        /// <summary>
        /// �������ô�����
        /// </summary>
        [Fact]
        public void TestCode()
        {
            Warning warning = new Warning("", "B");
            Assert.Equal("B", warning.Code);
        }

        /// <summary>
        /// ���������쳣
        /// </summary>
        [Fact]
        public void TestException()
        {
            Warning warning = new Warning(new System.Exception("A"));
            Assert.Empty(warning.Message);
            Assert.Equal("A", warning.GetMessage());
        }

        /// <summary>
        /// �������ô�����Ϣ���쳣
        /// </summary>
        [Fact]
        public void TestMessageAndException()
        {
            Warning warning = new Warning("A", "", new System.Exception("C"));
            Assert.Equal("A", warning.Message);
            Assert.Equal($"A{Environment.NewLine}C", warning.GetMessage());
        }

        /// <summary>
        /// ��������2���쳣
        /// </summary>
        [Fact]
        public void TestException_2Layer()
        {
            Warning warning = new Warning("A", "", new System.Exception("C", new NotImplementedException("D")));
            Assert.Equal(3, warning.GetExceptions().Count);
            Assert.Equal(typeof(Warning), warning.GetExceptions()[0].GetType());
            Assert.Equal(typeof(System.Exception), warning.GetExceptions()[1].GetType());
            Assert.Equal(typeof(NotImplementedException), warning.GetExceptions()[2].GetType());
            Assert.Equal("A", warning.GetExceptions()[0].Message);
            Assert.Equal("C", warning.GetExceptions()[1].Message);
            Assert.Equal("D", warning.GetExceptions()[2].Message);
            Assert.Equal($"A{Environment.NewLine}C{Environment.NewLine}D", warning.GetMessage());
        }

        /// <summary>
        /// ���Ի�ȡ�쳣�б�
        /// </summary>
        [Fact]
        public void TestGetExceptions_1()
        {
            var exception = new System.Exception("A");
            var list = Warning.GetExceptions(exception);
            Assert.Single(list);
            Assert.Equal("A", list[0].Message);
        }

        /// <summary>
        /// ���Ի�ȡ�쳣�б�
        /// </summary>
        [Fact]
        public void TestGetExceptions_2()
        {
            var exceptionB = new System.Exception("B");
            var exceptionA = new System.Exception("A", exceptionB);
            var list = Warning.GetExceptions(exceptionA);
            Assert.Equal(2, list.Count);
            Assert.Equal("A", list[0].Message);
            Assert.Equal("B", list[1].Message);
        }
    }
}