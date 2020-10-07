using System.Linq;
using System.Collections.Generic;
using Meow.Biz.Area.Model;

namespace Meow.Biz.Area.Dto.Extension
{
    /// <summary>
    /// 地址数据传输对象扩展
    /// </summary>
    public static class AddressDtoExtension
    {
        /// <summary>
        /// 转换为地址数据传输对象
        /// </summary>
        /// <param name="address">地址</param>
        public static AddressDto ToDto(this Address address)
        {
            if (address == null)
                return new AddressDto();
            return new AddressDto
            {
                ProvinceId = address.ProvinceId,
                CityId = address.CityId,
                CountyId = address.CountyId,
                TownId = address.TownId,
                Province = address.Province,
                City = address.City,
                County = address.County,
                Town = address.Town,
                ZipCode = address.ZipCode,
                Detail = address.Detail,
            };
        }

        /// <summary>
        /// 转换为地址数据传输对象
        /// </summary>
        /// <param name="address">地址集合</param>
        public static List<AddressDto> ToDto(this List<Address> address)
        {
            if (address == null)
                return new List<AddressDto>();
            return address.Select(t => t.ToDto()).ToList();
        }

        /// <summary>
        /// 转换为地址对象
        /// </summary>
        /// <param name="dto">地区数据传输对象</param>
        public static Address ToAddress(this AddressDto dto)
        {
            if (dto == null)
                return Address.Null;
            return new Address(dto.ProvinceId
                             , dto.CityId
                             , dto.CountyId
                             , dto.TownId
                             , dto.Province
                             , dto.City
                             , dto.County
                             , dto.Town
                             , dto.ZipCode
                             , dto.Detail);
        }

        /// <summary>
        /// 转换为地址对象
        /// </summary>
        /// <param name="dto">地址数据传输对象集合</param>
        public static List<Address> ToAddress(this List<AddressDto> dto)
        {
            if (dto == null)
                return new List<Address>();
            return dto.Select(t => t.ToAddress()).ToList();
        }
    }
}