using System.Collections.Generic;
using Meow.Biz.Area.Dto;
using Meow.Biz.Area.Dto.Extension;
using Meow.Biz.Area.Repository;
using Meow.Parameter.Enum;
using Meow.Parameter.Object;
using Guid = System.Guid;

namespace Meow.Biz.Area.Service
{
    /// <summary>
    /// 地区服务
    /// </summary>
    public class AreaService : IAreaService
    {
        /// <summary>
        /// 地区仓储
        /// </summary>
        protected IAreaRepository AreaRepository { get; set; }

        /// <summary>
        /// 初始化地区服务
        /// </summary>
        /// <param name="areaRepository">地区仓储</param>
        public AreaService(IAreaRepository areaRepository)
        {
            AreaRepository = areaRepository;
        }

        /// <summary>
        /// 获取：若不存在则返回null
        /// </summary>
        /// <param name="id">编号</param>
        public AreaDto Get(Guid? id)
        {
            var area = AreaRepository.Find(id);
            return area.ToDto();
        }

        /// <summary>
        /// 获取树
        /// </summary>
        /// <param name="id">编号</param>
        /// <param name="endLevel">结束地区级别：默认区县级别</param>
        public Tree<AreaDto> GetTree(Guid? id, AreaLevel endLevel = AreaLevel.County)
        {
            var area = AreaRepository.FindTree(id, endLevel);
            return area.ToDto();
        }

        /// <summary>
        /// 获取：若不存在则返回空对象
        /// </summary>
        /// <param name="id">编号</param>
        public AreaDto Single(Guid? id)
        {
            var area = AreaRepository.Single(id);
            return area.ToDto();
        }

        /// <summary>
        /// 根据编号集合获取
        /// </summary>
        /// <param name="ids">编号集合</param>
        public List<AreaDto> GetByIds(IEnumerable<Guid?> ids)
        {
            var areas = AreaRepository.FindByIds(ids);
            return areas.ToDto();
        }

        /// <summary>
        /// 获取所有
        /// </summary>
        public List<AreaDto> GetAll()
        {
            var areas = AreaRepository.FindAll();
            return areas.ToDto();
        }

        /// <summary>
        /// 获取所有树
        /// </summary>
        public List<Tree<AreaDto>> GetAllTree()
        {
            var areas = AreaRepository.FindAllTree();
            return areas.ToDto();
        }

        /// <summary>
        /// 获取范围树
        /// </summary>
        /// <param name="startLevel">起始地区级别：默认到省份级别</param>
        /// <param name="endLevel">结束地区级别：默认区县级别</param>
        public List<Tree<AreaDto>> GetBetweenTree(AreaLevel startLevel = AreaLevel.Province, AreaLevel endLevel = AreaLevel.County)
        {
            var areas = AreaRepository.FindBetweenTree(startLevel, endLevel);
            return areas.ToDto();
        }

        /// <summary>
        /// 获取子集
        /// </summary>
        /// <param name="id">编号</param>
        public List<AreaDto> GetSubset(Guid? id)
        {
            var areas = AreaRepository.FindSubset(id);
            return areas.ToDto();
        }

        /// <summary>
        /// 获取父级
        /// </summary>
        /// <param name="id">编号</param>
        public List<AreaDto> GetParent(Guid? id)
        {
            var areas = AreaRepository.FindParent(id);
            return areas.ToDto();
        }

        /// <summary>
        /// 获取父级树
        /// </summary>
        /// <param name="id">编号</param>
        public Tree<AreaDto> GetParentTree(Guid? id)
        {
            var area = AreaRepository.FindParentTree(id);
            return area.ToDto();
        }

        /// <summary>
        /// 获取地址
        /// </summary>
        /// <param name="id">编号</param>
        public AddressDto GetAddress(Guid? id)
        {
            var address = AreaRepository.FindAddress(id);
            return address.ToDto();
        }

        /// <summary>
        /// 获取地址
        /// </summary>
        /// <param name="provinceId">省份编号</param>
        /// <param name="cityId">城市编号</param>
        /// <param name="countyId">区县编号</param>
        /// <param name="townId">街道/乡镇编号</param>
        public AddressDto GetAddress(Guid? provinceId, Guid? cityId, Guid? countyId, Guid? townId = null)
        {
            var address = AreaRepository.FindAddress(provinceId, cityId, countyId, townId);
            return address.ToDto();
        }

        /// <summary>
        /// 根据关键字获取
        /// </summary>
        /// <param name="keyWord">关键字</param>
        /// <param name="areaLevel">地区级别：默认为null,为null查询所有</param>
        public List<AreaDto> GetByKeyWord(string keyWord, AreaLevel? areaLevel = null)
        {
            var areas = AreaRepository.FindByKeyWord(keyWord, areaLevel);
            return areas.ToDto();
        }

        /// <summary>
        /// 根据关键字获取树
        /// </summary>
        /// <param name="keyWord">关键字</param>
        /// <param name="areaLevel">地区级别：默认为null,为null查询所有</param>
        /// <param name="endLevel">结束地区级别：默认区县级别</param>
        public List<Tree<AreaDto>> GetTreeByKeyWord(string keyWord, AreaLevel? areaLevel = null, AreaLevel endLevel = AreaLevel.County)
        {
            var areas = AreaRepository.FindTreeByKeyWord(keyWord, areaLevel, endLevel);
            return areas.ToDto();
        }
    }
}