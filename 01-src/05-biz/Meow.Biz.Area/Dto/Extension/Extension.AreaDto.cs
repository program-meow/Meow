using System.Linq;
using System.Collections.Generic;
using Meow.Extension.Parameter.Object;
using Meow.Parameter.Object;

namespace Meow.Biz.Area.Dto.Extension
{
    /// <summary>
    /// 地区数据传输对象扩展
    /// </summary>
    public static class AreaDtoExtension
    {
        /// <summary>
        /// 转换为地区数据传输对象
        /// </summary>
        /// <param name="area">地区</param>
        public static AreaDto ToDto(this Model.Area area)
        {
            if (area == null)
                return new AreaDto();
            return new AreaDto
            {
                Id = area.Id,
                RegionalismCode = area.RegionalismCode,
                GeographicalRegion = area.GeographicalRegion,
                EconomicRegion = area.EconomicRegion,
                Name = area.Name,
                ShortName = area.ShortName,
                Pinyin = area.Pinyin,
                ShortPinyin = area.ShortPinyin,
                FirstPinyin = area.FirstPinyin,
                AreaCode = area.AreaCode,
                ZipCode = area.ZipCode,
                Longitude = area.Longitude,
                Latitude = area.Latitude,
                IsAdministrativeCenter = area.IsAdministrativeCenter,
                ParentId = area.ParentId,
                Level = area.Level,
                SortId = area.SortId,
            };
        }

        /// <summary>
        /// 转换为地区数据传输对象
        /// </summary>
        /// <param name="areas">地区集合</param>
        public static List<AreaDto> ToDto(this List<Model.Area> areas)
        {
            if (areas == null)
                return new List<AreaDto>();
            return areas.Select(t => t.ToDto()).ToList();
        }

        /// <summary>
        /// 转换为地区数据传输对象
        /// </summary>
        /// <param name="area">地区</param>
        public static Tree<AreaDto> ToDto(this Tree<Model.Area> area)
        {
            if (area == null)
                return new Tree<AreaDto>();
            return area.To(ToDto);
        }

        /// <summary>
        /// 转换为地区数据传输对象
        /// </summary>
        /// <param name="areas">地区集合</param>
        public static List<Tree<AreaDto>> ToDto(this List<Tree<Model.Area>> areas)
        {
            if (areas == null)
                return new List<Tree<AreaDto>>();
            return areas.To(ToDto);
        }
    }
}