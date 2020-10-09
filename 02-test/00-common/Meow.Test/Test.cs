using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

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
            var value = new TestObjct
            {
                Name = "11",
                Value = new List<TestObjct>
                {
                    new TestObjct
                    {
                        Name = "21"
                    },
                    new TestObjct
                    {
                        Name = "22",
                    }
                }
            };
            var list = new List<TestObjct> { value };
            var aa = Meow.Helper.Reflection.Analyzing(value);
            var bb = Meow.Helper.Reflection.Analyzing(list);
        }
    }
}
