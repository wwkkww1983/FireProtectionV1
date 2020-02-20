using System;
using System.Collections.Generic;
using System.Text;

namespace FireProtectionV1.FireWorking.Dto
{
    public class AddEngineerRecordInput : FireElectricDeviceParaDto
    {
        /// <summary>
        /// 施工人员Id
        /// </summary>
        public int EngineerUserId { get; set; }
        /// <summary>
        /// 防火单位Id，如果是要新增防火单位，则FireUnitId=0；如果是已存在的防火单位，则传入该单位的FireUnitId
        /// </summary>
        public int FireUnitId { get; set; }
        /// <summary>
        /// 防火单位名称，如果FireUnitId=0，则需传入该参数，否则不需传入该参数
        /// </summary>
        public string FireUnitName { get; set; }
        /// <summary>
        /// 防火单位区域，如果FireUnitId=0，则需传入该参数，否则不需传入该参数
        /// </summary>
        public int AreaId { get; set; }
        /// <summary>
        /// 经度，如果FireUnitId=0，则需传入该参数，否则不需传入该参数
        /// </summary>
        public decimal Lng { get; set; }
        /// <summary>
        /// 纬度，如果FireUnitId=0，则需传入该参数，否则不需传入该参数
        /// </summary>
        public decimal Lat { get; set; }
        /// <summary>
        /// 防火单位联系人姓名，如果FireUnitId=0，则需传入该参数，否则不需传入该参数
        /// </summary>
        public string ContractName { get; set; }
        /// <summary>
        /// 防火单位联系人手机，如果FireUnitId=0，则需传入该参数，否则不需传入该参数
        /// </summary>
        public string ContractPhone { get; set; }
        /// <summary>
        /// 电气火灾设施Sn
        /// </summary>
        public string DeviceSn { get; set; }
    }
}
