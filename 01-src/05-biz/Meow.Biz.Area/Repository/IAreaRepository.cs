using System;
using System.Collections.Generic;
using System.Linq;
using Meow.Aspect;
using Meow.Biz.Area.Model;
using Meow.Dependency;
using Meow.Parameter.Enum;
using Meow.Parameter.Object;

namespace Meow.Biz.Area.Repository
{
    /// <summary>
    /// 地区仓储
    /// </summary>
    public interface IAreaRepository : IScopeDependency
    {
        /// <summary>
        /// 查找
        /// </summary>
        IQueryable<Model.Area> Find();
        /// <summary>
        /// 查找：若不存在则返回null
        /// </summary>
        /// <param name="id">编号</param>
        Model.Area Find([NotEmpty] Guid? id);
        /// <summary>
        /// 查找树
        /// </summary>
        /// <param name="id">编号</param>
        /// <param name="endLevel">结束地区级别：默认区县级别</param>
        Tree<Model.Area> FindTree([NotEmpty] Guid? id, AreaLevel endLevel = AreaLevel.County);
        /// <summary>
        /// 查找：若不存在则返回空对象
        /// </summary>
        /// <param name="id">编号</param>
        Model.Area Single([NotEmpty] Guid? id);
        /// <summary>
        /// 根据编号集合查找
        /// </summary>
        /// <param name="ids">编号集合</param>
        List<Model.Area> FindByIds([NotNull] IEnumerable<Guid?> ids);
        /// <summary>
        /// 查找所有
        /// </summary>
        List<Model.Area> FindAll();
        /// <summary>
        /// 查找所有树
        /// </summary>
        List<Tree<Model.Area>> FindAllTree();
        /// <summary>
        /// 查找范围树
        /// </summary>
        /// <param name="startLevel">起始地区级别：默认到省份级别</param>
        /// <param name="endLevel">结束地区级别：默认区县级别</param>
        List<Tree<Model.Area>> FindBetweenTree(AreaLevel startLevel = AreaLevel.Province, AreaLevel endLevel = AreaLevel.County);
        /// <summary>
        /// 查找子集
        /// </summary>
        /// <param name="id">编号</param>
        List<Model.Area> FindSubset([NotEmpty] Guid? id);
        /// <summary>
        /// 查找父级
        /// </summary>
        /// <param name="id">编号</param>
        List<Model.Area> FindParent([NotEmpty] Guid? id);
        /// <summary>
        /// 查找父级树
        /// </summary>
        /// <param name="id">编号</param>
        Tree<Model.Area> FindParentTree([NotEmpty] Guid? id);
        /// <summary>
        /// 查找地址
        /// </summary>
        /// <param name="id">编号</param>
        Address FindAddress([NotEmpty] Guid? id);
        /// <summary>
        /// 查找地址
        /// </summary>
        /// <param name="provinceId">省份编号</param>
        /// <param name="cityId">城市编号</param>
        /// <param name="countyId">区县编号</param>
        /// <param name="townId">街道/乡镇编号</param>
        Address FindAddress(Guid? provinceId, Guid? cityId, Guid? countyId, Guid? townId = null);
        /// <summary>
        /// 根据关键字查找
        /// </summary>
        /// <param name="keyWord">关键字</param>
        /// <param name="areaLevel">地区级别：默认为null,为null查询所有</param>
        List<Model.Area> FindByKeyWord([NotEmpty] string keyWord, AreaLevel? areaLevel = null);
        /// <summary>
        /// 根据关键字查找树
        /// </summary>
        /// <param name="keyWord">关键字</param>
        /// <param name="areaLevel">地区级别：默认为null,为null查询所有</param>
        /// <param name="endLevel">结束地区级别：默认区县级别</param>
        List<Tree<Model.Area>> FindTreeByKeyWord([NotEmpty] string keyWord, AreaLevel? areaLevel = null, AreaLevel endLevel = AreaLevel.County);
    }
}