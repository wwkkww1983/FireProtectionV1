using System;
using System.Collections.Generic;
using System.Text;

namespace GovFire.Dto
{
    public class EventDto
    {
        public string id { get; set; }
        public string firecompany { get; set; }
        public string eventtype { get; set; }
        public string eventcontent { get; set; }
        public string createtime { get; set; }
        public string donetime { get; set; }
        public string state { get; set; }
        /// <summary>
        /// 维度
        /// </summary>
        public string lat { get; set; }
        /// <summary>
        /// 经度
        /// </summary>
        public string lon { get; set; }
        /// <summary>
        /// 大联动数据Id
        /// </summary>
        public string fireUnitId { get; set; }
    }
}
