using Meow.Reflection;
using Xunit;

namespace Meow.Test.Reflection
{
    /// <summary>
    /// ���Ͳ���������
    /// </summary>
    public class FinderTest
    {
        /// <summary>
        /// ���Ͳ�����
        /// </summary>
        private readonly IFind _finder;

        /// <summary>
        /// ��ʼ�����Ͳ���������
        /// </summary>
        public FinderTest()
        {
            _finder = new Finder();
        }

        /// <summary>
        /// �������ͼ���
        /// </summary>
        [Fact]
        public void TestFind()
        {
            var types = _finder.Find<IA>();
            Assert.Single(types);
            Assert.Equal(typeof(A), types[0]);
        }

        /// <summary>
        /// �������ͼ���
        /// </summary>
        [Fact]
        public void TestFind_2()
        {
            var types = _finder.Find<IB>();
            Assert.Equal(2, types.Count);
            Assert.Equal(typeof(A), types[0]);
            Assert.Equal(typeof(B), types[1]);
        }

        /// <summary>
        /// ���ҵĽ�����Ͱ�������
        /// </summary>
        [Fact]
        public void TestFind_3()
        {
            var types = _finder.Find<IC>();
            Assert.Equal(2, types.Count);
            Assert.Equal(typeof(B), types[0]);
            Assert.Equal(typeof(D<>), types[1]);
        }

        /// <summary>
        /// ���ҷ�������
        /// </summary>
        [Fact]
        public void TestFind_4()
        {
            var types = _finder.Find<IG<E>>();
            Assert.Single(types);
            Assert.Equal(typeof(E), types[0]);
        }

        /// <summary>
        /// ���ҷ�������
        /// </summary>
        [Fact]
        public void TestFind_5()
        {
            var types = _finder.Find(typeof(IG<>));
            Assert.Equal(2, types.Count);
            Assert.Equal(typeof(E), types[0]);
            Assert.Equal(typeof(F2<>), types[1]);
        }

        /// <summary>
        /// ���Բ���
        /// </summary>
        [Fact]
        public void TestFind_6()
        {
            Meow.Helper.Thread.ParallelExecute(() => {
                var types = _finder.Find<IA>();
                Assert.Single(types);
                Assert.Equal(typeof(A), types[0]);
            }, () => {
                var types = _finder.Find<IB>();
                Assert.Equal(2, types.Count);
                Assert.Equal(typeof(A), types[0]);
                Assert.Equal(typeof(B), types[1]);
            }, () => {
                var types = _finder.Find<IC>();
                Assert.Equal(2, types.Count);
                Assert.Equal(typeof(B), types[0]);
                Assert.Equal(typeof(D<>), types[1]);
            });
        }
    }

    /// <summary>
    /// ���Բ��ҽӿ�
    /// </summary>
    public interface IA
    {
    }

    /// <summary>
    /// ��������
    /// </summary>
    public class A : IA, IB
    {
    }

    /// <summary>
    /// ���Բ��ҽӿ�
    /// </summary>
    public interface IB
    {
    }

    /// <summary>
    /// ��������
    /// </summary>
    public class B : IB, IC
    {
    }

    /// <summary>
    /// ��������
    /// </summary>
    public abstract class C : IB, IC
    {
    }

    /// <summary>
    /// ���Բ��ҽӿ�
    /// </summary>
    public interface IC
    {
    }

    /// <summary>
    /// ��������
    /// </summary>
    public class D<T> : IC
    {
    }

    /// <summary>
    /// ���Բ��ҽӿ�
    /// </summary>
    public interface IG<T>
    {
    }

    /// <summary>
    /// ��������
    /// </summary>
    public class E : IG<E>
    {
    }

    /// <summary>
    /// ��������
    /// </summary>
    public class F2<T> : IG<T>
    {
    }
}