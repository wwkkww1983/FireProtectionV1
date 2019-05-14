using Abp.Domain.Repositories;
using Abp.Domain.Services;
using FireProtectionV1.Enterprise.Model;
using FireProtectionV1.Infrastructure.Dto;
using FireProtectionV1.Infrastructure.Model;
using FireProtectionV1.SupervisionCore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireProtectionV1.Infrastructure.Manager
{
    public class InfrastructureManager : DomainService, IInfrastructureManager
    {
        IRepository<SupervisionItem> _supervisionItemRepository;

        public InfrastructureManager(IRepository<SupervisionItem> supervisionItemRepository)
        {
            _supervisionItemRepository = supervisionItemRepository;
        }

        /// <summary>
        /// 初始化数据
        /// </summary>
        /// <returns></returns>
        public async Task InitData()
        {
            #region 监管执法项目
            if (_supervisionItemRepository.Count() == 0)
            {
                SupervisionItem supervisionItem = new SupervisionItem()
                {
                    Name = "消防安全管理"
                };
                var parentId = await _supervisionItemRepository.InsertAndGetIdAsync(supervisionItem);
                supervisionItem = new SupervisionItem()
                {
                    Name = "消防安全制度、消防安全操作规程",
                    ParentId = parentId
                };
                _supervisionItemRepository.Insert(supervisionItem);
                supervisionItem = new SupervisionItem()
                {
                    Name = "确定单位、部门、岗位消防安全责任人",
                    ParentId = parentId
                };
                _supervisionItemRepository.Insert(supervisionItem);
                supervisionItem = new SupervisionItem()
                {
                    Name = "对员工经常的消防安全教育",
                    ParentId = parentId
                };
                _supervisionItemRepository.Insert(supervisionItem);
                supervisionItem = new SupervisionItem()
                {
                    Name = "定期防火检查",
                    ParentId = parentId
                };
                _supervisionItemRepository.Insert(supervisionItem);
                supervisionItem = new SupervisionItem()
                {
                    Name = "整改火灾隐患",
                    ParentId = parentId
                };
                _supervisionItemRepository.Insert(supervisionItem);
                supervisionItem = new SupervisionItem()
                {
                    Name = "消防设施定期检测、维修制度",
                    ParentId = parentId
                };
                _supervisionItemRepository.Insert(supervisionItem);
                supervisionItem = new SupervisionItem()
                {
                    Name = "疏散通道、安全出口管理",
                    ParentId = parentId
                };
                _supervisionItemRepository.Insert(supervisionItem);
                supervisionItem = new SupervisionItem()
                {
                    Name = "单位建立防火档案",
                    ParentId = parentId
                };
                _supervisionItemRepository.Insert(supervisionItem);
                supervisionItem = new SupervisionItem()
                {
                    Name = "每日防火巡查",
                    ParentId = parentId
                };
                _supervisionItemRepository.Insert(supervisionItem);
                supervisionItem = new SupervisionItem()
                {
                    Name = "职工定期消防培训",
                    ParentId = parentId
                };
                _supervisionItemRepository.Insert(supervisionItem);
                supervisionItem = new SupervisionItem()
                {
                    Name = "重点工种人员持证上岗",
                    ParentId = parentId
                };
                _supervisionItemRepository.Insert(supervisionItem);
                supervisionItem = new SupervisionItem()
                {
                    Name = "灭火疏散预案",
                    ParentId = parentId
                };
                _supervisionItemRepository.Insert(supervisionItem);
                supervisionItem = new SupervisionItem()
                {
                    Name = "消防中心值班",
                    ParentId = parentId
                };
                _supervisionItemRepository.Insert(supervisionItem);

                supervisionItem = new SupervisionItem()
                {
                    Name = "建筑防火"
                };
                parentId = await _supervisionItemRepository.InsertAndGetIdAsync(supervisionItem);
                supervisionItem = new SupervisionItem()
                {
                    Name = "防火间距",
                    ParentId = parentId
                };
                _supervisionItemRepository.Insert(supervisionItem);
                supervisionItem = new SupervisionItem()
                {
                    Name = "消防车通道",
                    ParentId = parentId
                };
                _supervisionItemRepository.Insert(supervisionItem);
                supervisionItem = new SupervisionItem()
                {
                    Name = "建筑耐火等级与用途",
                    ParentId = parentId
                };
                _supervisionItemRepository.Insert(supervisionItem);
                supervisionItem = new SupervisionItem()
                {
                    Name = "安全出口",
                    ParentId = parentId
                };
                _supervisionItemRepository.Insert(supervisionItem);
                supervisionItem = new SupervisionItem()
                {
                    Name = "楼梯",
                    ParentId = parentId
                };
                _supervisionItemRepository.Insert(supervisionItem);
                supervisionItem = new SupervisionItem()
                {
                    Name = "防火门",
                    ParentId = parentId
                };
                _supervisionItemRepository.Insert(supervisionItem);
                supervisionItem = new SupervisionItem()
                {
                    Name = "防火卷帘",
                    ParentId = parentId
                };
                _supervisionItemRepository.Insert(supervisionItem);
                supervisionItem = new SupervisionItem()
                {
                    Name = "自然排烟窗口",
                    ParentId = parentId
                };
                _supervisionItemRepository.Insert(supervisionItem);

                supervisionItem = new SupervisionItem()
                {
                    Name = "消防设施"
                };
                parentId = await _supervisionItemRepository.InsertAndGetIdAsync(supervisionItem);
                supervisionItem = new SupervisionItem()
                {
                    Name = "消防水箱",
                    ParentId = parentId
                };
                _supervisionItemRepository.Insert(supervisionItem);
                supervisionItem = new SupervisionItem()
                {
                    Name = "消防水池",
                    ParentId = parentId
                };
                _supervisionItemRepository.Insert(supervisionItem);
                supervisionItem = new SupervisionItem()
                {
                    Name = "室外消火栓",
                    ParentId = parentId
                };
                _supervisionItemRepository.Insert(supervisionItem);
                supervisionItem = new SupervisionItem()
                {
                    Name = "室内消火栓",
                    ParentId = parentId
                };
                _supervisionItemRepository.Insert(supervisionItem);
                supervisionItem = new SupervisionItem()
                {
                    Name = "消防卷盘",
                    ParentId = parentId
                };
                _supervisionItemRepository.Insert(supervisionItem);
                supervisionItem = new SupervisionItem()
                {
                    Name = "消防水泵",
                    ParentId = parentId
                };
                _supervisionItemRepository.Insert(supervisionItem);
                supervisionItem = new SupervisionItem()
                {
                    Name = "水泵接合器",
                    ParentId = parentId
                };
                _supervisionItemRepository.Insert(supervisionItem);
                supervisionItem = new SupervisionItem()
                {
                    Name = "自动喷水及水雾灭火系统",
                    ParentId = parentId
                };
                _supervisionItemRepository.Insert(supervisionItem);
                supervisionItem = new SupervisionItem()
                {
                    Name = "泡沫灭火系统",
                    ParentId = parentId
                };
                _supervisionItemRepository.Insert(supervisionItem);
                supervisionItem = new SupervisionItem()
                {
                    Name = "气体灭火系统",
                    ParentId = parentId
                };
                _supervisionItemRepository.Insert(supervisionItem);
                supervisionItem = new SupervisionItem()
                {
                    Name = "火灾自动报警系统",
                    ParentId = parentId
                };
                _supervisionItemRepository.Insert(supervisionItem);
                supervisionItem = new SupervisionItem()
                {
                    Name = "灭火器",
                    ParentId = parentId
                };
                _supervisionItemRepository.Insert(supervisionItem);
                supervisionItem = new SupervisionItem()
                {
                    Name = "消防电源",
                    ParentId = parentId
                };
                _supervisionItemRepository.Insert(supervisionItem);
                supervisionItem = new SupervisionItem()
                {
                    Name = "自备发电机",
                    ParentId = parentId
                };
                _supervisionItemRepository.Insert(supervisionItem);
                supervisionItem = new SupervisionItem()
                {
                    Name = "消防电梯",
                    ParentId = parentId
                };
                _supervisionItemRepository.Insert(supervisionItem);
                supervisionItem = new SupervisionItem()
                {
                    Name = "疏散指示标志",
                    ParentId = parentId
                };
                _supervisionItemRepository.Insert(supervisionItem);
                supervisionItem = new SupervisionItem()
                {
                    Name = "应急照明",
                    ParentId = parentId
                };
                _supervisionItemRepository.Insert(supervisionItem);
                supervisionItem = new SupervisionItem()
                {
                    Name = "应急广播",
                    ParentId = parentId
                };
                _supervisionItemRepository.Insert(supervisionItem);
                supervisionItem = new SupervisionItem()
                {
                    Name = "消防电话插孔",
                    ParentId = parentId
                };
                _supervisionItemRepository.Insert(supervisionItem);
                supervisionItem = new SupervisionItem()
                {
                    Name = "防排烟系统",
                    ParentId = parentId
                };
                _supervisionItemRepository.Insert(supervisionItem);

                supervisionItem = new SupervisionItem()
                {
                    Name = "火灾爆炸危险场所"
                };
                parentId = await _supervisionItemRepository.InsertAndGetIdAsync(supervisionItem);
                supervisionItem = new SupervisionItem()
                {
                    Name = "防爆泄压面积",
                    ParentId = parentId
                };
                _supervisionItemRepository.Insert(supervisionItem);
                supervisionItem = new SupervisionItem()
                {
                    Name = "不发火花地面",
                    ParentId = parentId
                };
                _supervisionItemRepository.Insert(supervisionItem);
                supervisionItem = new SupervisionItem()
                {
                    Name = "防止液体流散设施",
                    ParentId = parentId
                };
                _supervisionItemRepository.Insert(supervisionItem);
                supervisionItem = new SupervisionItem()
                {
                    Name = "通风窗、洞口",
                    ParentId = parentId
                };
                _supervisionItemRepository.Insert(supervisionItem);
                supervisionItem = new SupervisionItem()
                {
                    Name = "防静电",
                    ParentId = parentId
                };
                _supervisionItemRepository.Insert(supervisionItem);
                supervisionItem = new SupervisionItem()
                {
                    Name = "防雷",
                    ParentId = parentId
                };
                _supervisionItemRepository.Insert(supervisionItem);
                supervisionItem = new SupervisionItem()
                {
                    Name = "防爆电气",
                    ParentId = parentId
                };
                _supervisionItemRepository.Insert(supervisionItem);
            }
            #endregion
            #region 初始化相关表数据
            #endregion
        }
    }
}
