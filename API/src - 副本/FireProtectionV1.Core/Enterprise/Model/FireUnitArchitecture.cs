using FireProtectionV1.Common.DBContext;
using System;
using System.Collections.Generic;
using System.Text;

namespace FireProtectionV1.Enterprise.Model
{
    public class FireUnitArchitecture : EntityBase
    {
        /// <summary>
        /// 建筑名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 消防联系人ID
        /// </summary>
        public int FireUnitUserId { get; set; }
        /// <summary>
        /// 地上层数
        /// </summary>
        public int AboveNum { get; set; }
        /// <summary>
        /// 地下层数
        /// </summary>
        public int BelowNum { get; set; }
        /// <summary>
        /// 建造年代
        /// </summary>
        public int BuildYear { get; set; }
        /// <summary>
        /// 建筑面积
        /// </summary>
        public decimal Area { get; set; }
        /// <summary>
        /// 建筑高度
        /// </summary>
        public decimal Height { get; set; }
        /// <summary>
        /// 外观图
        /// </summary>
        public string Outward_Picture { get; set; }
        /// <summary>
        /// 所属防火单位
        /// </summary>
        public int FireUnitId { get; set; }
        /// <summary>
        /// 消防设施_楼梯间_是否存在
        /// </summary>
        public bool FireDevice_LTJ_Exist { get; set; }
        /// <summary>
        /// 消防设施_楼梯间_细节
        /// </summary>
        public string FireDevice_LTJ_Detail { get; set; }
        /// <summary>
        /// 消防设施_火警系统_是否存在
        /// </summary>
        public bool FireDevice_HJ_Exist { get; set; }
        /// <summary>
        /// 消防设施_火警系统_细节
        /// </summary>
        public string FireDevice_HJ_Detail { get; set; }
        /// <summary>
        /// 消防设施_自动灭火系统_是否存在
        /// </summary>
        public bool FireDevice_MH_Exist { get; set; }
        /// <summary>
        /// 消防设施_自动灭火系统_细节
        /// </summary>
        public string FireDevice_MH_Detail { get; set; }
        /// <summary>
        /// 消防设施_通风和防排烟系统_是否存在
        /// </summary>
        public bool FireDevice_TFPY_Exist { get; set; }
        /// <summary>
        /// 消防设施_通风和防排烟系统_细节
        /// </summary>
        public string FireDevice_TFPY_Detail { get; set; }
        /// <summary>
        /// 消防设施_消火栓系统_是否存在
        /// </summary>
        public bool FireDevice_XHS_Exist { get; set; }
        /// <summary>
        /// 消防设施_消火栓系统_细节
        /// </summary>
        public string FireDevice_XHS_Detail { get; set; }
        /// <summary>
        /// 消防设施_消防水源_是否存在
        /// </summary>
        public bool FireDevice_XFSY_Exist { get; set; }
        /// <summary>
        /// 消防设施_消防水源_细节
        /// </summary>
        public string FireDevice_XFSY_Detail { get; set; }
        /// <summary>
        /// 消防设施_防火门_是否存在
        /// </summary>
        public bool FireDevice_FHM_Exist { get; set; }
        /// <summary>
        /// 消防设施_防火门_细节
        /// </summary>
        public string FireDevice_FHM_Detail { get; set; }
        /// <summary>
        /// 消防设施_防火卷帘_是否存在
        /// </summary>
        public bool FireDevice_FHJL_Exist { get; set; }
        /// <summary>
        /// 消防设施_防火卷帘_细节
        /// </summary>
        public string FireDevice_FHJL_Detail { get; set; }
        /// <summary>
        /// 消防设施_灭火器_是否存在
        /// </summary>
        public bool FireDevice_MHQ_Exist { get; set; }
        /// <summary>
        /// 消防设施_灭火器_细节
        /// </summary>
        public string FireDevice_MHQ_Detail { get; set; }
        /// <summary>
        /// 消防设施_应急照明_是否存在
        /// </summary>
        public bool FireDevice_YJZM_Exist { get; set; }
        /// <summary>
        /// 消防设施_应急照明_细节
        /// </summary>
        public string FireDevice_YJZM_Detail { get; set; }
        /// <summary>
        /// 消防设施_疏散指示标志_是否存在
        /// </summary>
        public bool FireDevice_SSZS_Exist { get; set; }
        /// <summary>
        /// 消防设施_疏散指示标志_细节
        /// </summary>
        public string FireDevice_SSZS_Detail { get; set; }
    }
}
