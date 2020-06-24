using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Meow.Tests.Mathematics
{
    /// <summary>
    /// 测试离散数学
    /// </summary>
    public class DiscreteTest
    {
        /// <summary>
        /// 测试组合
        /// </summary>
        [Fact]
        public static void TestCombination()
        {
            var a = "A";
            var b = "B";
            var c = "C";
            var d = "D";
            var aList = new List<string> { a };
            var aData = Meow.Mathematics.Discrete.Combination(aList);
            Assert.True(aData.Count() == 1);
            var bList = "AB";
            var bData = Meow.Mathematics.Discrete.Combination(bList);
            Assert.Equal(3, bData.Count());
            string[] cList = { a, b, c };
            var cData = Meow.Mathematics.Discrete.Combination(cList);
            Assert.Equal(7, cData.Count());
            var dList = new List<string> { a, b, c, d };
            var dData1 = Meow.Mathematics.Discrete.Combination(dList);
            Assert.Equal(15, dData1.Count());
            var dData2 = Meow.Mathematics.Discrete.Combination(dList, -1);
            Assert.False(dData2.Any());
            var dData3 = Meow.Mathematics.Discrete.Combination(dList, 1000000);
            Assert.False(dData3.Any());
            var dData4 = Meow.Mathematics.Discrete.Combination(dList, 2);
            Assert.Equal(6, dData4.Count());
            var dData5 = Meow.Mathematics.Discrete.Combination(dList, 3);
            Assert.Equal(4, dData5.Count());
            var dData6 = Meow.Mathematics.Discrete.Combination(dList, 4);
            Assert.True(dData6.Count() == 1);
        }

        /// <summary>
        /// 测试组合,组合项连接为带分隔符的字符串
        /// </summary>
        [Fact]
        public static void TestCombinationJoin()
        {
            var a = "A";
            var b = "B";
            var c = "C";
            var d = "D";
            var aList = new List<string> { a };
            var aData = Meow.Mathematics.Discrete.CombinationJoin(aList);
            Assert.True(aData.Count() == 1);
            Assert.Contains("A", aData);
            var bList = "AB";
            var bData = Meow.Mathematics.Discrete.CombinationJoin(bList, "", "、");
            Assert.Equal(3, bData.Count());
            Assert.Contains("A", bData);
            Assert.Contains("B", bData);
            Assert.Contains("A、B", bData);
            string[] cList = { a, b, c };
            var cData = Meow.Mathematics.Discrete.CombinationJoin(cList);
            Assert.Equal(7, cData.Count());
            Assert.Contains("A", cData);
            Assert.Contains("B", cData);
            Assert.Contains("C", cData);
            Assert.Contains("A,B", cData);
            Assert.Contains("A,C", cData);
            Assert.Contains("B,C", cData);
            Assert.Contains("A,B,C", cData);
            var dList = new List<string> { a, b, c, d };
            var dData1 = Meow.Mathematics.Discrete.CombinationJoin(dList, "\"", "、");
            Assert.Equal(15, dData1.Count());
            Assert.Contains("\"A\"", dData1);
            Assert.Contains("\"B\"", dData1);
            Assert.Contains("\"C\"", dData1);
            Assert.Contains("\"D\"", dData1);
            Assert.Contains("\"A\"、\"B\"", dData1);
            Assert.Contains("\"A\"、\"C\"", dData1);
            Assert.Contains("\"A\"、\"D\"", dData1);
            Assert.Contains("\"B\"、\"C\"", dData1);
            Assert.Contains("\"B\"、\"D\"", dData1);
            Assert.Contains("\"C\"、\"D\"", dData1);
            Assert.Contains("\"A\"、\"B\"、\"C\"", dData1);
            Assert.Contains("\"A\"、\"B\"、\"D\"", dData1);
            Assert.Contains("\"A\"、\"C\"、\"D\"", dData1);
            Assert.Contains("\"B\"、\"C\"、\"D\"", dData1);
            Assert.Contains("\"A\"、\"B\"、\"C\"、\"D\"", dData1);
            var dData2 = Meow.Mathematics.Discrete.CombinationJoin(dList, -1);
            Assert.False(dData2.Any());
            var dData3 = Meow.Mathematics.Discrete.CombinationJoin(dList, 1000000);
            Assert.False(dData3.Any());
            var dData4 = Meow.Mathematics.Discrete.CombinationJoin(dList, 2, "", "、");
            Assert.Equal(6, dData4.Count());
            Assert.Contains("A、B", dData4);
            Assert.Contains("A、C", dData4);
            Assert.Contains("A、D", dData4);
            Assert.Contains("B、C", dData4);
            Assert.Contains("B、D", dData4);
            Assert.Contains("C、D", dData4);
            var dData5 = Meow.Mathematics.Discrete.CombinationJoin(dList, 3, "\"", "、");
            Assert.Equal(4, dData5.Count());
            Assert.Contains("\"A\"、\"B\"、\"C\"", dData5);
            Assert.Contains("\"A\"、\"B\"、\"D\"", dData5);
            Assert.Contains("\"A\"、\"C\"、\"D\"", dData5);
            Assert.Contains("\"B\"、\"C\"、\"D\"", dData5);
            var dData6 = Meow.Mathematics.Discrete.CombinationJoin(dList, 4);
            Assert.True(dData6.Count() == 1);
            Assert.Contains("A,B,C,D", dData6);
        }

        /// <summary>
        /// 测试排列
        /// </summary>
        [Fact]
        public static void TestPermutation()
        {
            var a = "A";
            var b = "B";
            var c = "C";
            var d = "D";
            var aList = new List<string> { a };
            var aData = Meow.Mathematics.Discrete.Permutation(aList);
            Assert.True(aData.Count() == 1);
            var bList = "AB";
            var bData = Meow.Mathematics.Discrete.Permutation(bList);
            Assert.Equal(2, bData.Count());
            string[] cList = { a, b, c };
            var cData = Meow.Mathematics.Discrete.Permutation(cList);
            Assert.Equal(6, cData.Count());
            var dList = new List<string> { a, b, c, d };
            var dData1 = Meow.Mathematics.Discrete.Permutation(dList);
            Assert.Equal(24, dData1.Count());
            var dData2 = Meow.Mathematics.Discrete.Permutation(dList, 1, 2);
            Assert.Equal(2, dData2.Count());
            var dData3 = Meow.Mathematics.Discrete.Permutation(dList, 1, 3);
            Assert.Equal(6, dData3.Count());
        }

        /// <summary>
        /// 测试排列,排列项连接为带分隔符的字符串
        /// </summary>
        [Fact]
        public static void TestPermutationJoin()
        {
            var a = "A";
            var b = "B";
            var c = "C";
            var d = "D";
            var aList = new List<string> { a };
            var aData = Meow.Mathematics.Discrete.PermutationJoin(aList);
            Assert.True(aData.Count() == 1);
            Assert.Contains("A", aData);
            string[] bList = { a, b };
            var bData = Meow.Mathematics.Discrete.PermutationJoin(bList, "", "、");
            Assert.Equal(2, bData.Count());
            Assert.Contains("A、B", bData);
            Assert.Contains("B、A", bData);
            string[] cList = { a, b, c };
            var cData = Meow.Mathematics.Discrete.PermutationJoin(cList);
            Assert.Equal(6, cData.Count());
            Assert.Contains("A,B,C", cData);
            Assert.Contains("B,A,C", cData);
            Assert.Contains("C,B,A", cData);
            var dList = new List<string> { a, b, c, d };
            var dData1 = Meow.Mathematics.Discrete.PermutationJoin(dList, "\"", "、");
            Assert.Equal(24, dData1.Count());
            Assert.Contains("\"A\"、\"B\"、\"C\"、\"D\"", dData1);
            Assert.Contains("\"B\"、\"A\"、\"C\"、\"D\"", dData1);
            Assert.Contains("\"C\"、\"B\"、\"A\"、\"D\"", dData1);
            Assert.Contains("\"D\"、\"C\"、\"B\"、\"A\"", dData1);
            Assert.Contains("\"B\"、\"D\"、\"C\"、\"A\"", dData1);
            Assert.Contains("\"D\"、\"B\"、\"C\"、\"A\"", dData1);
            Assert.Contains("\"A\"、\"D\"、\"C\"、\"B\"", dData1);
            var dData2 = Meow.Mathematics.Discrete.PermutationJoin(dList, 1, 2, "", "、");
            Assert.Equal(2, dData2.Count());
            Assert.Contains("A、B、C、D", dData2);
            Assert.Contains("A、C、B、D", dData2);
            var dData3 = Meow.Mathematics.Discrete.PermutationJoin(dList, 1, 3, "\"", "、");
            Assert.Equal(6, dData3.Count());
            Assert.Contains("\"A\"、\"B\"、\"C\"、\"D\"", dData3);
            Assert.Contains("\"A\"、\"B\"、\"D\"、\"C\"", dData3);
            Assert.Contains("\"A\"、\"C\"、\"B\"、\"D\"", dData3);
            Assert.Contains("\"A\"、\"C\"、\"D\"、\"B\"", dData3);
            Assert.Contains("\"A\"、\"D\"、\"B\"、\"C\"", dData3);
            Assert.Contains("\"A\"、\"D\"、\"C\"、\"B\"", dData3);
        }

        /// <summary>
        /// 测试排列组合
        /// </summary>
        [Fact]
        public static void TestPermutationCombination()
        {
            var a = "A";
            var b = "B";
            var c = "C";
            var d = "D";
            var aList = new List<string> { a };
            var aData = Meow.Mathematics.Discrete.PermutationCombination(aList);
            Assert.True(aData.Count() == 1);
            var bList = "AB";
            var bData = Meow.Mathematics.Discrete.PermutationCombination(bList);
            Assert.Equal(4, bData.Count());
            string[] cList = { a, b, c };
            var cData = Meow.Mathematics.Discrete.PermutationCombination(cList);
            Assert.Equal(15, cData.Count());
            var dList = new List<string> { a, b, c, d };
            var dData1 = Meow.Mathematics.Discrete.PermutationCombination(dList);
            Assert.Equal(64, dData1.Count());
            var dData2 = Meow.Mathematics.Discrete.PermutationCombination(dList, -1);
            Assert.False(dData2.Any());
            var dData3 = Meow.Mathematics.Discrete.PermutationCombination(dList, 1000000);
            Assert.False(dData3.Any());
            var dData4 = Meow.Mathematics.Discrete.PermutationCombination(dList, 2);
            Assert.Equal(12, dData4.Count());
            var dData5 = Meow.Mathematics.Discrete.PermutationCombination(dList, 3);
            Assert.Equal(24, dData5.Count());
            var dData6 = Meow.Mathematics.Discrete.PermutationCombination(dList, 4);
            Assert.Equal(24, dData6.Count());
        }

        /// <summary>
        /// 测试排列组合,组合排列项连接为带分隔符的字符串
        /// </summary>
        [Fact]
        public static void TestPermutationCombinationJoin()
        {
            var a = "A";
            var b = "B";
            var c = "C";
            var d = "D";
            var aList = new List<string> { a };
            var aData = Meow.Mathematics.Discrete.PermutationCombinationJoin(aList);
            Assert.True(aData.Count() == 1);
            Assert.Contains("A", aData);
            var bList = "AB";
            var bData = Meow.Mathematics.Discrete.PermutationCombinationJoin(bList, "", "、");
            Assert.Equal(4, bData.Count());
            Assert.Contains("A", bData);
            Assert.Contains("B", bData);
            Assert.Contains("A、B", bData);
            Assert.Contains("B、A", bData);
            string[] cList = { a, b, c };
            var cData = Meow.Mathematics.Discrete.PermutationCombinationJoin(cList);
            Assert.Equal(15, cData.Count());
            Assert.Contains("A", cData);
            Assert.Contains("B", cData);
            Assert.Contains("C", cData);
            Assert.Contains("A,B", cData);
            Assert.Contains("B,A", cData);
            Assert.Contains("A,C", cData);
            Assert.Contains("B,C", cData);
            Assert.Contains("C,B", cData);
            Assert.Contains("A,B,C", cData);
            Assert.Contains("A,C,B", cData);
            Assert.Contains("B,A,C", cData);
            Assert.Contains("C,B,A", cData);
            var dList = new List<string> { a, b, c, d };
            var dData1 = Meow.Mathematics.Discrete.PermutationCombinationJoin(dList, "\"", "、");
            Assert.Equal(64, dData1.Count());
            Assert.Contains("\"A\"", dData1);
            Assert.Contains("\"B\"", dData1);
            Assert.Contains("\"C\"", dData1);
            Assert.Contains("\"D\"", dData1);
            Assert.Contains("\"A\"、\"B\"", dData1);
            Assert.Contains("\"A\"、\"C\"", dData1);
            Assert.Contains("\"C\"、\"A\"", dData1);
            Assert.Contains("\"A\"、\"D\"", dData1);
            Assert.Contains("\"B\"、\"C\"", dData1);
            Assert.Contains("\"B\"、\"D\"", dData1);
            Assert.Contains("\"D\"、\"B\"", dData1);
            Assert.Contains("\"C\"、\"D\"", dData1);
            Assert.Contains("\"D\"、\"C\"", dData1);
            Assert.Contains("\"A\"、\"B\"、\"C\"", dData1);
            Assert.Contains("\"A\"、\"C\"、\"B\"", dData1);
            Assert.Contains("\"A\"、\"B\"、\"D\"", dData1);
            Assert.Contains("\"A\"、\"C\"、\"D\"", dData1);
            Assert.Contains("\"A\"、\"D\"、\"C\"", dData1);
            Assert.Contains("\"B\"、\"D\"、\"C\"", dData1);
            Assert.Contains("\"B\"、\"C\"、\"D\"", dData1);
            Assert.Contains("\"C\"、\"B\"、\"D\"", dData1);
            Assert.Contains("\"C\"、\"A\"、\"D\"", dData1);
            Assert.Contains("\"D\"、\"B\"、\"A\"", dData1);
            Assert.Contains("\"D\"、\"C\"、\"A\"", dData1);
            Assert.Contains("\"A\"、\"B\"、\"C\"、\"D\"", dData1);
            Assert.Contains("\"A\"、\"B\"、\"D\"、\"C\"", dData1);
            Assert.Contains("\"B\"、\"A\"、\"C\"、\"D\"", dData1);
            Assert.Contains("\"B\"、\"C\"、\"A\"、\"D\"", dData1);
            Assert.Contains("\"C\"、\"B\"、\"A\"、\"D\"", dData1);
            Assert.Contains("\"C\"、\"B\"、\"D\"、\"A\"", dData1);
            Assert.Contains("\"D\"、\"A\"、\"C\"、\"B\"", dData1);
            Assert.Contains("\"D\"、\"B\"、\"C\"、\"A\"", dData1);
            var dData2 = Meow.Mathematics.Discrete.PermutationCombinationJoin(dList, -1);
            Assert.False(dData2.Any());
            var dData3 = Meow.Mathematics.Discrete.PermutationCombinationJoin(dList, 1000000);
            Assert.False(dData3.Any());
            var dData4 = Meow.Mathematics.Discrete.PermutationCombinationJoin(dList, 2, "", "、");
            Assert.Equal(12, dData4.Count());
            Assert.Contains("A、B", dData4);
            Assert.Contains("A、C", dData4);
            Assert.Contains("A、D", dData4);
            Assert.Contains("B、A", dData4);
            Assert.Contains("B、C", dData4);
            Assert.Contains("B、D", dData4);
            Assert.Contains("C、A", dData4);
            Assert.Contains("C、B", dData4);
            Assert.Contains("C、D", dData4);
            Assert.Contains("D、A", dData4);
            Assert.Contains("D、B", dData4);
            Assert.Contains("D、C", dData4);
            var dData5 = Meow.Mathematics.Discrete.PermutationCombinationJoin(dList, 3, "\"", "、");
            Assert.Equal(24, dData5.Count());
            Assert.Contains("\"A\"、\"B\"、\"C\"", dData5);
            Assert.Contains("\"A\"、\"B\"、\"D\"", dData5);
            Assert.Contains("\"A\"、\"C\"、\"D\"", dData5);
            Assert.Contains("\"B\"、\"C\"、\"D\"", dData5);
            Assert.Contains("\"B\"、\"D\"、\"A\"", dData5);
            Assert.Contains("\"B\"、\"D\"、\"C\"", dData5);
            Assert.Contains("\"C\"、\"A\"、\"D\"", dData5);
            Assert.Contains("\"C\"、\"D\"、\"A\"", dData5);
            Assert.Contains("\"C\"、\"D\"、\"B\"", dData5);
            Assert.Contains("\"D\"、\"C\"、\"B\"", dData5);
            Assert.Contains("\"D\"、\"B\"、\"A\"", dData5);
            Assert.Contains("\"D\"、\"A\"、\"C\"", dData5);
            var dData6 = Meow.Mathematics.Discrete.PermutationCombinationJoin(dList, 4);
            Assert.Equal(24, dData6.Count());
            Assert.Contains("A,B,C,D", dData6);
            Assert.Contains("A,B,D,C", dData6);
            Assert.Contains("B,A,C,D", dData6);
            Assert.Contains("B,C,A,D", dData6);
            Assert.Contains("C,B,A,D", dData6);
            Assert.Contains("C,B,D,A", dData6);
            Assert.Contains("D,A,C,B", dData6);
            Assert.Contains("D,B,C,A", dData6);
        }
    }
}
