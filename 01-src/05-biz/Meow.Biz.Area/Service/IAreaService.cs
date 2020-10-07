using System;
using System.Collections.Generic;
using Meow.Aspect;
using Meow.Biz.Area.Dto;
using Meow.Dependency;
using Meow.Parameter.Enum;
using Meow.Parameter.Object;

namespace Meow.Biz.Area.Service
{
    /// <summary>
    /// 地区服务
    /// </summary>
    public interface IAreaService : IScopeDependency
    {
        /// <summary>
        /// 获取：若不存在则返回null
        /// </summary>
        /// <param name="id">编号</param>
        AreaDto Get([NotEmpty] Guid? id);
        /// <summary>
        /// 获取树
        /// </summary>
        /// <param name="id">编号</param>
        /// <param name="endLevel">结束地区级别：默认区县级别</param>
        Tree<AreaDto> GetTree([NotEmpty] Guid? id, AreaLevel endLevel = AreaLevel.County);
        /// <summary>
        /// 获取：若不存在则返回空对象
        /// </summary>
        /// <param name="id">编号</param>
        AreaDto Single([NotEmpty] Guid? id);
        /// <summary>
        /// 根据编号集合获取
        /// </summary>
        /// <param name="ids">编号集合</param>
        List<AreaDto> GetByIds([NotNull] IEnumerable<Guid?> ids);
        /// <summary>
        /// 获取所有
        /// </summary>
        List<AreaDto> GetAll();
        /// <summary>
        /// 获取所有树
        /// </summary>
        List<Tree<AreaDto>> GetAllTree();
        /// <summary>
        /// 获取范围树
        /// </summary>
        /// <param name="startLevel">起始地区级别：默认到省份级别</param>
        /// <param name="endLevel">结束地区级别：默认区县级别</param>
        List<Tree<AreaDto>> GetBetweenTree(AreaLevel startLevel = AreaLevel.Province, AreaLevel endLevel = AreaLevel.County);
        /// <summary>
        /// 获取子集
        /// </summary>
        /// <param name="id">编号</param>
        List<AreaDto> GetSubset([NotEmpty] Guid? id);
        /// <summary>
        /// 获取父级
        /// </summary>
        /// <param name="id">编号</param>
        List<AreaDto> GetParent([NotEmpty] Guid? id);
        /// <summary>
        /// 获取父级树
        /// </summary>
        /// <param name="id">编号</param>
        Tree<AreaDto> GetParentTree([NotEmpty] Guid? id);
        /// <summary>
        /// 获取地址
        /// </summary>
        /// <param name="id">编号</param>
        AddressDto GetAddress([NotEmpty] Guid? id);
        /// <summary>
        /// 获取地址
        /// </summary>
        /// <param name="provinceId">省份编号</param>
        /// <param name="cityId">城市编号</param>
        /// <param name="countyId">区县编号</param>
        /// <param name="townId">街道/乡镇编号</param>
        AddressDto GetAddress(Guid? provinceId, Guid? cityId, Guid? countyId, Guid? townId = null);
        /// <summary>
        /// 根据关键字获取
        /// </summary>
        /// <param name="keyWord">关键字</param>
        /// <param name="areaLevel">地区级别：默认为null,为null查询所有</param>
        List<AreaDto> GetByKeyWord([NotEmpty] string keyWord, AreaLevel? areaLevel = null);
        /// <summary>
        /// 根据关键字获取树
        /// </summary>
        /// <param name="keyWord">关键字</param>
        /// <param name="areaLevel">地区级别：默认为null,为null查询所有</param>
        /// <param name="endLevel">结束地区级别：默认区县级别</param>
        List<Tree<AreaDto>> GetTreeByKeyWord([NotEmpty] string keyWord, AreaLevel? areaLevel = null, AreaLevel endLevel = AreaLevel.County);
    }
}