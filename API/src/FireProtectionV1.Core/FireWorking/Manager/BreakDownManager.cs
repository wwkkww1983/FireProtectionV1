using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using FireProtectionV1.Common.DBContext;
using FireProtectionV1.Common.Enum;
using FireProtectionV1.Common.Helper;
using FireProtectionV1.Enterprise.Model;
using FireProtectionV1.FireWorking.Dto;
using FireProtectionV1.FireWorking.Model;
using FireProtectionV1.User.Model;
using GovFire;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireProtectionV1.FireWorking.Manager
{
    public class BreakDownManager : IBreakDownManager
    {
        IHostingEnvironment _hostingEnv;
        IRepository<FireAlarmDetector> _repFireAlarmDetector;
        IRepository<SafeUnitUser> _repSafeUnitUser;
        IRepository<FireUnit> _repFireUnit;
        IRepository<BreakDown> _repBreakDown;
        IRepository<FireUnitUser> _repFireUnitUser;
        IRepository<PhotosPathSave> _repPhotosPathSave;
        IRepository<SafeUnit> _repSafeUnit;
        IRepository<DataToPatrolDetailProblem> _repDataToPatrolDetailProblem;
        IRepository<Fault> _repFault;
        IRepository<DataToDuty> _repDataToDuty;
        IRepository<DataToPatrol> _repDataToPatrol;
        IRepository<DataToPatrolDetail> _repDataToPatrolDetail;
        public BreakDownManager(
            IHostingEnvironment hostingEnv,
            IRepository<FireAlarmDetector> repFireAlarmDetector,
            IRepository<SafeUnitUser> repSafeUnitUser,
            IRepository<FireUnit> repFireUnit,
            IRepository<BreakDown> repBreakDown,
            IRepository<PhotosPathSave> repPhotosPathSave,
            IRepository<FireUnitUser> repFireUnitUser,
            IRepository<SafeUnit> repSafeUnit,
            IRepository<DataToPatrolDetailProblem> repDataToPatrolDetailProblem,
            IRepository<DataToDuty> repDataToDuty,
            IRepository<DataToPatrol> repDataToPatrol,
            IRepository<DataToPatrolDetail> repDataToPatrolDetail,
            IRepository<Fault> repFault
            )
        {
            _hostingEnv = hostingEnv;
            _repFireAlarmDetector = repFireAlarmDetector;
            _repSafeUnitUser = repSafeUnitUser;
            _repFireUnit = repFireUnit;
            _repBreakDown = repBreakDown;
            _repPhotosPathSave = repPhotosPathSave;
            _repFireUnitUser = repFireUnitUser;
            _repSafeUnit = repSafeUnit;
            _repDataToPatrolDetailProblem = repDataToPatrolDetailProblem;
            _repDataToDuty = repDataToDuty;
            _repDataToPatrol = repDataToPatrol;
            _repDataToPatrolDetail = repDataToPatrolDetail;
            _repFault = repFault;
        }
        /// <summary>
        /// 获取设施故障列表
        /// </summary>
        /// <param name="input"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        public Task<PagedResultDto<GetBreakDownOutput>> GetBreakDownlist(GetBreakDownInput input, PagedResultRequestDto dto)
        {
            var breakDowns = _repBreakDown.GetAll().Where(item => item.FireUnitId.Equals(input.FireUnitId) && item.HandleStatus.Equals(input.HandleStatus));

            var query = from a in breakDowns
                        select new GetBreakDownOutput()
                        {
                            BreakDownId = a.Id,
                            CreationTime = a.CreationTime,
                            DispatchTime = a.DispatchTime,
                            HandleStatus = a.HandleStatus,
                            SolutionTime = a.SolutionTime,
                            SolutionWay = a.SolutionWay,
                            Source = a.Source,
                            UserId = a.UserId,
                            UserBelongUnitId = a.UserBelongUnitId,
                            FireUnitId = a.FireUnitId
                        };

            if (input.HandleStatus.Equals(HandleStatus.UnResolve))
            {
                query = query.OrderByDescending(item => item.CreationTime);
            }
            else if (input.HandleStatus.Equals(HandleStatus.SelfHandle) || input.HandleStatus.Equals(HandleStatus.SafeResolving))
            {
                query = query.OrderByDescending(item => item.DispatchTime);
            }
            else if (input.HandleStatus.Equals(HandleStatus.SafeResolved) || input.HandleStatus.Equals(HandleStatus.Resolved))
            {
                query = query.OrderByDescending(item => item.SolutionTime);
            }

            var list = query.Skip(dto.SkipCount).Take(dto.MaxResultCount).ToList();
            var tCount = query.Count();

            foreach (var item in list)
            {
                if (item.UserId > 0 && item.UserBelongUnitId > 0)
                {
                    if (item.FireUnitId.Equals(item.UserBelongUnitId))
                    {
                        var user = _repFireUnitUser.Get(item.UserId);
                        item.UserName = user != null ? user.Name : "";
                        item.UserPhone = user != null ? user.Account : "";
                    }
                    else
                    {
                        var user = _repSafeUnitUser.Get(item.UserId);
                        item.UserName = user != null ? user.Name : "";
                        item.UserPhone = user != null ? user.Account : "";
                    }
                }
            }

            return Task.FromResult(new PagedResultDto<GetBreakDownOutput>(tCount, list));
        }

        /// <summary>
        /// 获取设施故障详情
        /// </summary>
        /// <param name="breakDownId"></param>
        /// <returns></returns>
        public async Task<GetBreakDownInfoOutput> GetBreakDownInfo(int breakDownId)
        {
            var breakDown = await _repBreakDown.GetAsync(breakDownId);

            var output = new GetBreakDownInfoOutput()
            {
                Id = breakDown.Id,
                CreationTime = breakDown.CreationTime,
                DataId = breakDown.DataId,
                DispatchTime = breakDown.DispatchTime,
                FireUnitId = breakDown.FireUnitId,
                HandleStatus = breakDown.HandleStatus,
                ProblemRemark = breakDown.ProblemRemark,
                ProblemVoiceUrl = breakDown.ProblemVoiceUrl,
                SolutionRemark = breakDown.SolutionRemark,
                SolutionTime = breakDown.SolutionTime,
                SolutionWay = breakDown.SolutionWay,
                Source = breakDown.Source,
                UserBelongUnitId = breakDown.UserBelongUnitId,
                UserId = breakDown.UserId,
                VoiceLength = breakDown.VoiceLength
            };

            if (output.UserId > 0 && output.UserBelongUnitId > 0)
            {
                if (output.FireUnitId.Equals(output.UserBelongUnitId))
                {
                    var user = _repFireUnitUser.Get(output.UserId);
                    output.UserName = user != null ? user.Name : "";
                    output.UserPhone = user != null ? user.Account : "";
                }
                else
                {
                    var user = _repSafeUnitUser.Get(output.UserId);
                    output.UserName = user != null ? user.Name : "";
                    output.UserPhone = user != null ? user.Account : "";
                }
            }

            //巡查来源需显示图片
            if (output.Source == FaultSource.Patrol)
            {
                var photospath = _repPhotosPathSave.GetAll().Where(u => u.TableName.Equals("DataToPatrolDetail") && u.DataId == output.DataId).Select(u => u.PhotoPath).ToList();
                output.PatrolPhotosPath = photospath;
                foreach (var f in output.PatrolPhotosPath)
                {
                    output.PatrolPhotosBase64.Add(ImageHelper.ThumbImg(_hostingEnv.ContentRootPath + f.Replace("Src", "App_Data/Files")));
                }
            }

            return output;
        }

        /// <summary>
        /// 更新设施故障详情
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task UpdateBreakDownInfo(UpdateBreakDownInfoInput input)
        {
            var breakDown = await _repBreakDown.GetAsync(input.BreakDownId);
            breakDown.HandleStatus = input.HandleStatus;
            breakDown.SolutionWay = input.SolutionWay;
            breakDown.SolutionRemark = input.SolutionRemark;
            if (input.HandleStatus.Equals(HandleStatus.SelfHandle) || input.HandleStatus.Equals(HandleStatus.SafeResolving))
            {
                breakDown.DispatchTime = DateTime.Now;
            }
            if (input.HandleStatus.Equals(HandleStatus.SafeResolved) || input.HandleStatus.Equals(HandleStatus.Resolved))
            {
                breakDown.SolutionTime = DateTime.Now;
            }

            await _repBreakDown.UpdateAsync(breakDown);

            // 如果解决了物联网终端故障，则将对应物联网终端的状态设置为正常
            if (input.HandleStatus.Equals(HandleStatus.Resolved) && breakDown.Source.Equals(FaultSource.Terminal))
            {
                var fault = await _repFault.GetAsync(breakDown.DataId);
                if (fault != null)
                {
                    fault.SolutionTime = DateTime.Now;
                    await _repFault.UpdateAsync(fault);

                    var detector = await _repFireAlarmDetector.GetAsync(fault.FireAlarmDetectorId);
                    if (detector != null)
                    {
                        detector.State = FireAlarmDetectorState.Normal;
                        await _repFireAlarmDetector.UpdateAsync(detector);
                    }
                }
            }
        }

        /// <summary>
        /// 获取设施故障处理情况
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public Task<GetBreakDownTotalOutput> GetBreakDownTotal(GetBreakDownTotalInput input)
        {
            var breakDowns = _repBreakDown.GetAll().Where(u => u.FireUnitId == input.FireUnitId);
            GetBreakDownTotalOutput output = new GetBreakDownTotalOutput();

            output.DutyCount = breakDowns.Count(u => u.Source == FaultSource.Duty);
            output.PatrolCount = breakDowns.Count(u => u.Source == FaultSource.Patrol);
            output.TerminalCount = breakDowns.Count(u => u.Source == FaultSource.Terminal);

            output.UuResolveCount = breakDowns.Count(u => u.HandleStatus == HandleStatus.UnResolve);
            output.ResolvingCount = breakDowns.Count(u => u.HandleStatus == HandleStatus.SelfHandle || u.HandleStatus == HandleStatus.SafeResolving || u.HandleStatus == HandleStatus.SafeResolved);
            output.ResolvedCount = breakDowns.Count(u => u.HandleStatus == HandleStatus.Resolved);

            return Task.FromResult(output);
        }
    }
}
