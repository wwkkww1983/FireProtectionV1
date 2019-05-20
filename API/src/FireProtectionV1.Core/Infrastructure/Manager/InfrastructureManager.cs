using Abp.Domain.Repositories;
using Abp.Domain.Services;
using FireProtectionV1.Enterprise.Model;
using FireProtectionV1.HydrantCore.Model;
using FireProtectionV1.Infrastructure.Dto;
using FireProtectionV1.Infrastructure.Model;
using FireProtectionV1.MiniFireStationCore.Model;
using FireProtectionV1.StreetGridCore.Model;
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
        IRepository<MiniFireStation> _miniFireStationRepository;
        IRepository<StreetGridUser> _streetGridUserRepository;
        IRepository<StreetGridEvent> _streetGridEventRepository;
        IRepository<StreetGridEventRemark> _streetGridEventRemarkRepository;
        IRepository<Hydrant> _hydrantRepository;
        IRepository<HydrantAlarm> _hydrantAlarmRepository;
        IRepository<HydrantPressure> _hydrantPressureRepository;

        public InfrastructureManager(
            IRepository<SupervisionItem> supervisionItemRepository,
            IRepository<MiniFireStation> miniFireStationRepository,
            IRepository<StreetGridUser> streetGridUserRepository,
            IRepository<StreetGridEvent> streetGridEventRepository,
            IRepository<StreetGridEventRemark> streetGridEventRemarkRepository,
            IRepository<Hydrant> hydrantRepository,
            IRepository<HydrantAlarm> hydrantAlarmRepository,
            IRepository<HydrantPressure> hydrantPressureRepository
            )
        {
            _supervisionItemRepository = supervisionItemRepository;
            _miniFireStationRepository = miniFireStationRepository;
            _streetGridUserRepository = streetGridUserRepository;
            _streetGridEventRepository = streetGridEventRepository;
            _streetGridEventRemarkRepository = streetGridEventRemarkRepository;
            _hydrantRepository = hydrantRepository;
            _hydrantAlarmRepository = hydrantAlarmRepository;
            _hydrantPressureRepository = hydrantPressureRepository;
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

            #region 微型消防站
            if (_miniFireStationRepository.Count() == 0)
            {
                MiniFireStation miniFireStation = new MiniFireStation()
                {
                    Name = "老成洛路微型消防站",
                    Level = 1,
                    ContactName = "赵海",
                    ContactPhone = "13518122543",
                    PersonNum = 6,
                    Address = "成都市龙泉驿区老成洛路",
                    Lng = 104.159203M,
                    Lat = 30.633145M
                };
                await _miniFireStationRepository.InsertAsync(miniFireStation);
                miniFireStation = new MiniFireStation()
                {
                    Name = "黄苑街微型消防站",
                    Level = 2,
                    ContactName = "付建平",
                    ContactPhone = "13880834116",
                    PersonNum = 6,
                    Address = "成都市成华区黄苑街",
                    Lng = 104.099777M,
                    Lat = 30.716157M
                };
                await _miniFireStationRepository.InsertAsync(miniFireStation);
                miniFireStation = new MiniFireStation()
                {
                    Name = "华兴正街微型消防站",
                    Level = 3,
                    ContactName = "刘利娟",
                    ContactPhone = "18628199893",
                    PersonNum = 6,
                    Address = "成都市成华区华兴正街",
                    Lng = 104.100191M,
                    Lat = 30.707010M
                };
                await _miniFireStationRepository.InsertAsync(miniFireStation);
                miniFireStation = new MiniFireStation()
                {
                    Name = "红星微型消防站",
                    Level = 1,
                    ContactName = "邓修权",
                    ContactPhone = "18628199769",
                    PersonNum = 6,
                    Address = "红星路口",
                    Lng = 104.109328M,
                    Lat = 30.674936M
                };
                await _miniFireStationRepository.InsertAsync(miniFireStation);
                miniFireStation = new MiniFireStation()
                {
                    Name = "南兴巷微型消防站",
                    Level = 3,
                    ContactName = "沈蓉",
                    ContactPhone = "18180773397",
                    PersonNum = 6,
                    Address = "南兴巷1号",
                    Lng = 104.145928M,
                    Lat = 30.721121M
                };
                await _miniFireStationRepository.InsertAsync(miniFireStation);
                miniFireStation = new MiniFireStation()
                {
                    Name = "华美社区微型消防站",
                    Level = 1,
                    ContactName = "杨朝志",
                    ContactPhone = "13678084887",
                    PersonNum = 6,
                    Address = "华美路68号",
                    Lng = 104.147864M,
                    Lat = 30.687958M
                };
                await _miniFireStationRepository.InsertAsync(miniFireStation);
                miniFireStation = new MiniFireStation()
                {
                    Name = "黄河商业城微型消防站",
                    Level = 1,
                    ContactName = "邓永能",
                    ContactPhone = "13541389897",
                    PersonNum = 7,
                    Address = "永陵路19号",
                    Lng = 104.116024M,
                    Lat = 30.675076M
                };
                await _miniFireStationRepository.InsertAsync(miniFireStation);
                miniFireStation = new MiniFireStation()
                {
                    Name = "东郊园区微型消防站",
                    Level = 1,
                    ContactName = "余艳斌",
                    ContactPhone = "13540264940",
                    PersonNum = 7,
                    Address = "建设南支路4号",
                    Lng = 104.135529M,
                    Lat = 30.659065M
                };
                await _miniFireStationRepository.InsertAsync(miniFireStation);
                miniFireStation = new MiniFireStation()
                {
                    Name = "府兴街微型消防站",
                    Level = 2,
                    ContactName = "蒋波",
                    ContactPhone = "13730841035",
                    PersonNum = 5,
                    Address = "府兴街40号",
                    Lng = 104.097318M,
                    Lat = 30.675861M
                };
                await _miniFireStationRepository.InsertAsync(miniFireStation);
                miniFireStation = new MiniFireStation()
                {
                    Name = "名望大厦微型消防站",
                    Level = 3,
                    ContactName = "唐红军",
                    ContactPhone = "15008290395",
                    PersonNum = 6,
                    Address = "梨花节8-4号",
                    Lng = 104.104758M,
                    Lat = 30.669728M
                };
                await _miniFireStationRepository.InsertAsync(miniFireStation);
                miniFireStation = new MiniFireStation()
                {
                    Name = "紫东苑微型消防站",
                    Level = 3,
                    ContactName = "张明莉",
                    ContactPhone = "13308089949",
                    PersonNum = 5,
                    Address = "紫东正街54号",
                    Lng = 104.109422M,
                    Lat = 30.667093M
                };
                await _miniFireStationRepository.InsertAsync(miniFireStation);
                miniFireStation = new MiniFireStation()
                {
                    Name = "少城社区微型消防站",
                    Level = 3,
                    ContactName = "曾盈",
                    ContactPhone = "18108224316",
                    PersonNum = 6,
                    Address = "蜀华街16号",
                    Lng = 104.105264M,
                    Lat = 30.687660M
                };
                await _miniFireStationRepository.InsertAsync(miniFireStation);
                miniFireStation = new MiniFireStation()
                {
                    Name = "林茂大厦微型消防站",
                    Level = 2,
                    ContactName = "姜师",
                    ContactPhone = "13096387208",
                    PersonNum = 5,
                    Address = "肖家村四巷",
                    Lng = 104.181039M,
                    Lat = 30.694428M
                };
                await _miniFireStationRepository.InsertAsync(miniFireStation);
                miniFireStation = new MiniFireStation()
                {
                    Name = "金芙蓉微型消防站",
                    Level = 2,
                    ContactName = "陶杰",
                    ContactPhone = "13982096208",
                    PersonNum = 5,
                    Address = "陆家桥社区",
                    Lng = 104.120700M,
                    Lat = 30.656383M
                };
                await _miniFireStationRepository.InsertAsync(miniFireStation);
                miniFireStation = new MiniFireStation()
                {
                    Name = "斑竹园社区微型消防站",
                    Level = 2,
                    ContactName = "夏永勇",
                    ContactPhone = "15281046150",
                    PersonNum = 6,
                    Address = "复兴街2号",
                    Lng = 104.179513M,
                    Lat = 30.702151M
                };
                await _miniFireStationRepository.InsertAsync(miniFireStation);
                miniFireStation = new MiniFireStation()
                {
                    Name = "华夏加油站微型消防站",
                    Level = 2,
                    ContactName = "张家祥",
                    ContactPhone = "15982457665",
                    PersonNum = 5,
                    Address = "牛坝路2号",
                    Lng = 104.117609M,
                    Lat = 30.691200M
                };
                await _miniFireStationRepository.InsertAsync(miniFireStation);
            }
            #endregion

            #region 街道网格
            if (_streetGridUserRepository.Count() == 0)
            {
                StreetGridUser streetGridUser = new StreetGridUser()
                {
                    Name = "张光焰",
                    Phone = "17743201262",
                    GridName = "第八网格",
                    Street = "白莲池街道",
                    Community = "白莲池社区"
                };
                await _streetGridUserRepository.InsertAsync(streetGridUser);
                streetGridUser = new StreetGridUser()
                {
                    Name = "兰保全",
                    Phone = "13882115252",
                    GridName = "第二网格",
                    Street = "白莲池街道",
                    Community = "白莲池社区"
                };
                await _streetGridUserRepository.InsertAsync(streetGridUser);
                streetGridUser = new StreetGridUser()
                {
                    Name = "苏敏",
                    Phone = "17743201653",
                    GridName = "第二网格",
                    Street = "白莲池街道",
                    Community = "白莲池社区"
                };
                await _streetGridUserRepository.InsertAsync(streetGridUser);
                streetGridUser = new StreetGridUser()
                {
                    Name = "吴灵",
                    Phone = "17743201101",
                    GridName = "第四网格",
                    Street = "白莲池街道",
                    Community = "白莲池社区"
                };
                await _streetGridUserRepository.InsertAsync(streetGridUser);
                streetGridUser = new StreetGridUser()
                {
                    Name = "万莉",
                    Phone = "13880667982",
                    GridName = "第一网格",
                    Street = "白莲池街道",
                    Community = "白莲池社区"
                };
                await _streetGridUserRepository.InsertAsync(streetGridUser);
                streetGridUser = new StreetGridUser()
                {
                    Name = "何智鹏",
                    Phone = "17743201406",
                    GridName = "第三网格",
                    Street = "白莲池街道",
                    Community = "白莲池社区"
                };
                await _streetGridUserRepository.InsertAsync(streetGridUser);
                streetGridUser = new StreetGridUser()
                {
                    Name = "陈俊明",
                    Phone = "13008107562",
                    GridName = "第五网格",
                    Street = "白莲池街道",
                    Community = "回龙社区"
                };
                await _streetGridUserRepository.InsertAsync(streetGridUser);
                streetGridUser = new StreetGridUser()
                {
                    Name = "钟兴长",
                    Phone = "13548078459",
                    GridName = "第二网格",
                    Street = "白莲池街道",
                    Community = "将军碑社区"
                };
                await _streetGridUserRepository.InsertAsync(streetGridUser);
                streetGridUser = new StreetGridUser()
                {
                    Name = "周光东",
                    Phone = "17743201045",
                    GridName = "第二网格",
                    Street = "白莲池街道",
                    Community = "将军碑社区"
                };
                await _streetGridUserRepository.InsertAsync(streetGridUser);
                streetGridUser = new StreetGridUser()
                {
                    Name = "吴晓奎",
                    Phone = "13438213155",
                    GridName = "第四网格",
                    Street = "白莲池街道",
                    Community = "狮子社区"
                };
                await _streetGridUserRepository.InsertAsync(streetGridUser);
                streetGridUser = new StreetGridUser()
                {
                    Name = "刘世冬",
                    Phone = "13908205373",
                    GridName = "第五网格",
                    Street = "白莲池街道",
                    Community = "狮子社区"
                };
                await _streetGridUserRepository.InsertAsync(streetGridUser);
                streetGridUser = new StreetGridUser()
                {
                    Name = "何波",
                    Phone = "17743201586",
                    GridName = "第八网格",
                    Street = "保和街道",
                    Community = "东升社区"
                };
                await _streetGridUserRepository.InsertAsync(streetGridUser);
                streetGridUser = new StreetGridUser()
                {
                    Name = "代勇",
                    Phone = "18781973919",
                    GridName = "第一网格",
                    Street = "保和街道",
                    Community = "白莲池社区"
                };
                await _streetGridUserRepository.InsertAsync(streetGridUser);
                streetGridUser = new StreetGridUser()
                {
                    Name = "何玲",
                    Phone = "13458502328",
                    GridName = "第八网格",
                    Street = "二仙桥街道",
                    Community = "东路社区"
                };
                await _streetGridUserRepository.InsertAsync(streetGridUser);
                streetGridUser = new StreetGridUser()
                {
                    Name = "蒲茜",
                    Phone = "17743201709",
                    GridName = "第八网格",
                    Street = "二仙桥街道",
                    Community = "东路社区"
                };
                await _streetGridUserRepository.InsertAsync(streetGridUser);
                streetGridUser = new StreetGridUser()
                {
                    Name = "周道",
                    Phone = "13541291348",
                    GridName = "第五网格",
                    Street = "二仙桥街道",
                    Community = "东路社区"
                };
                await _streetGridUserRepository.InsertAsync(streetGridUser);
                streetGridUser = new StreetGridUser()
                {
                    Name = "黄奇",
                    Phone = "17743201778",
                    GridName = "第六网格",
                    Street = "府青路街道",
                    Community = "八里庄社区"
                };
                await _streetGridUserRepository.InsertAsync(streetGridUser);
                streetGridUser = new StreetGridUser()
                {
                    Name = "蔡玉生",
                    Phone = "17743201001",
                    GridName = "第二网格",
                    Street = "府青路街道",
                    Community = "府青路社区"
                };
                await _streetGridUserRepository.InsertAsync(streetGridUser);
            }
            #endregion

            #region 街道网格事件
            if (_streetGridEventRepository.Count() == 0)
            {
                StreetGridEvent streetGridEvent = new StreetGridEvent()
                {
                    Title = "安装私人防盗推拉门",
                    EventType = "消防栓异常",
                    StreetGridUserId = 1,
                    Status = EventStatus.处理中
                };
                var id = await _streetGridEventRepository.InsertAndGetIdAsync(streetGridEvent);
                StreetGridEventRemark streetGridEventRemark = new StreetGridEventRemark()
                {
                    StreetGridEventId = id,
                    Remark = "华西花园二期1-5单元一户居民因在大门口外安装私人防盗推拉门，挡住小区内的消防栓，造成消防隐患。网格员多次上门但家中无人无法联系，请相关领导给予处理。"
                };
                await _streetGridEventRemarkRepository.InsertAsync(streetGridEventRemark);
                streetGridEvent = new StreetGridEvent()
                {
                    Title = "安全隐患",
                    EventType = "煤气超标存放",
                    StreetGridUserId = 1,
                    Status = EventStatus.处理中
                };
                id = await _streetGridEventRepository.InsertAndGetIdAsync(streetGridEvent);
                streetGridEventRemark = new StreetGridEventRemark()
                {
                    StreetGridEventId = id,
                    Remark = "经巡查天鹅村15组金火焰联络点的临时库房里存放了12瓶满气罐，存在安全隐患"
                };
                await _streetGridEventRemarkRepository.InsertAsync(streetGridEventRemark);
                streetGridEvent = new StreetGridEvent()
                {
                    Title = "消防检查",
                    EventType = "三合一、二合一",
                    StreetGridUserId = 2,
                    Status = EventStatus.已办结
                };
                id = await _streetGridEventRepository.InsertAndGetIdAsync(streetGridEvent);
                streetGridEventRemark = new StreetGridEventRemark()
                {
                    StreetGridEventId = id,
                    Remark = "消防检查"
                };
                await _streetGridEventRemarkRepository.InsertAsync(streetGridEventRemark);
                streetGridEvent = new StreetGridEvent()
                {
                    Title = "电瓶车违规",
                    EventType = "电动（瓶）车违规充电",
                    StreetGridUserId = 1,
                    Status = EventStatus.已办结
                };
                id = await _streetGridEventRepository.InsertAndGetIdAsync(streetGridEvent);
                streetGridEventRemark = new StreetGridEventRemark()
                {
                    StreetGridEventId = id,
                    Remark = "居民将电源从房屋内牵出在窗户下充电"
                };
                await _streetGridEventRemarkRepository.InsertAsync(streetGridEventRemark);
                streetGridEvent = new StreetGridEvent()
                {
                    Title = "占道停车",
                    EventType = "三合一、二合一",
                    StreetGridUserId = 3,
                    Status = EventStatus.已办结
                };
                id = await _streetGridEventRepository.InsertAndGetIdAsync(streetGridEvent);
                streetGridEventRemark = new StreetGridEventRemark()
                {
                    StreetGridEventId = id,
                    Remark = "机动车占用盲道"
                };
                await _streetGridEventRemarkRepository.InsertAsync(streetGridEventRemark);
                streetGridEvent = new StreetGridEvent()
                {
                    Title = "消防器材老旧",
                    EventType = "灭火器严重异常",
                    StreetGridUserId = 4,
                    Status = EventStatus.待处理
                };
                id = await _streetGridEventRepository.InsertAndGetIdAsync(streetGridEvent);
                streetGridEventRemark = new StreetGridEventRemark()
                {
                    StreetGridEventId = id,
                    Remark = "消防器材严重老化"
                };
                await _streetGridEventRemarkRepository.InsertAsync(streetGridEventRemark);
                streetGridEvent = new StreetGridEvent()
                {
                    Title = "联合四组邱家院子租户使用明柴火",
                    EventType = "危险用火",
                    StreetGridUserId = 5,
                    Status = EventStatus.已办结
                };
                id = await _streetGridEventRepository.InsertAndGetIdAsync(streetGridEvent);
                streetGridEventRemark = new StreetGridEventRemark()
                {
                    StreetGridEventId = id,
                    Remark = "联合四组邱家院子租户使用简易灶具使用柴火"
                };
                await _streetGridEventRemarkRepository.InsertAsync(streetGridEventRemark);
                streetGridEvent = new StreetGridEvent()
                {
                    Title = "配电箱未做加盖防护",
                    EventType = "危险用电",
                    StreetGridUserId = 2,
                    Status = EventStatus.已办结
                };
                id = await _streetGridEventRepository.InsertAndGetIdAsync(streetGridEvent);
                streetGridEventRemark = new StreetGridEventRemark()
                {
                    StreetGridEventId = id,
                    Remark = "成都市成华区白莲池街道白莲池社区一组河对面仓库内，一配电箱未做加盖防护，危险用电"
                };
                await _streetGridEventRemarkRepository.InsertAsync(streetGridEventRemark);
                streetGridEvent = new StreetGridEvent()
                {
                    Title = "检查安全",
                    EventType = "三合一、二合一",
                    StreetGridUserId = 3,
                    Status = EventStatus.处理中
                };
                id = await _streetGridEventRepository.InsertAndGetIdAsync(streetGridEvent);
                streetGridEventRemark = new StreetGridEventRemark()
                {
                    StreetGridEventId = id,
                    Remark = "检查宣传车棚用电安全知识"
                };
                await _streetGridEventRemarkRepository.InsertAsync(streetGridEventRemark);
                streetGridEvent = new StreetGridEvent()
                {
                    Title = "商铺电动车违规充电",
                    EventType = "电动（瓶）车违规充电",
                    StreetGridUserId = 6,
                    Status = EventStatus.已办结
                };
                id = await _streetGridEventRepository.InsertAndGetIdAsync(streetGridEvent);
                streetGridEventRemark = new StreetGridEventRemark()
                {
                    StreetGridEventId = id,
                    Remark = "商贩电瓶车违规充电。已告其停止"
                };
                await _streetGridEventRemarkRepository.InsertAsync(streetGridEventRemark);
                streetGridEvent = new StreetGridEvent()
                {
                    Title = "违规充电",
                    EventType = "电动（瓶）车违规充电",
                    StreetGridUserId = 7,
                    Status = EventStatus.已办结
                };
                id = await _streetGridEventRepository.InsertAndGetIdAsync(streetGridEvent);
                streetGridEventRemark = new StreetGridEventRemark()
                {
                    StreetGridEventId = id,
                    Remark = "第十网格员于早上10：27院落巡防是时发现二环路东三段2号院落内发现违规拉电给电瓶车充电。"
                };
                await _streetGridEventRemarkRepository.InsertAsync(streetGridEventRemark);
                streetGridEvent = new StreetGridEvent()
                {
                    Title = "清查凤翔华庭荊翠南二街23号",
                    EventType = "三合一、二合一",
                    StreetGridUserId = 6,
                    Status = EventStatus.已办结
                };
                id = await _streetGridEventRepository.InsertAndGetIdAsync(streetGridEvent);
                streetGridEventRemark = new StreetGridEventRemark()
                {
                    StreetGridEventId = id,
                    Remark = "双水碾派出所对荊翠南二街23号凤翔华庭进行清查"
                };
                await _streetGridEventRemarkRepository.InsertAsync(streetGridEventRemark);
                streetGridEvent = new StreetGridEvent()
                {
                    Title = "巡查电瓶车棚",
                    EventType = "电动（瓶）车违规充电",
                    StreetGridUserId = 6,
                    Status = EventStatus.已办结
                };
                id = await _streetGridEventRepository.InsertAndGetIdAsync(streetGridEvent);
                streetGridEventRemark = new StreetGridEventRemark()
                {
                    StreetGridEventId = id,
                    Remark = "检查各小区自行车棚冲电桩安全隐患。"
                };
                await _streetGridEventRemarkRepository.InsertAsync(streetGridEventRemark);
                streetGridEvent = new StreetGridEvent()
                {
                    Title = "灭火器排查",
                    EventType = "灭火器轻微异常",
                    StreetGridUserId = 8,
                    Status = EventStatus.已办结
                };
                id = await _streetGridEventRepository.InsertAndGetIdAsync(streetGridEvent);
                streetGridEventRemark = new StreetGridEventRemark()
                {
                    StreetGridEventId = id,
                    Remark = "灭火器排查正常"
                };
                await _streetGridEventRemarkRepository.InsertAsync(streetGridEventRemark);
                streetGridEvent = new StreetGridEvent()
                {
                    Title = "双建路363号商铺堆放液化气罐在人行道上",
                    EventType = "煤气超标存放",
                    StreetGridUserId = 9,
                    Status = EventStatus.待处理
                };
                id = await _streetGridEventRepository.InsertAndGetIdAsync(streetGridEvent);
                streetGridEventRemark = new StreetGridEventRemark()
                {
                    StreetGridEventId = id,
                    Remark = "双建路363号商铺堆放液化石油气罐在人行道上。以多次上报。"
                };
                await _streetGridEventRemarkRepository.InsertAsync(streetGridEventRemark);
                streetGridEvent = new StreetGridEvent()
                {
                    Title = "检查消防通道",
                    EventType = "群租房",
                    StreetGridUserId = 10,
                    Status = EventStatus.处理中
                };
                id = await _streetGridEventRepository.InsertAndGetIdAsync(streetGridEvent);
                streetGridEventRemark = new StreetGridEventRemark()
                {
                    StreetGridEventId = id,
                    Remark = "检查消防通道"
                };
                await _streetGridEventRemarkRepository.InsertAsync(streetGridEventRemark);
                streetGridEvent = new StreetGridEvent()
                {
                    Title = "铺面失火",
                    EventType = "危险用火",
                    StreetGridUserId = 11,
                    Status = EventStatus.已办结
                };
                id = await _streetGridEventRepository.InsertAndGetIdAsync(streetGridEvent);
                streetGridEventRemark = new StreetGridEventRemark()
                {
                    StreetGridEventId = id,
                    Remark = "铺面失火，装修时电焊火花引起燃火，已扑灭"
                };
                await _streetGridEventRemarkRepository.InsertAsync(streetGridEventRemark);
                streetGridEvent = new StreetGridEvent()
                {
                    Title = "清查新疆人",
                    EventType = "三合一、二合一",
                    StreetGridUserId = 5,
                    Status = EventStatus.已办结
                };
                id = await _streetGridEventRepository.InsertAndGetIdAsync(streetGridEvent);
                streetGridEventRemark = new StreetGridEventRemark()
                {
                    StreetGridEventId = id,
                    Remark = "清查新疆人"
                };
                await _streetGridEventRemarkRepository.InsertAsync(streetGridEventRemark);
                streetGridEvent = new StreetGridEvent()
                {
                    Title = "共享单车骑进院落",
                    EventType = "安全通道轻微堵塞",
                    StreetGridUserId = 1,
                    Status = EventStatus.待处理
                };
                id = await _streetGridEventRepository.InsertAndGetIdAsync(streetGridEvent);
                streetGridEventRemark = new StreetGridEventRemark()
                {
                    StreetGridEventId = id,
                    Remark = "东四院20栋2单元共享单车骑进院落，轻微堵塞安全通道"
                };
                await _streetGridEventRemarkRepository.InsertAsync(streetGridEventRemark);
                streetGridEvent = new StreetGridEvent()
                {
                    Title = "雍华府商住楼检查",
                    EventType = "三合一、二合一",
                    StreetGridUserId = 3,
                    Status = EventStatus.已办结
                };
                id = await _streetGridEventRepository.InsertAndGetIdAsync(streetGridEvent);
                streetGridEventRemark = new StreetGridEventRemark()
                {
                    StreetGridEventId = id,
                    Remark = "单位检查"
                };
                await _streetGridEventRemarkRepository.InsertAsync(streetGridEventRemark);
            }
            #endregion

            #region 市政消火栓
            if (_hydrantRepository.Count() == 0)
            {
                Hydrant hydrant = new Hydrant()
                {
                    Sn = "jrt4fd",
                    AreaId = 32950,
                    Address = "东荆路8号东林景忆小区后门",
                    Status = Common.Enum.GatewayStatus.Unusual,
                    Lng = 104.135640M,
                    Lat = 30.666812M
                };
                var id = await _hydrantRepository.InsertAndGetIdAsync(hydrant);
                HydrantPressure hydrantPressure = new HydrantPressure()
                {
                    HydrantId = id,
                    Pressure = 170,
                    CreationTime = DateTime.Parse("2019-05-18 09:00"),
                };
                await _hydrantPressureRepository.InsertAsync(hydrantPressure);
                hydrantPressure = new HydrantPressure()
                {
                    HydrantId = id,
                    Pressure = 176,
                    CreationTime = DateTime.Parse("2019-05-19 09:00"),
                };
                await _hydrantPressureRepository.InsertAsync(hydrantPressure);
                hydrantPressure = new HydrantPressure()
                {
                    HydrantId = id,
                    Pressure = 80,
                    CreationTime = DateTime.Parse("2019-05-19 16:32"),
                };
                await _hydrantPressureRepository.InsertAsync(hydrantPressure);
                HydrantAlarm hydrantAlarm = new HydrantAlarm()
                {
                    HydrantId = id,
                    CreationTime = DateTime.Parse("2019-05-19 16:32"),
                    Title = "消火栓水压异常报警。编号jrt4fd，水压80kPa"
                };
                await _hydrantAlarmRepository.InsertAsync(hydrantAlarm);

                hydrant = new Hydrant()
                {
                    Sn = "htrhh7",
                    AreaId = 32951,
                    Address = "二环路东二段2-5号",
                    Status = Common.Enum.GatewayStatus.Unusual,
                    Lng = 104.116797M,
                    Lat = 30.658379M
                };
                id = await _hydrantRepository.InsertAndGetIdAsync(hydrant);
                hydrantPressure = new HydrantPressure()
                {
                    HydrantId = id,
                    Pressure = 220,
                    CreationTime = DateTime.Parse("2019-05-18 09:00"),
                };
                await _hydrantPressureRepository.InsertAsync(hydrantPressure);
                hydrantPressure = new HydrantPressure()
                {
                    HydrantId = id,
                    Pressure = 198,
                    CreationTime = DateTime.Parse("2019-05-19 09:00"),
                };
                await _hydrantPressureRepository.InsertAsync(hydrantPressure);
                hydrantPressure = new HydrantPressure()
                {
                    HydrantId = id,
                    Pressure = 33,
                    CreationTime = DateTime.Parse("2019-05-19 17:02"),
                };
                await _hydrantPressureRepository.InsertAsync(hydrantPressure);
                hydrantAlarm = new HydrantAlarm()
                {
                    HydrantId = id,
                    CreationTime = DateTime.Parse("2019-05-19 17:02"),
                    Title = "消火栓水压异常报警。编号htrhh7，水压33kPa"
                };
                await _hydrantAlarmRepository.InsertAsync(hydrantAlarm);
                hydrant = new Hydrant()
                {
                    Sn = "fjfgjh",
                    AreaId = 32950,
                    Address = "双林北支路335号",
                    Status = Common.Enum.GatewayStatus.Offline,
                    Lng = 104.098228M,
                    Lat = 30.713786M
                };
                await _hydrantRepository.InsertAsync(hydrant);
                hydrant = new Hydrant()
                {
                    Sn = "gwer5e",
                    AreaId = 32952,
                    Address = "东紫路618号附13号",
                    Status = Common.Enum.GatewayStatus.Online,
                    Lng = 104.150987M,
                    Lat = 30.673344M
                };
                await _hydrantRepository.InsertAsync(hydrant);
                hydrant = new Hydrant()
                {
                    Sn = "lkjur3",
                    AreaId = 32952,
                    Address = "中道街157号",
                    Status = Common.Enum.GatewayStatus.Online,
                    Lng = 104.123284M,
                    Lat = 30.683161M
                };
                await _hydrantRepository.InsertAsync(hydrant);
                hydrant = new Hydrant()
                {
                    Sn = "fghdg4",
                    AreaId = 32953,
                    Address = "崔家店横一街57号",
                    Status = Common.Enum.GatewayStatus.Online,
                    Lng = 104.099134M,
                    Lat = 30.707252M
                };
                await _hydrantRepository.InsertAsync(hydrant);
                hydrant = new Hydrant()
                {
                    Sn = "u5hr64",
                    AreaId = 32954,
                    Address = "水碾河北2街19",
                    Status = Common.Enum.GatewayStatus.Online,
                    Lng = 104.104024M,
                    Lat = 30.671676M
                };
                await _hydrantRepository.InsertAsync(hydrant);
                hydrant = new Hydrant()
                {
                    Sn = "hhrhbd",
                    AreaId = 32951,
                    Address = "建设支巷14号",
                    Status = Common.Enum.GatewayStatus.Online,
                    Lng = 104.160230M,
                    Lat = 30.648819M
                };
                await _hydrantRepository.InsertAsync(hydrant);
                hydrant = new Hydrant()
                {
                    Sn = "hftyr6",
                    AreaId = 32955,
                    Address = "建设路47号附13-14",
                    Status = Common.Enum.GatewayStatus.Online,
                    Lng = 104.123912M,
                    Lat = 30.657478M
                };
                await _hydrantRepository.InsertAsync(hydrant);
                hydrant = new Hydrant()
                {
                    Sn = "rygfr6",
                    AreaId = 32952,
                    Address = "新鸿南路77附13号",
                    Status = Common.Enum.GatewayStatus.Online,
                    Lng = 104.105732M,
                    Lat = 30.677001M
                };
                await _hydrantRepository.InsertAsync(hydrant);
                hydrant = new Hydrant()
                {
                    Sn = "htyry5",
                    AreaId = 32956,
                    Address = "怡福巷36号1层",
                    Status = Common.Enum.GatewayStatus.Online,
                    Lng = 104.106435M,
                    Lat = 30.662876M
                };
                await _hydrantRepository.InsertAsync(hydrant);
                hydrant = new Hydrant()
                {
                    Sn = "hrty4h",
                    AreaId = 32950,
                    Address = "双成三路15号附189-184号",
                    Status = Common.Enum.GatewayStatus.Online,
                    Lng = 104.105681M,
                    Lat = 30.667650M
                };
                await _hydrantRepository.InsertAsync(hydrant);
                hydrant = new Hydrant()
                {
                    Sn = "jrtrg5",
                    AreaId = 32957,
                    Address = "天祥街18号",
                    Status = Common.Enum.GatewayStatus.Online,
                    Lng = 104.140455M,
                    Lat = 30.680552M
                };
                await _hydrantRepository.InsertAsync(hydrant);
                hydrant = new Hydrant()
                {
                    Sn = "sdfds5",
                    AreaId = 32952,
                    Address = "建设支巷14号附4号",
                    Status = Common.Enum.GatewayStatus.Online,
                    Lng = 104.122043M,
                    Lat = 30.685953M
                };
                await _hydrantRepository.InsertAsync(hydrant);
                hydrant = new Hydrant()
                {
                    Sn = "gtry4r",
                    AreaId = 32955,
                    Address = "新华社区15幢",
                    Status = Common.Enum.GatewayStatus.Online,
                    Lng = 104.105096M,
                    Lat = 30.686094M
                };
                await _hydrantRepository.InsertAsync(hydrant);
                hydrant = new Hydrant()
                {
                    Sn = "ert454",
                    AreaId = 32951,
                    Address = "东风路北1巷-新1号-附12",
                    Status = Common.Enum.GatewayStatus.Online,
                    Lng = 104.145627M,
                    Lat = 30.649302M
                };
                await _hydrantRepository.InsertAsync(hydrant);
                hydrant = new Hydrant()
                {
                    Sn = "hfrty6",
                    AreaId = 32958,
                    Address = "双桥路247号1-3号",
                    Status = Common.Enum.GatewayStatus.Online,
                    Lng = 104.122429M,
                    Lat = 30.682856M
                };
                await _hydrantRepository.InsertAsync(hydrant);
                hydrant = new Hydrant()
                {
                    Sn = "hrtf54",
                    AreaId = 32958,
                    Address = "双桥路184号附1号",
                    Status = Common.Enum.GatewayStatus.Online,
                    Lng = 104.121227M,
                    Lat = 30.675624M
                };
                await _hydrantRepository.InsertAsync(hydrant);
            }
            #endregion

            #region 初始化相关表数据
            #endregion
        }
    }
}
