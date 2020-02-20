using FireProtectionV1.FireWorking.Dto;
using FireProtectionV1.FireWorking.Manager;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FireProtectionV1.AppService
{
    public class EngineerRecordAppService : AppServiceBase
    {
        IEngineerRecordManager _engineerRecordManager;
        public EngineerRecordAppService(IEngineerRecordManager engineerRecordManager)
        {
            _engineerRecordManager = engineerRecordManager;
        }
        /// <summary>
        /// 添加施工记录
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task AddEngineerRecord(AddEngineerRecordInput input)
        {
            await _engineerRecordManager.AddEngineerRecord(input);
        }
    }
}
