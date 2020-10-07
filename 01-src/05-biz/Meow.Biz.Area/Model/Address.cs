using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Meow.Biz.Area.Model
{
    /// <summary>
    /// 地址
    /// </summary>
    public class Address
    {
        /// <summary>
        /// 初始化地址
        /// </summary>
        /// <param name="provinceId">省份编号</param>
        /// <param name="cityId">城市编号</param>
        /// <param name="countyId">区县编号</param>
        /// <param name="townId">街道/乡镇编号</param>
        /// <param name="province">省份</param>
        /// <param name="city">城市</param>
        /// <param name="county">区县</param>
        /// <param name="town">街道/乡镇</param>
        /// <param name="zipCode">邮政编码</param>
        /// <param name="detail">详细地址</param>
        public Address(Guid? provinceId, Guid? cityId, Guid? countyId, Guid? townId, string province, string city, string county, string town, string zipCode, string detail = "")
        {
            ProvinceId = provinceId;
            CityId = cityId;
            CountyId = countyId;
            TownId = townId;
            Province = province;
            City = city;
            County = county;
            Town = town;
            ZipCode = zipCode;
            Detail = detail;
        }

        /// <summary>
        /// 省份编号
        /// </summary>
        [Column("ProvinceId")]
        [DisplayName("省份编号")]
        public Guid? ProvinceId { get; set; }
        /// <summary>
        /// 城市编号
        /// </summary>
        [Column("CityId")]
        [DisplayName("城市编号")]
        public Guid? CityId { get; set; }
        /// <summary>
        /// 区县编号
        /// </summary>
        [Column("CountyId")]
        [DisplayName("区县编号")]
        public Guid? CountyId { get; set; }
        /// <summary>
        /// 街道/乡镇编号
        /// </summary>
        [Column("TownId")]
        [DisplayName("街道/乡镇编号")]
        public Guid? TownId { get; set; }
        /// <summary>
        /// 省份
        /// </summary>
        [Column("Province")]
        [DisplayName("省份")]
        [StringLength(100, ErrorMessage = "省份输入过长，不能超过100位")]
        public string Province { get; set; }
        /// <summary>
        /// 城市
        /// </summary>
        [Column("City")]
        [DisplayName("城市")]
        [StringLength(100, ErrorMessage = "城市输入过长，不能超过100位")]
        public string City { get; set; }
        /// <summary>
        /// 区县
        /// </summary>
        [Column("County")]
        [DisplayName("区县")]
        [StringLength(100, ErrorMessage = "区县输入过长，不能超过100位")]
        public string County { get; set; }
        /// <summary>
        /// 街道/乡镇
        /// </summary>
        [Column("Town")]
        [DisplayName("街道/乡镇")]
        [StringLength(100, ErrorMessage = "街道/乡镇输入过长，不能超过100位")]
        public string Town { get; set; }
        /// <summary>
        /// 邮政编码
        /// </summary>
        [Column("ZipCode")]
        [DisplayName("邮政编码")]
        [StringLength(20, ErrorMessage = "邮政编码输入过长，不能超过20位")]
        public string ZipCode { get; set; }
        /// <summary>
        /// 详细地址：街道/门牌号
        /// </summary>
        [Column("Street")]
        [DisplayName("详细地址")]
        [StringLength(200, ErrorMessage = "详细地址输入过长，不能超过200位")]
        public string Detail { get; set; }

        /// <summary>
        /// 地址描述
        /// </summary>
        [DisplayName("地址")]
        public string Description => $"{Province}{City}{County}{Town}{Detail}";
        /// <summary>
        /// 空地址
        /// </summary>
        public static readonly Address Null = new Address(null, null, null, null, "", "", "", "", "", "");
    }
}
