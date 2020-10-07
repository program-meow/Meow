using System;
using System.ComponentModel;
using Meow.Extension.Helper;
using Meow.Parameter.Enum;

namespace Meow.Biz.Area.Dto
{
    /// <summary>
    /// 地区传输对象
    /// </summary>
    public class AreaDto
    {
        /// <summary>
        /// 编号
        /// </summary>
        [DisplayName("编号")]
        public Guid Id { get; set; }
        /// <summary>
        /// 行政区划代码
        /// </summary>
        [DisplayName("行政区划代码")]
        public string RegionalismCode { get; set; }
        /// <summary>
        /// 地理区
        /// </summary>
        [DisplayName("地理区")]
        public GeographicalRegion? GeographicalRegion { get; set; }
        /// <summary>
        /// 地理区说明
        /// </summary>
        [DisplayName("地理区")]
        public string GeographicalRegionDescription => GeographicalRegion.Description();
        /// <summary>
        /// 经济区
        /// </summary>
        [DisplayName("经济区")]
        public EconomicRegion? EconomicRegion { get; set; }
        /// <summary>
        /// 经济区说明
        /// </summary>
        [DisplayName("经济区")]
        public string EconomicRegionDescription => EconomicRegion.Description();
        /// <summary>
        /// 全称
        /// </summary>
        [DisplayName("全称")]
        public string Name { get; set; }
        /// <summary>
        /// 简称
        /// </summary>
        [DisplayName("简称")]
        public string ShortName { get; set; }
        /// <summary>
        /// 拼音
        /// </summary>
        [DisplayName("拼音")]
        public string Pinyin { get; set; }
        /// <summary>
        /// 简拼
        /// </summary>
        [DisplayName("简拼")]
        public string ShortPinyin { get; set; }
        /// <summary>
        /// 首拼
        /// </summary>
        [DisplayName("首拼")]
        public string FirstPinyin { get; set; }
        /// <summary>
        /// 区号
        /// </summary>
        [DisplayName("区号")]
        public string AreaCode { get; set; }
        /// <summary>
        /// 邮政编码
        /// </summary>
        [DisplayName("邮政编码")]
        public string ZipCode { get; set; }
        /// <summary>
        /// 经度
        /// </summary>
        [DisplayName("经度")]
        public decimal? Longitude { get; set; }
        /// <summary>
        /// 纬度
        /// </summary>
        [DisplayName("纬度")]
        public decimal? Latitude { get; set; }
        /// <summary>
        /// 是否行政中心
        /// </summary>
        [DisplayName("是否行政中心")]
        public bool? IsAdministrativeCenter { get; set; }
        /// <summary>
        /// 父编号
        /// </summary>
        [DisplayName("父编号")]
        public Guid? ParentId { get; set; }
        /// <summary>
        /// 级别
        /// </summary>
        [DisplayName("级别")]
        public AreaLevel? Level { get; set; }
        /// <summary>
        /// 级别说明
        /// </summary>
        [DisplayName("级别")]
        public string LevelDescription => Level.Description();
        /// <summary>
        /// 排序号
        /// </summary>
        [DisplayName("排序号")]
        public int? SortId { get; set; }
    }
}