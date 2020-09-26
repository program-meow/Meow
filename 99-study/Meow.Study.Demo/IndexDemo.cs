using System;
using System.Windows.Forms;
using Meow.Study.Demo.Helper;

namespace Meow.Study.Demo
{
    /// <summary>
    /// Demo首页
    /// </summary>
    public partial class IndexDemo : Form
    {
        /// <summary>
        /// 初始化
        /// </summary>
        public IndexDemo()
        {
            InitializeComponent();
        }

        private void IndexDemo_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Email按钮
        /// </summary>
        private void EmailButton_Click(object sender, EventArgs e)
        {
            var email = new EmailDemo();
            email.ShowDialog();
        }
    }
}
