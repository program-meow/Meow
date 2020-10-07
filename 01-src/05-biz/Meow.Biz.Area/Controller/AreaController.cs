using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Meow.Application.Presentation.Controller;
using Meow.Biz.Area.Service;
using Meow.Parameter.Enum;

namespace Meow.Biz.Area.Controller
{
    /// <summary>
    /// 地区控制器
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public abstract class AreaController : WebApiController
    {
        /// <summary>
        /// 地区服务
        /// </summary>
        protected IAreaService AreaService { get; set; }

        /// <summary>
        /// 初始化地区控制器
        /// </summary>
        /// <param name="areaService">地区服务</param>
        protected AreaController(IAreaService areaService)
        {
            AreaService = areaService;
        }

        /// <summary>
        /// 获取：若不存在则返回空对象
        /// </summary>
        /// <param name="id">编号</param>
        [HttpGet]
        public IActionResult Get(Guid? id)
        {
            var result = AreaService.Single(id);
            return Ok(result);
        }

        /// <summary>
        /// 获取树
        /// </summary>
        /// <param name="id">编号</param>
        /// <param name="endLevel">结束地区级别：默认区县级别</param>
        [HttpGet("getTree")]
        public IActionResult GetTree(Guid? id, AreaLevel endLevel = AreaLevel.County)
        {
            var result = AreaService.GetTree(id, endLevel);
            return Ok(result);
        }

        /// <summary>
        /// 根据编号集合获取
        /// </summary>
        /// <param name="ids">编号集合</param>
        [HttpGet("getByIds")]
        public IActionResult GetByIds(IEnumerable<Guid?> ids)
        {
            var result = AreaService.GetByIds(ids);
            return Ok(result);
        }

        /// <summary>
        /// 获取所有
        /// </summary>
        [HttpGet("getAll")]
        public IActionResult GetAll()
        {
            var result = AreaService.GetAll();
            return Ok(result);
        }

        /// <summary>
        /// 获取所有树
        /// </summary>
        [HttpGet("getAllTree")]
        public IActionResult GetAllTree()
        {
            var result = AreaService.GetAllTree();
            return Ok(result);
        }

        /// <summary>
        /// 获取范围树
        /// </summary>
        /// <param name="startLevel">起始地区级别：默认到省份级别</param>
        /// <param name="endLevel">结束地区级别：默认区县级别</param>
        [HttpGet("getBetweenTree")]
        public IActionResult GetBetweenTree(AreaLevel startLevel = AreaLevel.Province, AreaLevel endLevel = AreaLevel.County)
        {
            var result = AreaService.GetBetweenTree(startLevel, endLevel);
            return Ok(result);
        }

        /// <summary>
        /// 获取子集
        /// </summary>
        /// <param name="id">编号</param>
        [HttpGet("getSubset")]
        public IActionResult GetSubset(Guid? id)
        {
            var result = AreaService.GetSubset(id);
            return Ok(result);
        }

        /// <summary>
        /// 获取父级
        /// </summary>
        /// <param name="id">编号</param>
        [HttpGet("getParent")]
        public IActionResult GetParent(Guid? id)
        {
            var result = AreaService.GetParent(id);
            return Ok(result);
        }

        /// <summary>
        /// 获取父级树
        /// </summary>
        /// <param name="id">编号</param>
        [HttpGet("getParentTree")]
        public IActionResult GetParentTree(Guid? id)
        {
            var result = AreaService.GetParentTree(id);
            return Ok(result);
        }

        /// <summary>
        /// 获取地址
        /// </summary>
        /// <param name="id">编号</param>
        [HttpGet("getAddressById")]
        public IActionResult GetAddress(Guid? id)
        {
            var result = AreaService.GetAddress(id);
            return Ok(result);
        }

        /// <summary>
        /// 获取地址
        /// </summary>
        /// <param name="provinceId">省份编号</param>
        /// <param name="cityId">城市编号</param>
        /// <param name="countyId">区县编号</param>
        /// <param name="townId">街道/乡镇编号</param>
        [HttpGet("getAddress")]
        public IActionResult GetAddress(Guid? provinceId, Guid? cityId, Guid? countyId, Guid? townId = null)
        {
            var result = AreaService.GetAddress(provinceId, cityId, countyId, townId);
            return Ok(result);
        }

        /// <summary>
        /// 根据关键字获取
        /// </summary>
        /// <param name="keyWord">关键字</param>
        /// <param name="areaLevel">地区级别：默认为null,为null查询所有</param>
        [HttpGet("getByKeyWord")]
        public IActionResult GetByKeyWord(string keyWord, AreaLevel? areaLevel = null)
        {
            var result = AreaService.GetByKeyWord(keyWord, areaLevel);
            return Ok(result);
        }

        /// <summary>
        /// 根据关键字获取树
        /// </summary>
        /// <param name="keyWord">关键字</param>
        /// <param name="areaLevel">地区级别：默认为null,为null查询所有</param>
        /// <param name="endLevel">结束地区级别：默认区县级别</param>
        [HttpGet("getTreeByKeyWord")]
        public IActionResult GetTreeByKeyWord(string keyWord, AreaLevel? areaLevel = null, AreaLevel endLevel = AreaLevel.County)
        {
            var result = AreaService.GetTreeByKeyWord(keyWord, areaLevel, endLevel);
            return Ok(result);
        }
    }
}