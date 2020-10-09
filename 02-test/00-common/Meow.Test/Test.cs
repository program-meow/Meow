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
                Age = 2,
                Qian = 5,
            };
            var bb = Meow.Helper.Reflection.AnalyzingObject(value);
        }
    }
}
