using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Meow.Biz.Area.Dto
{
    /// <summary>
    /// 地址传输对象
    /// </summary>
    public class AddressDto
    {
        /// <summary>
        /// 省份编号
        /// </summary>
        [DisplayName("省份编号")]
        public Guid? ProvinceId { get; set; }
        /// <summary>
        /// 城市编号
        /// </summary>
        [DisplayName("城市编号")]
        public Guid? CityId { get; set; }
        /// <summary>
        /// 区县编号
        /// </summary>
        [DisplayName("区县编号")]
        public Guid? CountyId { get; set; }
        /// <summary>
        /// 街道/乡镇编号
        /// </summary>
        [DisplayName("街道/乡镇编号")]
        public Guid? TownId { get; set; }
        /// <summary>
        /// 省份
        /// </summary>
        [DisplayName("省份")]
        [StringLength(100, ErrorMessage = "省份输入过长，不能超过100位")]
        public string Province { get; set; }
        /// <summary>
        /// 城市
        /// </summary>
        [DisplayName("城市")]
        [StringLength(100, ErrorMessage = "城市输入过长，不能超过100位")]
        public string City { get; set; }
        /// <summary>
        /// 区县
        /// </summary>
        [DisplayName("区县")]
        [StringLength(100, ErrorMessage = "区县输入过长，不能超过100位")]
        public string County { get; set; }
        /// <summary>
        /// 街道/乡镇
        /// </summary>
        [DisplayName("街道/乡镇")]
        [StringLength(100, ErrorMessage = "街道/乡镇输入过长，不能超过100位")]
        public string Town { get; set; }
        /// <summary>
        /// 邮政编码
        /// </summary>
        [DisplayName("邮政编码")]
        [StringLength(20, ErrorMessage = "邮政编码输入过长，不能超过20位")]
        public string ZipCode { get; set; }
        /// <summary>
        /// 详细地址：街道/门牌号
        /// </summary>
        [DisplayName("详细地址")]
        [StringLength(200, ErrorMessage = "详细地址输入过长，不能超过200位")]
        public string Detail { get; set; }

        /// <summary>
        /// 地址描述
        /// </summary>
        [DisplayName("地址")]
        public string Description => $"{Province}{City}{County}{Town}{Detail}";
    }
}