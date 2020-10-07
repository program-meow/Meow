using System.Collections.Generic;
using System.Linq;
using Meow.Biz.Area.Data;
using Meow.Biz.Area.Model;
using Meow.Exception;
using Meow.Extension.Helper;
using Meow.Parameter.Enum;
using Meow.Parameter.Object;
using Guid = System.Guid;

namespace Meow.Biz.Area.Repository
{
    /// <summary>
    /// 地区仓储
    /// </summary>
    public class AreaRepository : IAreaRepository
    {
        /// <summary>
        /// 数据
        /// </summary>
        private readonly List<Model.Area> _data = DataSingleton.GetInstance();
        /// <summary>
        /// 默认国家编码
        /// </summary>
        private readonly string _defaultCountryCode = "100000";

        /// <summary>
        /// 获取默认国家编号
        /// </summary>
        private Guid FindDefaultCountryId()
        {
            var country = _data.FirstOrDefault(t => t.RegionalismCode == _defaultCountryCode);
            if (country == null)
                throw new Warning("国家不存在");
            return country.Id;
        }

        /// <summary>
        /// 查找
        /// </summary>
        public IQueryable<Model.Area> Find()
        {
            var countryId = FindDefaultCountryId();
            return _data.AsQueryable()
                        .Where(t => t.Path.Contains(countryId.ToString()))
                        .Where(t => t.Id != countryId);
        }

        /// <summary>
        /// 查找：若不存在则返回null
        /// </summary>
        /// <param name="id">编号</param>
        public Model.Area Find(Guid? id)
        {
            if (id.IsEmpty())
                return null;
            return Find().FirstOrDefault(t => t.Id == id);
        }

        /// <summary>
        /// 查找树
        /// </summary>
        /// <param name="id">编号</param>
        /// <param name="endLevel">结束地区级别：默认区县级别</param>
        public Tree<Model.Area> FindTree(Guid? id, AreaLevel endLevel = AreaLevel.County)
        {
            var area = Find(id);
            if (area.IsNull())
                return new Tree<Model.Area>();
            if (area.Level.SafeValue() > endLevel)
                endLevel = area.Level.SafeValue() + 1;
            var areas = FindAll();
            var subsets = FindAllTree(areas, area.Level.SafeValue() + 1, endLevel, area.Id);
            return new Tree<Model.Area>(area, subsets, area.SortId);
        }

        /// <summary>
        /// 查找：若不存在则返回空对象
        /// </summary>
        /// <param name="id">编号</param>
        public Model.Area Single(Guid? id)
        {
            if (id.IsEmpty())
                return new Model.Area();
            return Find().Single(t => t.Id == id);
        }

        /// <summary>
        /// 根据编号集合查找
        /// </summary>
        /// <param name="ids">编号集合</param>
        public List<Model.Area> FindByIds(IEnumerable<Guid?> ids)
        {
            var safeIds = ids.ToNotNull();
            if (safeIds.IsEmpty())
                return new List<Model.Area>();
            return Find().Where(t => safeIds.Contains(t.Id)).ToList();
        }

        /// <summary>
        /// 查找所有
        /// </summary>
        public List<Model.Area> FindAll()
        {
            return Find().ToList();
        }

        /// <summary>
        /// 查找所有树
        /// </summary>
        public List<Tree<Model.Area>> FindAllTree()
        {
            var countryId = FindDefaultCountryId();
            var areas = FindAll();
            var result = FindAllTree(areas, AreaLevel.Province, AreaLevel.Town, countryId);
            return result;
        }

        /// <summary>
        /// 查找所有树
        /// </summary>
        /// <param name="list">集合</param>
        /// <param name="startLevel">起始地区级别</param>
        /// <param name="endLevel">结束地区级别</param>
        /// <param name="parentId">父编号</param>
        private List<Tree<Model.Area>> FindAllTree(List<Model.Area> list, AreaLevel startLevel, AreaLevel endLevel, Guid? parentId = null)
        {
            var result = new List<Tree<Model.Area>>();
            if (startLevel > endLevel)
                return result;
            var areas = list.Where(t => t.Level == startLevel).WhereIfNotEmpty(t => t.ParentId == parentId).ToList();
            foreach (var item in areas)
            {
                var subsets = FindAllTree(list, startLevel + 1, endLevel, item.Id);
                result.Add(new Tree<Model.Area>(item, subsets, item.SortId));
            }
            return result.OrderBy(t => t.SortId).ToList();
        }

        /// <summary>
        /// 查找范围树
        /// </summary>
        /// <param name="startLevel">起始地区级别：默认到省份级别</param>
        /// <param name="endLevel">结束地区级别：默认区县级别</param>
        public List<Tree<Model.Area>> FindBetweenTree(AreaLevel startLevel = AreaLevel.Province, AreaLevel endLevel = AreaLevel.County)
        {
            if (AreaLevel.Province > startLevel || AreaLevel.County < endLevel)
                throw new Warning("地区级别错误");
            var areas = FindAll();
            var result = FindAllTree(areas, startLevel, endLevel);
            return result;
        }

        /// <summary>
        /// 查找子集
        /// </summary>
        /// <param name="id">编号</param>
        public List<Model.Area> FindSubset(Guid? id)
        {
            var area = Find(id);
            if (area.IsNull())
                return new List<Model.Area>();
            var subsetLevel = area.Level.SafeValue() + 1;
            var result = Find().Where(t => t.Level == subsetLevel).ToList();
            return result;
        }

        /// <summary>
        /// 查找父级
        /// </summary>
        /// <param name="id">编号</param>
        public List<Model.Area> FindParent(Guid? id)
        {
            var area = Find(id);
            if (area.IsNull())
                return new List<Model.Area>();
            var ids = area.Path.ToGuidList();
            return Find().Where(t => ids.Contains(t.Id)).OrderBy(t => t.Level).ToList();
        }

        /// <summary>
        /// 查找父级树
        /// </summary>
        /// <param name="id">编号</param>
        public Tree<Model.Area> FindParentTree(Guid? id)
        {
            var areas = FindParent(id);
            if (areas.IsEmpty())
                return new Tree<Model.Area>();
            return FindParentTree(areas, AreaLevel.Province);
        }

        /// <summary>
        /// 获取父级树
        /// </summary>
        /// <param name="areas">地区集合</param>
        /// <param name="areaLevel">地区级别</param>
        private Tree<Model.Area> FindParentTree(List<Model.Area> areas, AreaLevel areaLevel)
        {
            if (areaLevel > AreaLevel.Town)
                return null;
            var area = areas.FirstOrDefault(t => t.Level == areaLevel);
            if (area.IsNull())
                return null;
            var subset = FindParentTree(areas, areaLevel + 1);
            return new Tree<Model.Area>(area, subset);
        }

        /// <summary>
        /// 查找地址
        /// </summary>
        /// <param name="id">编号</param>
        public Address FindAddress(Guid? id)
        {
            var areas = FindParent(id);
            if (areas.IsEmpty())
                return Address.Null;
            var province = areas.FirstOrDefault(t => t.Level == AreaLevel.Province);
            var city = areas.FirstOrDefault(t => t.Level == AreaLevel.City);
            var county = areas.FirstOrDefault(t => t.Level == AreaLevel.County);
            var town = areas.FirstOrDefault(t => t.Level == AreaLevel.Town);
            var zipCode = town?.ZipCode ?? county?.ZipCode ?? city?.ZipCode ?? province?.ZipCode;
            return new Address(province?.Id, city?.Id, county?.Id, town?.Id, province?.Name, city?.Name, county?.Name, town?.Name, zipCode);
        }

        /// <summary>
        /// 查找地址
        /// </summary>
        /// <param name="provinceId">省份编号</param>
        /// <param name="cityId">城市编号</param>
        /// <param name="countyId">区县编号</param>
        /// <param name="townId">街道/乡镇编号</param>
        public Address FindAddress(Guid? provinceId, Guid? cityId, Guid? countyId, Guid? townId = null)
        {
            var id = townId ?? countyId ?? cityId ?? provinceId;
            return FindAddress(id);
        }

        /// <summary>
        /// 根据关键字查找
        /// </summary>
        /// <param name="keyWord">关键字</param>
        /// <param name="areaLevel">地区级别：默认为null,为null查询所有</param>
        public List<Model.Area> FindByKeyWord(string keyWord, AreaLevel? areaLevel = null)
        {
            if (keyWord.IsEmpty())
                return new List<Model.Area>();
            return Find()
                  .Where(t => t.Name.Contains(keyWord) || t.Pinyin.Contains(keyWord))
                  .WhereIfNotEmpty(t => t.Level == areaLevel)
                  .OrderBy(t => t.SortId)
                  .ThenBy(t => t.Level)
                  .ToList();
        }

        /// <summary>
        /// 根据关键字查找树
        /// </summary>
        /// <param name="keyWord">关键字</param>
        /// <param name="areaLevel">地区级别：默认为null,为null查询所有</param>
        /// <param name="endLevel">结束地区级别：默认区县级别</param>
        public List<Tree<Model.Area>> FindTreeByKeyWord(string keyWord, AreaLevel? areaLevel = null, AreaLevel endLevel = AreaLevel.County)
        {
            var result = new List<Tree<Model.Area>>();
            if (keyWord.IsEmpty())
                return result;
            var areas = FindByKeyWord(keyWord, areaLevel);
            foreach (var item in areas)
            {
                var subset = FindTree(item.Id, endLevel);
                result.Add(subset);
            }
            return result.OrderBy(t => t.SortId).ToList();
        }
    }
}