using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Meow.Extension.Helper;
using Meow.Http;
using Meow.Parameter.Enum;

namespace Meow.Test
{
    /// <summary>
    /// 测试
    /// </summary>
    public partial class Test : Form
    {
        /// <summary>
        /// 测试
        /// </summary>
        public Test()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            var value = GetObjct();
            var aa = value.AnalyzingToItems();

            var test = new List<int> { 1, 2 };
            var bb = test.Analyzing();
            var cc = test.AnalyzingToItems();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var value = GetObjct();
            var list = new List<TestObjct> { value, value };
            var aa = list.AnalyzingToItems();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private TestObjct GetObjct()
        {
            var value = new TestObjct
            {
                Name = "11",
                Value = new TestObjct
                {
                    Name = "21",
                    Value = new TestObjct
                    {
                        Name = "31",
                    }
                },
                Enum = Database.Oracle,
                List = new List<TestObjct>
                {
                    new TestObjct
                    {
                        Name = "List11",
                        List = new List<TestObjct>
                        {
                            new TestObjct
                            {
                                Name = "List12",
                            },
                            new TestObjct
                            {
                                Name = "List13",
                            }
                        }
                    },
                    new TestObjct
                    {
                        Name = "List21",
                        List = new List<TestObjct>
                        {
                            new TestObjct
                            {
                                Name = "List22",
                            },
                            new TestObjct
                            {
                                Name = "List23",
                            }
                        }
                    }
                },
                ListString = new List<string> { "A", "B" },
                ListArray = new string[] { "C", "D" },
                ListListString = new List<List<string>> { new List<string> { "A", "B" }, new List<string> { "C", "D" } },
                ArrayArrayString = new string[][] { new string[] { "A", "B" } },
            };
            return value;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var data = new HttpTestObjct
            {
                Id = "id123",
                Name = "name456"
            };
            var get = new HttpGet("http://localhost:32217/home/getTest").Data(data).Result();
            //var post = new HttpPost("http://localhost:32217/home/postTest").UrlData(data).Result();
            //var put = new HttpPut("http://localhost:32217/home/putTest").UrlData(data).Result();
            //var delete = new HttpDelete("http://localhost:32217/home/deleteTest").UrlData(data).Result();
        }
    }
}
