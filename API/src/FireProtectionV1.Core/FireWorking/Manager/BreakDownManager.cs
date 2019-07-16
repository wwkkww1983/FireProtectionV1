using Abp.Domain.Repositories;
using FireProtectionV1.Common.DBContext;
using FireProtectionV1.Common.Enum;
using FireProtectionV1.Enterprise.Model;
using FireProtectionV1.FireWorking.Dto;
using FireProtectionV1.FireWorking.Model;
using FireProtectionV1.User.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireProtectionV1.FireWorking.Manager
{
    public class BreakDownManager : IBreakDownManager
    {
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
            var expr = ExprExtension.True<BreakDown>()
                .IfAnd(input.Source != SourceType.UnKnow, item => item.Source == (byte)input.Source);
            breakdownlist = breakdownlist.Where(expr);
            var list = from a in breakdownlist
                         join b in userlist on a.UserId equals b.Id
                         orderby a.CreationTime 
                         select new GetBreakDownOutput
                         {
                             BreakDownId=a.Id,
                             Source = a.Source,
                             UserName = b.Name,
                             Phone = b.Account,
                             CreationTime = a.CreationTime.ToString("yyyy-MM-dd hh:mm"),
                             SolutionTime = a.SolutionTime.ToString("yyyy-MM-dd hh:mm")
                         };
            if (input.HandleStatus == HandleStatus.Resolving || input.HandleStatus == HandleStatus.Resolved)
            {
                list=list.OrderByDescending(u => u.SolutionTime);
            }
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
                UserName = user.Name,
                Phone = user.Account,
                CreationTime = breakdown.CreationTime.ToString("yyyy-MM-dd hh:mm"),
                SolutionWay=breakdown.SolutionWay,
                Remark=breakdown.Remark,
                SolutionTime=breakdown.SolutionTime.ToString("yyyy-MM-dd hh:mm")
            };
            //值班来源
            if (breakdown.Source==1)
            {
                var dutyproblem = _dataToDutyProblemRep.FirstOrDefault(u=>u.Id==breakdown.DataId);
                var photospath = _photosPathSave.GetAll().Where(u => u.TableName.Equals("DataToDutyProblem") && u.DataId == dutyproblem.Id).Select(u=>u.PhotoPath).ToList();
                output.ProblemRemakeType = (byte)dutyproblem.ProblemRemarkType;
                output.RemakeText = dutyproblem.ProblemRemark;
                output.PatrolPhotosPath = photospath;
            }
            //巡查来源
            if (breakdown.Source == 2)
            {
                var patroldetailproblem = _patrolDetailProblem.FirstOrDefault(u => u.Id == breakdown.DataId);
                var photospath = _photosPathSave.GetAll().Where(u => u.TableName.Equals("DataToPatrolDetailProblem") && u.DataId == patroldetailproblem.Id).Select(u => u.PhotoPath).ToList();
                output.ProblemRemakeType = (byte)patroldetailproblem.ProblemRemarkType;
                output.RemakeText = patroldetailproblem.ProblemRemark;
                output.PatrolPhotosPath = photospath;
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
                breakdown.HandleStatus = (byte)input.HandleStatus;
                breakdown.SolutionWay = input.SolutionWay;
                breakdown.Remark = input.Remark;

                if (input.HandleStatus != HandleStatus.UuResolve)
                {
                    breakdown.SolutionTime = DateTime.Now;
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


                }
                await _breakDownRep.UpdateAsync(breakdown);
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
