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
        IRepository<Detector> _repDetector;
        IHostingEnvironment _hostingEnv;
        IRepository<SafeUnitUser> _repSafeUnitUser;
        IRepository<FireUnit> _fireUnitRep;
        IRepository<BreakDown> _breakDownRep;
        IRepository<FireUnitUser> _fireUnitAccountRepository;
        IRepository<DataToDutyProblem> _dataToDutyProblemRep;
        IRepository<PhotosPathSave> _photosPathSave;
        IRepository<SafeUnit> _safeUnit;
        IRepository<DataToPatrolDetailProblem> _patrolDetailProblem;
        IRepository<Fault> _Fault;
        IRepository<DataToDuty> _dutyRep;
        IRepository<DataToPatrol> _patrolRep;
        IRepository<DataToPatrolDetail> _patrolDetailRep;
        public BreakDownManager(
            IRepository<Detector> repDetector,
            IRepository<SafeUnitUser> repSafeUnitUser,
            IHostingEnvironment hostingEnv,
            IRepository<FireUnit> fireUnitRep,
            IRepository<BreakDown> breakDownRep,
            IRepository<DataToDutyProblem> dataToDutyProblemRep,
            IRepository<PhotosPathSave> photosPathSaveRep,
            IRepository<FireUnitUser> fireUnitAccountRepository,
            IRepository<SafeUnit> safeUnit,
            IRepository<DataToPatrolDetailProblem> patrolDetailProblem,
            IRepository<DataToDuty> dutyRep,
            IRepository<DataToPatrol> patrolRep,
            IRepository<DataToPatrolDetail> patrolDetailRep,
            IRepository<Fault> Fault
            )
        {
            _repDetector = repDetector;
            _repSafeUnitUser = repSafeUnitUser;
            _hostingEnv = hostingEnv;
            _fireUnitRep = fireUnitRep;
            _breakDownRep = breakDownRep;
            _dataToDutyProblemRep = dataToDutyProblemRep;
            _photosPathSave = photosPathSaveRep;
            _fireUnitAccountRepository = fireUnitAccountRepository;
            _safeUnit = safeUnit;
            _patrolDetailProblem = patrolDetailProblem;
            _dutyRep = dutyRep;
            _patrolRep = patrolRep;
            _patrolDetailRep = patrolDetailRep;
            _Fault = Fault;
        }

        /// <summary>
        /// 获取设施故障列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public Task<GetBreakDownPagingOutput> GetBreakDownlist(GetBreakDownInput input)
        {
            var breakdownlist = _breakDownRep.GetAll().Where(u => u.FireUnitId == input.FireUnitId&&u.HandleStatus==(byte)input.HandleStatus);
            var userlist = _fireUnitAccountRepository.GetAll();
            //值班故障
            var dutys = _breakDownRep.GetAll().Where(u => u.FireUnitId == input.FireUnitId);
            //处理维保叫修页面，还有问题来源的选项
            if (input.HandleStatus == HandleStatus.SafeResolved || input.HandleStatus == HandleStatus.SafeResolving)
            {
                breakdownlist = breakdownlist.Where(p => p.HandleStatus == (byte)input.HandleStatus);
                if(input.HandleStatus == HandleStatus.SafeResolving)
                    breakdownlist= breakdownlist.OrderByDescending(p=>p.DispatchTime);
                else if (input.HandleStatus == HandleStatus.SafeResolved)
                    breakdownlist = breakdownlist.OrderByDescending(p => p.SafeCompleteTime);
            }
            else
            {
                var expr = ExprExtension.True<BreakDown>()
                .IfAnd(input.Source != SourceType.UnKnow, item => item.Source == (byte)input.Source);
                breakdownlist = breakdownlist.Where(expr);
                if (input.HandleStatus == HandleStatus.SelfHandle)
                    breakdownlist = breakdownlist.OrderByDescending(p => p.DispatchTime);
                else if (input.HandleStatus == HandleStatus.UuResolve)
                    breakdownlist = breakdownlist.OrderByDescending(p => p.CreationTime);
                else if (input.HandleStatus == HandleStatus.Resolved)
                {
                    if (input.IsRequstBySafe)
                        breakdownlist = breakdownlist.Where(p => p.SolutionWay == 2);
                    breakdownlist = breakdownlist.OrderByDescending(p => p.SolutionTime);
                }
            }
            var lst = breakdownlist.Select(a=> new
            {
                A=a,
                B = new GetBreakDownOutput
                {
                    BreakDownId = a.Id,
                    Source = a.Source,
                    CreationTime = a.CreationTime.ToString("yyyy-MM-dd HH:mm"),
                    SolutionTime = a.SolutionTime.ToString("yyyy-MM-dd HH:mm"),
                    DispatchTime=a.DispatchTime.ToString("yyyy-MM-dd HH:mm"),
                    SafeCompleteTime=a.SafeCompleteTime.ToString("yyyy-MM-dd HH:mm")
                }
            }).ToList();
            foreach(var v in lst)
            {
                if (v.A.UserFrom == null)
                    continue;
                if (v.A.UserFrom.Equals("FireUnitUser"))
                {
                    var user = _fireUnitAccountRepository.FirstOrDefault(p => p.Id == v.A.UserId);
                    if (user != null)
                    {
                        v.B.UserName = user.Name;
                        v.B.Phone = user.Account;
                    }
                }else if (v.A.UserFrom.Equals("SafeUnitUser"))
                {
                    var user = _repSafeUnitUser.FirstOrDefault(p => p.Id == v.A.UserId);
                    if (user != null)
                    {
                        v.B.UserName = user.Name;
                        v.B.Phone = user.Account;
                    }
                }
            }
            var list = lst.Select(p => p.B);
            GetBreakDownPagingOutput output = new GetBreakDownPagingOutput()
            {
                TotalCount = list.Count(),
                BreakDownList = list.Skip(input.SkipCount).Take(input.MaxResultCount).ToList()
            };
            return Task.FromResult(output);
        }

        /// <summary>
        /// 获取设施故障详情
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<GetBreakDownInfoOutput> GetBreakDownInfo(GetBreakDownInfoInput input)
        {
            var breakdown = await _breakDownRep.SingleAsync(u => u.Id == input.BreakDownId);
            var user = _fireUnitAccountRepository.FirstOrDefault(u => u.Id == breakdown.UserId);
            
            GetBreakDownInfoOutput output = new GetBreakDownInfoOutput()
            {
                HandleStatus = breakdown.HandleStatus,
                Source = breakdown.Source,
                UserName = user==null?"":user.Name,
                Phone = user == null ? "" : user.Account,
                CreationTime = breakdown.CreationTime.ToString("yyyy-MM-dd HH:mm"),
                SolutionWay=breakdown.SolutionWay,
                Remark=breakdown.Remark,
                SolutionTime=breakdown.SolutionTime.ToString("yyyy-MM-dd HH:mm")
            };
            //值班来源
            if (breakdown.Source==1)
            {
                var dutyproblem = _dataToDutyProblemRep.FirstOrDefault(u=>u.Id==breakdown.DataId);
                var photospath = _photosPathSave.GetAll().Where(u => u.TableName.Equals("DataToDutyProblem") && u.DataId == dutyproblem.Id).Select(u=>u.PhotoPath).ToList();
                output.ProblemRemakeType = (byte)dutyproblem.ProblemRemarkType;
                output.RemakeText = dutyproblem.ProblemRemark;
                output.VoiceLength = dutyproblem.VoiceLength;
                output.PatrolPhotosPath = photospath;
            }
            output.PhotosBase64 = new List<string>();
            //巡查来源
            if (breakdown.Source == 2)
            {
                var patroldetailproblem = _patrolDetailProblem.FirstOrDefault(u => u.Id == breakdown.DataId);
                var photospath = _photosPathSave.GetAll().Where(u => u.TableName.Equals("DataToPatrolDetail") && u.DataId == patroldetailproblem.PatrolDetailId).Select(u => u.PhotoPath).ToList();
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
            if (breakdown.Source == 3)
            {
                var fault = _Fault.FirstOrDefault(u => u.Id == breakdown.DataId);
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
            try {
                var breakdown = await _breakDownRep.SingleAsync(u => u.Id == input.BreakDownId);
                DateTime now = DateTime.Now;
                //防火单位页面提交
                if (input.HandleStatus == HandleStatus.Resolving)
                {
                    breakdown.SolutionWay = input.SolutionWay;
                    breakdown.DispatchTime = now;
                    if (input.SolutionWay == 1)
                        breakdown.HandleStatus = (byte)HandleStatus.SelfHandle;
                    else if (input.SolutionWay == 2)
                        breakdown.HandleStatus = (byte)HandleStatus.SafeResolving;
                }else if(input.HandleStatus == HandleStatus.Resolved)
                {
                    breakdown.SolutionWay = input.SolutionWay;
                    breakdown.SolutionTime = now;
                    breakdown.HandleStatus = (byte)input.HandleStatus;
                    if(breakdown.Source== (byte)Common.Enum.SourceType.Terminal)
                    {
                        var fault = await _Fault.FirstOrDefaultAsync(p => p.Id == breakdown.DataId);
                        if (fault != null)
                        {
                            var detector = await _repDetector.FirstOrDefaultAsync(p => p.Id == fault.DetectorId);
                            if (detector != null)
                            {
                                if(detector.FaultNum>0)
                                    detector.FaultNum--;
                                await _repDetector.UpdateAsync(detector);
                            }
                        }
                    }
                }
                //维保单位页面提交
                else if (input.HandleStatus == HandleStatus.SafeResolving)
                {
                    breakdown.HandleStatus = (byte)input.HandleStatus;
                }
                else if (input.HandleStatus == HandleStatus.SafeResolved)
                {
                    breakdown.HandleStatus = (byte)input.HandleStatus;
                    breakdown.SafeCompleteTime = now;
                }

                breakdown.Remark = input.Remark;

                //if (input.HandleStatus != HandleStatus.UuResolve)
                //{
                //    breakdown.SolutionTime = DateTime.Now;
                    ////更新值班
                    //if (breakdown.Source == 1)
                    //{
                    //    var dutyproblem = _dataToDutyProblemRep.Single(u => u.Id == breakdown.DataId);
                    //    var duty = _dutyRep.Single(u => u.Id == dutyproblem.DutyId);
                    //    duty.DutyStatus = (byte)ProblemStatusType.Repaired;
                    //    await _dutyRep.UpdateAsync(duty);
                    //}
                    ////更新巡查
                    //if (breakdown.Source == 2)
                    //{
                    //    var patroldetailproblem = _patrolDetailProblem.Single(u => u.Id == breakdown.DataId);
                    //    var patroldetail = _patrolDetailRep.Single(u => u.Id == patroldetailproblem.PatrolDetailId);
                    //    patroldetail.PatrolStatus = (byte)ProblemStatusType.Repaired;
                    //    _patrolDetailRep.InsertOrUpdateAndGetId(patroldetail);
                    //    if (_patrolDetailRep.GetAll().Where(u => u.PatrolId == patroldetail.PatrolId && u.PatrolStatus == (byte)ProblemStatusType.Repaired && u.Id != patroldetail.Id).Count() == 0)
                    //    {
                    //        var patrol = _patrolRep.Single(u => u.Id == patroldetail.PatrolId);
                    //        patrol.PatrolStatus = (byte)ProblemStatusType.Repaired;
                    //        _patrolRep.Update(patrol);
                    //    }
                    //}
                    ////物联终端来源
                    //if (breakdown.Source == 3)
                    //{
                    //    var fault = _Fault.FirstOrDefault(u => u.Id == breakdown.DataId);
                    //    fault.ProcessState = 1;
                    //    _Fault.Update(fault);
                    //}


                //}
                await _breakDownRep.UpdateAsync(breakdown);
                var fireunit =await _fireUnitRep.FirstOrDefaultAsync(p => p.Id == breakdown.FireUnitId);
                DataApi.UpdateEvent(new GovFire.Dto.EventDto()
                {
                    id = breakdown.Id.ToString(),
                    state = breakdown.HandleStatus == 3 ? "1" : "0",
                    createtime = breakdown.CreationTime.ToString("yyyy-MM-dd HH:mm:ss"),
                    donetime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    eventcontent = breakdown.Remark,
                    eventtype = BreakDownWords.GetSource(breakdown.Source),
                    firecompany = fireunit==null?"": fireunit.Name,
                    lat = fireunit == null ? "" : fireunit.Lat.ToString(),
                    lon = fireunit == null ? "" : fireunit.Lng.ToString(),
                    fireUnitId = ""
                });

                return output;
            } catch (Exception e) {
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
            var breakdownlist = _breakDownRep.GetAll().Where(u => u.FireUnitId == input.FireUnitId);
            GetBreakDownTotalOutput output = new GetBreakDownTotalOutput();
            output.DutyCount = breakdownlist.Where(u => u.Source == (byte)SourceType.Duty).Count();
            output.PatrolCount = breakdownlist.Where(u => u.Source == (byte)SourceType.Patrol).Count();
            output.TerminalCount = breakdownlist.Where(u => u.Source == (byte)SourceType.Terminal).Count();

            output.UuResolveCount= breakdownlist.Where(u => u.HandleStatus == (byte)HandleStatus.UuResolve).Count();
            output.ResolvingCount = breakdownlist.Where(u => u.HandleStatus == (byte)HandleStatus.Resolving).Count();
            output.ResolvedCount = breakdownlist.Where(u => u.HandleStatus == (byte)HandleStatus.Resolved).Count();

            return Task.FromResult(output);
        }
    }
}
