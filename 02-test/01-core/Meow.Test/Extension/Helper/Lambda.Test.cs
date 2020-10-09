using System;
using System.Linq.Expressions;
using Meow.Common.Test.Sample;
using Meow.Extension.Helper;
using Xunit;
using MicrosoftExpression = System.Linq.Expressions.Expression;

namespace Meow.Test.Extension.Helper
{
    /// <summary>
    /// ����Lambda���ʽ��չ
    /// </summary>
    public class LambdaTest
    {
        //    /// <summary>
        //    /// ���Գ�ʼ��
        //    /// </summary>
        //    public LambdaTest()
        //    {
        //        _parameterExpression = MicrosoftExpression.Parameter(typeof(Sample), "t");
        //        _expression1 = _parameterExpression.Property("StringValue").Call("Contains", MicrosoftExpression.Constant("A"));
        //        _expression2 = _parameterExpression.Property("NullableDateValue")
        //                .Property("Value")
        //                .Property("Year")
        //                .Greater(MicrosoftExpression.Constant(2000));
        //    }

        //    /// <summary>
        //    /// �������ʽ
        //    /// </summary>
        //    private readonly ParameterExpression _parameterExpression;

        //    /// <summary>
        //    /// ���ʽ1
        //    /// </summary>
        //    private MicrosoftExpression _expression1;

        //    /// <summary>
        //    /// ���ʽ2
        //    /// </summary>
        //    private MicrosoftExpression _expression2;

        //    /// <summary>
        //    /// ����And����
        //    /// </summary>
        //    [Fact]
        //    public void TestAnd()
        //    {
        //        var andExpression = _expression1.And(_expression2).ToLambda<Func<Sample, bool>>(_parameterExpression);
        //        Expression<Func<Sample, bool>> expected = t => t.StringValue.Contains("A") && t.NullableDateValue.Value.Year > 2000;
        //        Assert.Equal(expected.ToString(), andExpression.ToString());

        //        Expression<Func<Sample, bool>> left = t => t.StringValue == "A";
        //        Expression<Func<Sample, bool>> right = t => t.StringValue == "B";
        //        expected = t => t.StringValue == "A" && t.StringValue == "B";
        //        Assert.Equal(expected.ToString(), left.And(right).ToString());
        //    }

        //    /// <summary>
        //    /// ����Or����
        //    /// </summary>
        //    [Fact]
        //    public void TestOr()
        //    {
        //        var andExpression = _expression1.Or(_expression2).ToLambda<Func<Sample, bool>>(_parameterExpression);
        //        Expression<Func<Sample, bool>> expected = t => t.StringValue.Contains("A") || t.NullableDateValue.Value.Year > 2000;
        //        Assert.Equal(expected.ToString(), andExpression.ToString());
        //    }

        //    /// <summary>
        //    /// ����Or���� - ����ʽΪ��
        //    /// </summary>
        //    [Fact]
        //    public void TestOr_LeftIsNull()
        //    {
        //        _expression1 = null;
        //        var andExpression = _expression1.Or(_expression2).ToLambda<Func<Sample, bool>>(_parameterExpression);
        //        Expression<Func<Sample, bool>> expected = t => t.NullableDateValue.Value.Year > 2000;
        //        Assert.Equal(expected.ToString(), andExpression.ToString());
        //    }

        //    /// <summary>
        //    /// ����Or���� - �ұ��ʽΪ��
        //    /// </summary>
        //    [Fact]
        //    public void TestOr_RightIsNull()
        //    {
        //        _expression2 = null;
        //        var andExpression = _expression1.Or(_expression2).ToLambda<Func<Sample, bool>>(_parameterExpression);
        //        Expression<Func<Sample, bool>> expected = t => t.StringValue.Contains("A");
        //        Assert.Equal(expected.ToString(), andExpression.ToString());
        //    }

        //    /// <summary>
        //    /// ����Or����
        //    /// </summary>
        //    [Fact]
        //    public void TestOr_2()
        //    {
        //        Expression<Func<Sample, bool>> left = t => t.StringValue == "A";
        //        Expression<Func<Sample, bool>> right = t => t.StringValue == "B";
        //        Expression<Func<Sample, bool>> expected = t => t.StringValue == "A" || t.StringValue == "B";
        //        Assert.Equal(expected.ToString(), left.Or(right).ToString());
        //    }

        //    /// <summary>
        //    /// ����Or���� - ����ʽΪ��
        //    /// </summary>
        //    [Fact]
        //    public void TestOr_2_LeftIsNull()
        //    {
        //        Expression<Func<Sample, bool>> left = null;
        //        Expression<Func<Sample, bool>> right = t => t.StringValue == "B";
        //        Expression<Func<Sample, bool>> expected = t => t.StringValue == "B";
        //        Assert.Equal(expected.ToString(), left.Or(right).ToString());
        //    }

        //    /// <summary>
        //    /// ����Or���� - �ұ��ʽΪ��
        //    /// </summary>
        //    [Fact]
        //    public void TestOr_2_RightIsNull()
        //    {
        //        Expression<Func<Sample, bool>> left = t => t.StringValue == "A";
        //        Expression<Func<Sample, bool>> right = null;
        //        Expression<Func<Sample, bool>> expected = t => t.StringValue == "A";
        //        Assert.Equal(expected.ToString(), left.Or(right).ToString());
        //    }

        //    /// <summary>
        //    /// ��ȡlambda���ʽ��Աֵ
        //    /// </summary>
        //    [Fact]
        //    public void TestValue_LambdaExpression()
        //    {
        //        Expression<Func<Sample, bool>> expression = test => test.StringValue == "A";
        //        Assert.Equal("A", expression.Value());
        //    }

        //    /// <summary>
        //    /// �������
        //    /// </summary>
        //    [Fact]
        //    public void TestEqual()
        //    {
        //        _expression1 = _parameterExpression.Property("IntValue").Equal(1);
        //        Assert.Equal("t => (t.IntValue == 1)",
        //            _expression1.ToLambda<Func<Sample, bool>>(_parameterExpression).ToString());
        //    }

        //    /// <summary>
        //    /// ���Բ����
        //    /// </summary>
        //    [Fact]
        //    public void TestNotEqual()
        //    {
        //        _expression1 = _parameterExpression.Property("NullableIntValue").NotEqual(1);
        //        Assert.Equal("t => (t.NullableIntValue != 1)",
        //            _expression1.ToLambda<Func<Sample, bool>>(_parameterExpression).ToString());
        //    }

        //    /// <summary>
        //    /// ���Դ���
        //    /// </summary>
        //    [Fact]
        //    public void TestGreater()
        //    {
        //        _expression1 = _parameterExpression.Property("NullableIntValue").Greater(1);
        //        Assert.Equal("t => (t.NullableIntValue > 1)",
        //            _expression1.ToLambda<Func<Sample, bool>>(_parameterExpression).ToString());
        //    }

        //    /// <summary>
        //    /// ���Դ��ڵ���
        //    /// </summary>
        //    [Fact]
        //    public void TestGreaterEqual_Nullable()
        //    {
        //        _expression1 = _parameterExpression.Property("NullableIntValue").GreaterEqual(1);
        //        Assert.Equal("t => (t.NullableIntValue >= 1)",
        //            _expression1.ToLambda<Func<Sample, bool>>(_parameterExpression).ToString());
        //    }

        //    /// <summary>
        //    /// ����С��
        //    /// </summary>
        //    [Fact]
        //    public void TestLess()
        //    {
        //        _expression1 = _parameterExpression.Property("NullableIntValue").Less(1);
        //        Assert.Equal("t => (t.NullableIntValue < 1)",
        //            _expression1.ToLambda<Func<Sample, bool>>(_parameterExpression).ToString());
        //    }

        //    /// <summary>
        //    /// ����С�ڵ���
        //    /// </summary>
        //    [Fact]
        //    public void TestLessEqual()
        //    {
        //        _expression1 = _parameterExpression.Property("NullableIntValue").LessEqual(1);
        //        Assert.Equal("t => (t.NullableIntValue <= 1)",
        //            _expression1.ToLambda<Func<Sample, bool>>(_parameterExpression).ToString());
        //    }

        //    /// <summary>
        //    /// ����ͷƥ��
        //    /// </summary>
        //    [Fact]
        //    public void TestStartsWith()
        //    {
        //        _expression1 = _parameterExpression.Property("StringValue").StartsWith("a");
        //        Assert.Equal("t => t.StringValue.StartsWith(\"a\")",
        //            _expression1.ToLambda<Func<Sample, bool>>(_parameterExpression).ToString());
        //    }

        //    /// <summary>
        //    /// ����βƥ��
        //    /// </summary>
        //    [Fact]
        //    public void TestEndsWith()
        //    {
        //        _expression1 = _parameterExpression.Property("StringValue").EndsWith("a");
        //        Assert.Equal("t => t.StringValue.EndsWith(\"a\")",
        //            _expression1.ToLambda<Func<Sample, bool>>(_parameterExpression).ToString());
        //    }

        //    /// <summary>
        //    /// ����ģ��ƥ��
        //    /// </summary>
        //    [Fact]
        //    public void TestContains()
        //    {
        //        _expression1 = _parameterExpression.Property("StringValue").Contains("a");
        //        Assert.Equal("t => t.StringValue.Contains(\"a\")",
        //            _expression1.ToLambda<Func<Sample, bool>>(_parameterExpression).ToString());
        //    }
    }
}
