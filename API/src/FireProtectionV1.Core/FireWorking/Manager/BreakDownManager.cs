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
        IRepository<DataToDutyProblem> _repDataToDutyProblem;
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
            IRepository<DataToDutyProblem> repDataToDutyProblem,
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
            _repDataToDutyProblem = repDataToDutyProblem;
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
                            SafeCompleteTime = a.SafeCompleteTime,
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

            return Task.FromResult(new PagedResultDto<GetBreakDownOutput>(tCount, list));
        }

        /// <summary>
        /// 获取设施故障列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        //public Task<GetBreakDownPagingOutput> GetBreakDownlist(GetBreakDownInput input)
        //{
        //    var breakdownlist = _breakDownRep.GetAll().Where(u => u.FireUnitId == input.FireUnitId && u.HandleStatus == input.HandleStatus);
        //    var userlist = _fireUnitAccountRepository.GetAll();
        //    //值班故障
        //    var dutys = _breakDownRep.GetAll().Where(u => u.FireUnitId == input.FireUnitId);
        //    //处理维保叫修页面，还有问题来源的选项
        //    if (input.HandleStatus == HandleStatus.SafeResolved || input.HandleStatus == HandleStatus.SafeResolving)
        //    {
        //        breakdownlist = breakdownlist.Where(p => p.HandleStatus == input.HandleStatus);
        //        if (input.HandleStatus == HandleStatus.SafeResolving)
        //            breakdownlist = breakdownlist.OrderByDescending(p => p.DispatchTime);
        //        else if (input.HandleStatus == HandleStatus.SafeResolved)
        //            breakdownlist = breakdownlist.OrderByDescending(p => p.SafeCompleteTime);
        //    }
        //    else
        //    {
        //        var expr = ExprExtension.True<BreakDown>()
        //        .IfAnd(input.Source != FaultSource.UnKnow, item => item.Source == input.Source);

        //        breakdownlist = breakdownlist.Where(expr);

        //        if (input.HandleStatus == HandleStatus.SelfHandle)
        //            breakdownlist = breakdownlist.OrderByDescending(p => p.DispatchTime);
        //        else if (input.HandleStatus == HandleStatus.UnResolve)
        //            breakdownlist = breakdownlist.OrderByDescending(p => p.CreationTime);
        //        else if (input.HandleStatus == HandleStatus.Resolved)
        //        {
        //            if (input.IsRequstBySafe)
        //                breakdownlist = breakdownlist.Where(p => p.SolutionWay == HandleChannel.Maintenance);
        //            breakdownlist = breakdownlist.OrderByDescending(p => p.SolutionTime);
        //        }
        //    }
        //    var lst = breakdownlist.Select(a => new
        //    {
        //        A = a,
        //        B = new GetBreakDownOutput
        //        {
        //            BreakDownId = a.Id,
        //            Source = a.Source,
        //            CreationTime = a.CreationTime.ToString("yyyy-MM-dd HH:mm"),
        //            SolutionTime = a.SolutionTime.ToString("yyyy-MM-dd HH:mm"),
        //            DispatchTime = a.DispatchTime.ToString("yyyy-MM-dd HH:mm"),
        //            SafeCompleteTime = a.SafeCompleteTime.ToString("yyyy-MM-dd HH:mm")
        //        }
        //    }).ToList();
        //    foreach (var v in lst)
        //    {
        //        var user = _fireUnitAccountRepository.FirstOrDefault(p => p.Id == v.A.UserId);
        //        if (user != null)
        //        {
        //            v.B.UserName = user.Name;
        //            v.B.Phone = user.Account;
        //        }
        //    }
        //    var list = lst.Select(p => p.B);
        //    GetBreakDownPagingOutput output = new GetBreakDownPagingOutput()
        //    {
        //        TotalCount = list.Count(),
        //        BreakDownList = list.Skip(input.SkipCount).Take(input.MaxResultCount).ToList()
        //    };
        //    return Task.FromResult(output);
        //}

        /// <summary>
        /// 获取设施故障详情
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<GetBreakDownInfoOutput> GetBreakDownInfo(GetBreakDownInfoInput input)
        {
            var breakdown = await _repBreakDown.SingleAsync(u => u.Id == input.BreakDownId);
            var user = _repFireUnitUser.FirstOrDefault(u => u.Id == breakdown.UserId);

            GetBreakDownInfoOutput output = new GetBreakDownInfoOutput()
            {
                HandleStatus = breakdown.HandleStatus,
                Source = breakdown.Source,
                UserName = user == null ? "" : user.Name,
                Phone = user == null ? "" : user.Account,
                CreationTime = breakdown.CreationTime.ToString("yyyy-MM-dd HH:mm"),
                SolutionWay = breakdown.SolutionWay,
                Remark = breakdown.Remark,
                SolutionTime = breakdown.SolutionTime.ToString("yyyy-MM-dd HH:mm")
            };
            //值班来源
            if (breakdown.Source == FaultSource.Duty)
            {
                var dutyproblem = _repDataToDutyProblem.FirstOrDefault(u => u.Id == breakdown.DataId);
                var photospath = _repPhotosPathSave.GetAll().Where(u => u.TableName.Equals("DataToDutyProblem") && u.DataId == dutyproblem.Id).Select(u => u.PhotoPath).ToList();
                output.ProblemRemakeType = (byte)dutyproblem.ProblemRemarkType;
                output.RemakeText = dutyproblem.ProblemRemark;
                output.VoiceLength = dutyproblem.VoiceLength;
                output.PatrolPhotosPath = photospath;
            }
            output.PhotosBase64 = new List<string>();
            //巡查来源
            if (breakdown.Source == FaultSource.Patrol)
            {
                var patroldetailproblem = _repDataToPatrolDetailProblem.FirstOrDefault(u => u.Id == breakdown.DataId);
                var photospath = _repPhotosPathSave.GetAll().Where(u => u.TableName.Equals("DataToPatrolDetail") && u.DataId == patroldetailproblem.PatrolDetailId).Select(u => u.PhotoPath).ToList();
                output.ProblemRemakeType = (byte)patroldetailproblem.ProblemRemarkType;
                output.RemakeText = patroldetailproblem.ProblemRemark;
                output.VoiceLength = patroldetailproblem.VoiceLength;
                output.PatrolPhotosPath = photospath;
                foreach (var f in output.PatrolPhotosPath)
                {
                    output.PhotosBase64.Add(ImageHelper.ThumbImg(_hostingEnv.ContentRootPath + f.Replace("Src", "App_Data/Files")));
                }
            }
            //物联终端来源
            if (breakdown.Source == FaultSource.Terminal)
            {
                var fault = _repFault.FirstOrDefault(u => u.Id == breakdown.DataId);
                output.ProblemRemakeType = 1;
                output.RemakeText = fault.FaultRemark;
            }
            return output;
        }


        /// <summary>
        /// 更新设施故障详情
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<SuccessOutput> UpdateBreakDownInfo(UpdateBreakDownInfoInput input)
        {
            SuccessOutput output = new SuccessOutput() { Success = true };
            try
            {
                var breakdown = await _repBreakDown.SingleAsync(u => u.Id == input.BreakDownId);
                DateTime now = DateTime.Now;
                //防火单位页面提交
                if (input.HandleStatus == HandleStatus.UnResolve)
                {
                    breakdown.SolutionWay = input.SolutionWay;
                    breakdown.DispatchTime = now;
                    if (input.SolutionWay == HandleChannel.Self)
                        breakdown.HandleStatus = HandleStatus.SelfHandle;
                    else if (input.SolutionWay == HandleChannel.Maintenance)
                        breakdown.HandleStatus = HandleStatus.SafeResolving;
                }
                else if (input.HandleStatus == HandleStatus.Resolved)
                {
                    breakdown.SolutionWay = input.SolutionWay;
                    breakdown.SolutionTime = now;
                    breakdown.HandleStatus = input.HandleStatus;
                    if (breakdown.Source == FaultSource.Terminal)
                    {
                        var fault = await _repFault.FirstOrDefaultAsync(p => p.Id == breakdown.DataId);
                        if (fault != null)
                        {
                            var detector = await _repFireAlarmDetector.FirstOrDefaultAsync(p => p.Id == fault.FireAlarmDetectorId);
                            if (detector != null)
                            {
                                if (detector.FaultNum > 0)
                                {
                                    detector.FaultNum--;
                                    detector.State = FireAlarmDetectorState.Normal;
                                    detector.LastFaultId = 0;
                                }
                                await _repFireAlarmDetector.UpdateAsync(detector);
                            }
                        }
                    }
                }
                //维保单位页面提交
                else if (input.HandleStatus == HandleStatus.SafeResolving)
                {
                    breakdown.HandleStatus = input.HandleStatus;
                }
                else if (input.HandleStatus == HandleStatus.SafeResolved)
                {
                    breakdown.HandleStatus = input.HandleStatus;
                    breakdown.SafeCompleteTime = now;
                }

                breakdown.Remark = input.Remark;

                await _repBreakDown.UpdateAsync(breakdown);
                var fireunit = await _repFireUnit.FirstOrDefaultAsync(p => p.Id == breakdown.FireUnitId);
                DataApi.UpdateEvent(new GovFire.Dto.EventDto()
                {
                    id = breakdown.Id.ToString(),
                    state = breakdown.HandleStatus == HandleStatus.Resolved ? "1" : "0",
                    createtime = breakdown.CreationTime.ToString("yyyy-MM-dd HH:mm:ss"),
                    donetime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    eventcontent = breakdown.Remark,
                    eventtype = BreakDownWords.GetSource((byte)breakdown.Source),
                    firecompany = fireunit == null ? "" : fireunit.Name,
                    lat = fireunit == null ? "" : fireunit.Lat.ToString(),
                    lon = fireunit == null ? "" : fireunit.Lng.ToString(),
                    fireUnitId = ""
                });

                return output;
            }
            catch (Exception e)
            {
                output.Success = false;
                output.FailCause = e.Message;
                return output;
            }

        }

        /// <summary>
        /// 获取设施故障处理情况
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public Task<GetBreakDownTotalOutput> GetBreakDownTotal(GetBreakDownTotalInput input)
        {
            var breakdownlist = _repBreakDown.GetAll().Where(u => u.FireUnitId == input.FireUnitId);
            GetBreakDownTotalOutput output = new GetBreakDownTotalOutput();
            output.DutyCount = breakdownlist.Where(u => u.Source == FaultSource.Duty).Count();
            output.PatrolCount = breakdownlist.Where(u => u.Source == FaultSource.Patrol).Count();
            output.TerminalCount = breakdownlist.Where(u => u.Source == FaultSource.Terminal).Count();

            output.UuResolveCount = breakdownlist.Where(u => u.HandleStatus == HandleStatus.UnResolve).Count();
            output.ResolvingCount = breakdownlist.Where(u => u.HandleStatus == HandleStatus.SelfHandle || u.HandleStatus == HandleStatus.SafeResolving || u.HandleStatus == HandleStatus.SafeResolved).Count();
            output.ResolvedCount = breakdownlist.Where(u => u.HandleStatus == HandleStatus.Resolved).Count();

            return Task.FromResult(output);
        }
    }
}
