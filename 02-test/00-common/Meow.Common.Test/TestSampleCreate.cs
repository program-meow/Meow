using System;
using System.Collections.Generic;
using Meow.Common.Test.Sample;

namespace Meow.Common.Test
{
    /// <summary>
    /// 测试样例创建
    /// </summary>
    public class TestSampleCreate
    {
        /// <summary>
        /// 获取案例对象集合
        /// </summary>
        public static List<Sample.Sample> GetList()
        {
            var guidValue1 = new Guid("EAB523C6-2FE7-47BE-89D5-C6D440C3033A");
            var guidValue2 = new Guid("83B0233C-A24F-49FD-8083-1337209EBC9A");
            var testValue1 = "小明";
            var testValue2 = "小王";
            var testValue3 = "小李";
            var numberValue1 = 1;
            var numberValue2 = 2;
            var numberValue3 = 3;
            var numberValue4 = 4;
            var timeValue1 = new DateTime(2000, 1, 1, 1, 1, 1);
            var timeValue2 = new DateTime(2001, 1, 1, 1, 1, 1);
            var timeValue3 = new DateTime(2002, 1, 1, 1, 1, 1);
            var timeValue4 = new DateTime(2003, 1, 1, 1, 1, 1);
            var enumValue = SampleEnum.A;
            return new List<Sample.Sample>
            {
                new Sample.Sample
                {
                    GuidValue = guidValue1,
                    TestValue = testValue1,
                    IntValue = numberValue1,
                    NullableIntValue = numberValue1,
                    EnumValue = enumValue,
                    DecimalValue = numberValue1,
                    NullableDecimalValue = numberValue1,
                    FloatValue = numberValue1,
                    NullableFloatValue = numberValue1,
                    DoubleValue = numberValue1,
                    NullableDoubleValue = numberValue1,
                    DateValue = timeValue1,
                    NullableDateValue = timeValue1,
                },
                new Sample.Sample
                {
                    GuidValue = guidValue2,
                    TestValue = testValue2,
                    IntValue = numberValue2,
                    NullableIntValue = numberValue2,
                    EnumValue = enumValue,
                    DecimalValue = numberValue2,
                    NullableDecimalValue = numberValue2,
                    FloatValue = numberValue2,
                    NullableFloatValue = numberValue2,
                    DoubleValue = numberValue2,
                    NullableDoubleValue = numberValue2,
                    DateValue = timeValue2,
                    NullableDateValue = timeValue2,
                },
                new Sample.Sample
                {
                    GuidValue = guidValue1,
                    TestValue = testValue3,
                    IntValue = numberValue3,
                    NullableIntValue = numberValue3,
                    EnumValue = enumValue,
                    DecimalValue = numberValue3,
                    NullableDecimalValue = numberValue3,
                    FloatValue = numberValue3,
                    NullableFloatValue = numberValue3,
                    DoubleValue = numberValue3,
                    NullableDoubleValue = numberValue3,
                    DateValue = timeValue3,
                    NullableDateValue = timeValue3,
                },
                new Sample.Sample
                {
                    GuidValue = guidValue2,
                    TestValue = testValue1,
                    IntValue = numberValue4,
                    NullableIntValue = numberValue4,
                    EnumValue = enumValue,
                    DecimalValue = numberValue4,
                    NullableDecimalValue = numberValue4,
                    FloatValue = numberValue4,
                    NullableFloatValue = numberValue4,
                    DoubleValue = numberValue4,
                    NullableDoubleValue = numberValue4,
                    DateValue = timeValue4,
                    NullableDateValue = timeValue4,
                },
                new Sample.Sample
                {
                    GuidValue = guidValue1,
                    TestValue = testValue2,
                    IntValue = 0,
                    NullableIntValue = null,
                    EnumValue = enumValue,
                    DecimalValue = 0,
                    NullableDecimalValue = null,
                    FloatValue = 0,
                    NullableFloatValue = null,
                    DoubleValue = 0,
                    NullableDoubleValue = null,
                    DateValue = DateTime.MinValue,
                    NullableDateValue = null,
                },
            };
        }
    }
}
