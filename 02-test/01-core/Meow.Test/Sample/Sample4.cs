namespace Meow.Test.Sample
{
    /// <summary>
    /// 测试样例4
    /// </summary>
    public class Sample4
    {
        /// <summary>
        /// A
        /// </summary>
        public string A { get; set; }
        /// <summary>
        /// B
        /// </summary>
        public string B { get; set; }
        /// <summary>
        /// E
        /// </summary>
        private string E { get; set; }
        /// <summary>
        /// F
        /// </summary>
        protected string F { get; set; }
        /// <summary>
        /// _c
        /// </summary>
#pragma warning disable 169
        private string _c;
#pragma warning restore 169
        /// <summary>
        /// _d
        /// </summary>
        protected string _d;
    }
}
