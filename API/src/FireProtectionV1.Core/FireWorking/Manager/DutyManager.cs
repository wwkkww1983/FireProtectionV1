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
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireProtectionV1.FireWorking.Manager
{
    public class DutyManager : IDutyManager
    {
        IHostingEnvironment _hostingEnv;
        IRepository<FireUnit> _repFireUnit;
        IRepository<DataToDuty> _repDataToDuty;
        IRepository<FireUnitUser> _repFireUnitUser;
        IRepository<PhotosPathSave> _repPhotosPathSave;
        IRepository<BreakDown> _repBreakDown;
        public DutyManager(
            IHostingEnvironment hostingEnv,
            IRepository<FireUnit> repFireUnit,
            IRepository<DataToDuty> repDataToDuty,
            IRepository<FireUnitUser> repFireUnitUser,
            IRepository<PhotosPathSave> repPhotosPathSave,
            IRepository<BreakDown> repBreakDown
            )
        {
            _hostingEnv = hostingEnv;
            _repFireUnit = repFireUnit;
            _repDataToDuty = repDataToDuty;
            _repFireUnitUser = repFireUnitUser;
            _repPhotosPathSave = repPhotosPathSave;
            _repBreakDown = repBreakDown;
        }

        public IQueryable<DataToDuty> GetDutyDataAll()
        {
            return _repDataToDuty.GetAll();
        }
        public IQueryable<DataToDuty> GetDutyDataMonth(int year, int month)
        {
            return _repDataToDuty.GetAll().Where(p => p.CreationTime.Year == year && p.CreationTime.Month == month);
        }
        public IQueryable<DataToDuty> GetDutyDataDuration(DateTime start, DateTime end)
        {
            return _repDataToDuty.GetAll().Where(p => p.CreationTime >= start && p.CreationTime <= end);
        }
        public IQueryable<FireUnitManualOuput> GetDutyFireUnitsAll(string filterName = null)
        {
            return from a in _repFireUnit.GetAll().Where(p => string.IsNullOrEmpty(filterName) ? true : p.Name.Contains(filterName))
                   join b in _repDataToDuty.GetAll().GroupBy(p => p.FireUnitId).Select(p => new
                   {
                       FireUnitId = p.Key,
                       LastTime = p.Max(p1 => p1.CreationTime),
                       Day30Count = p.Where(p1 => p1.CreationTime >= DateTime.Now.Date.AddDays(-30)).Count()
                   })
                   on a.Id equals b.FireUnitId into g
                   from b2 in g.DefaultIfEmpty()
                   select new FireUnitManualOuput()
                   {
                       FireUnitId = a.Id,
                       FireUnitName = a.Name,
                       LastTime = b2 == null ? "" : b2.LastTime.ToString("yyyy-MM-dd"),
                       Recent30DayCount = b2 == null ? 0 : b2.Day30Count
                   };
        }
        public IQueryable<FireUnitManualOuput> GetNoDuty1DayFireUnits()
        {
            DateTime now = DateTime.Now;
            var workFireUnits = from a in _repDataToDuty.GetAll().Where(p => p.CreationTime > now.Date.AddDays(-1))
                                    .GroupBy(p => p.FireUnitId).Select(p => p.Key).ToList()
                                join b in _repFireUnit.GetAll()
                                on a equals b.Id
                                select b;

            var noWorkFireUnits = _repFireUnit.GetAll().Except(workFireUnits.ToList());
            var query = from a in noWorkFireUnits
                        join b in _repDataToDuty.GetAll().GroupBy(p => p.FireUnitId).Select(p => new
                        {
                            FireUnitId = p.Key,
                            LastTime = p.Max(p1 => p1.CreationTime),
                            Day30Count = p.Where(p1 => p1.CreationTime >= now.Date.AddDays(-30)).Count()
                        })
                        on a.Id equals b.FireUnitId into g
                        from b2 in g.DefaultIfEmpty()
                        select new FireUnitManualOuput()
                        {
                            FireUnitId = a.Id,
                            FireUnitName = a.Name,
                            LastTime = b2 == null ? "" : b2.LastTime.ToString("yyyy-MM-dd"),
                            Recent30DayCount = b2 == null ? 0 : b2.Day30Count
                        };
            return query;
        }

        /// <summary>
        /// 获取值班记录列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public Task<PagedResultDto<GetDataDutyOutput>> GetDutylist(GetDataDutyInput input, PagedResultRequestDto dto)
        {
            var dutys = _repDataToDuty.GetAll().Where(item => item.FireUnitId.Equals(input.FireUnitId));
            if (input.DutyStatus > 0)
            {
                dutys = dutys.Where(item => item.Status.Equals(input.DutyStatus));
            }
            var users = _repFireUnitUser.GetAll().Where(item => item.FireUnitID.Equals(input.FireUnitId));

            var query = from a in dutys
                        join b in users on a.UserId equals b.Id into result1
                        from a_b in result1.DefaultIfEmpty()
                        select new GetDataDutyOutput()
                        {
                            CreationTime = a.CreationTime,
                            DutyId = a.Id,
                            DutyStatus = a.Status,
                            DutyUserName = a_b != null ? a_b.Name : "",
                            DutyUserPhone = a_b != null ? a_b.Account : ""
                        };

            return Task.FromResult(new PagedResultDto<GetDataDutyOutput>()
            {
                Items = query.OrderByDescending(item => item.CreationTime).Skip(dto.SkipCount).Take(dto.MaxResultCount).ToList(),
                TotalCount = query.Count()
            });
        }
        /// <summary>
        /// 获取值班记录详情
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<GetDataDutyInfoOutput> GetDutyInfo(int dutyId)
        {
            var duty = await _repDataToDuty.GetAsync(dutyId);

            var output = new GetDataDutyInfoOutput()
            {
                CreationTime = duty.CreationTime,
                DutyPhtosPath = _repPhotosPathSave.GetAll().Where(item => item.TableName.Equals("DataToDuty") && item.DataId.Equals(dutyId)).Select(item => item.PhotoPath).ToList(),
                FireUnitId = duty.FireUnitId,
                Id = duty.Id,
                UserId = duty.UserId,
                Status = duty.Status
            };

            var user = await _repFireUnitUser.GetAsync(duty.UserId);
            output.DutyUserName = user != null ? user.Name : "";
            output.DutyUserPhone = user != null ? user.Account : "";

            foreach (var f in output.DutyPhtosPath)
            {
                output.DutyPhotosBase64.Add(ImageHelper.ThumbImg(_hostingEnv.ContentRootPath + f.Replace("Src", "App_Data/Files")));
            }

            if (output.Status != DutyOrPatrolStatus.Normal)
            {
                var breakDown = await _repBreakDown.FirstOrDefaultAsync(item => item.DataId.Equals(output.Id));
                if (breakDown != null)
                {
                    output.ProblemRemark = breakDown.ProblemRemark;
                    output.ProblemVoiceUrl = breakDown.ProblemVoiceUrl;
                    output.VoiceLength = breakDown.VoiceLength;
                    output.ProblemPhtosPath = _repPhotosPathSave.GetAll().Where(item => item.TableName.Equals("DataToDuty_Problem") && item.DataId.Equals(dutyId)).Select(item => item.PhotoPath).ToList();
                    foreach (var f in output.ProblemPhtosPath)
                    {
                        output.ProblemPhotosBase64.Add(ImageHelper.ThumbImg(_hostingEnv.ContentRootPath + f.Replace("Src", "App_Data/Files")));
                    }
                }
            }

            return output;
        }
        /// <summary>
        /// 新增值班记录
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task AddDutyInfo(AddDataDutyInfoInput input)
        {
            var dutyId = await _repDataToDuty.InsertAndGetIdAsync(new DataToDuty()
            {
                FireUnitId = input.FireUnitId,
                UserId = input.UserId,
                Status = input.Status
            });

            // 保存值班记录照片
            string tableName = "DataToDuty";
            string path = _hostingEnv.ContentRootPath + $@"/App_Data/Files/Photos/DataToDuty/";
            if (input.DutyPicture1 != null)
            {
                string dutyPicture1_Name = await SaveFileHelper.SaveFile(input.DutyPicture1, path);
                string photopath = "/Src/Photos/DataToDuty/" + dutyPicture1_Name;

                await _repPhotosPathSave.InsertAsync(new PhotosPathSave()
                {
                    TableName = tableName,
                    DataId = dutyId,
                    PhotoPath = photopath
                });
            }
            if (input.DutyPicture2 != null)
            {
                string dutyPicture2_Name = await SaveFileHelper.SaveFile(input.DutyPicture2, path);
                string photopath = "/Src/Photos/DataToDuty/" + dutyPicture2_Name;

                await _repPhotosPathSave.InsertAsync(new PhotosPathSave()
                {
                    TableName = tableName,
                    DataId = dutyId,
                    PhotoPath = photopath
                });
            }
            if (input.DutyPicture3 != null)
            {
                string dutyPicture3_Name = await SaveFileHelper.SaveFile(input.DutyPicture3, path);
                string photopath = "/Src/Photos/DataToDuty/" + dutyPicture3_Name;

                await _repPhotosPathSave.InsertAsync(new PhotosPathSave()
                {
                    TableName = tableName,
                    DataId = dutyId,
                    PhotoPath = photopath
                });
            }

            // 如果存在问题，则向设施故障表中写入数据
            if (input.Status != DutyOrPatrolStatus.Normal)
            {
                BreakDown breakDown = new BreakDown()
                {
                    DataId = dutyId,
                    FireUnitId = input.FireUnitId,
                    ProblemRemark = input.ProblemRemark,
                    Source = FaultSource.Duty,
                    UserId = input.UserId,
                    UserBelongUnitId = input.FireUnitId
                };


                tableName = "DataToDuty_Problem";
                if (input.ProblemPicture1 != null)
                {
                    string problemPicture1_Name = await SaveFileHelper.SaveFile(input.ProblemPicture1, path);
                    string photopath = "/Src/Photos/DataToDuty/" + problemPicture1_Name;

                    await _repPhotosPathSave.InsertAsync(new PhotosPathSave()
                    {
                        TableName = tableName,
                        DataId = dutyId,
                        PhotoPath = photopath
                    });
                }
                if (input.ProblemPicture2 != null)
                {
                    string problemPicture2_Name = await SaveFileHelper.SaveFile(input.ProblemPicture2, path);
                    string photopath = "/Src/Photos/DataToDuty/" + problemPicture2_Name;

                    await _repPhotosPathSave.InsertAsync(new PhotosPathSave()
                    {
                        TableName = tableName,
                        DataId = dutyId,
                        PhotoPath = photopath
                    });
                }
                if (input.ProblemPicture3 != null)
                {
                    string problemPicture3_Name = await SaveFileHelper.SaveFile(input.ProblemPicture3, path);
                    string photopath = "/Src/Photos/DataToDuty/" + problemPicture3_Name;

                    await _repPhotosPathSave.InsertAsync(new PhotosPathSave()
                    {
                        TableName = tableName,
                        DataId = dutyId,
                        PhotoPath = photopath
                    });
                }

                if (input.ProblemVoice != null)
                {
                    string voicePath = _hostingEnv.ContentRootPath + $@"/App_Data/Files/Voices/DataToDuty/";
                    string voiceName = await SaveFileHelper.SaveFile(input.ProblemVoice, voicePath);

                    breakDown.ProblemVoiceUrl = "/Src/Voices/DataToDuty/" + voiceName;
                    breakDown.VoiceLength = input.VoiceLength;
                }

                if (input.Status == DutyOrPatrolStatus.Repaired)
                {
                    breakDown.HandleStatus = HandleStatus.Resolved;
                    breakDown.SolutionTime = DateTime.Now;
                    breakDown.SolutionWay = HandleChannel.Self;
                }
                else
                {
                    breakDown.HandleStatus = HandleStatus.UnResolve;
                }

                await _repBreakDown.InsertAsync(breakDown);
            }
        }
        /// <summary>
        /// 获取值班记录日历列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public Task<List<GetDataForCalendarOutput>> GetDutylistForCalendar(GetDataForCalendarInput input)
        {
            int year = DateTime.Now.Year;
            int month = DateTime.Now.Month;
            if (input.CalendarDate.HasValue)
            {
                year = input.CalendarDate.Value.Year;
                month = input.CalendarDate.Value.Month;
            }

            var dataDutys = _repDataToDuty.GetAll().Where(item => item.FireUnitId.Equals(input.FireUnitId) && item.CreationTime.Year.Equals(year) && item.CreationTime.Month.Equals(month));

            var query = from a in dataDutys
                        select new GetDataForCalendarOutput()
                        {
                            Id = a.Id,
                            CreationTime = a.CreationTime.ToString("yyyy-MM-dd"),
                            Status = a.Status
                        };

            // 一天可能有多条数据，同一天中只取Status最大的那一条
            return Task.FromResult(query.GroupBy(item => item.CreationTime).Select(item => item.OrderByDescending(d => d.Status)).FirstOrDefault().ToList());
        }
        /// <summary>
        /// 获取值班记录状态统计
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public Task<GetDutyStatusTotalOutput> GetDutyStateTotal(int fireUnitId)
        {
            var query = _repDataToDuty.GetAll().Where(item=>item.FireUnitId.Equals(fireUnitId));
            GetDutyStatusTotalOutput output = new GetDutyStatusTotalOutput()
            {
                NormalCount = query.Count(item=>item.Status.Equals(DutyOrPatrolStatus.Normal)),
                GreenFaultCount = query.Count(item => item.Status.Equals(DutyOrPatrolStatus.Repaired)),
                RedFaultCount = query.Count(item => item.Status.Equals(DutyOrPatrolStatus.DisRepaired))
            };
            return Task.FromResult(output);
        }

        /// <summary>
        /// Web获取值班记录详情
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        //public async Task<List<GetDataDutyInfoOutput>> GetDutyInfoForWeb(GetDataDutyInfoForWebInput input)
        //{
        //    var dutys = _repDataToDuty.GetAll().Where(item => item.FireUnitId.Equals(input.FireUnitId) && item.CreationTime.ToShortDateString().Equals(input.Date.ToShortDateString()));
        //    var users = _repFireUnitUser.GetAll().Where(item => item.FireUnitID.Equals(input.FireUnitId));
        //    var photos = _repPhotosPathSave.GetAll();

        //    var query = from a in dutys
        //                join b in users on a.UserId equals b.Id into result1
        //                from a_b in result1.DefaultIfEmpty()
        //                select new GetDataDutyInfoOutput()
        //                {
        //                    CreationTime = a.CreationTime,
        //                    DutyPhtosPath = photos.Where(item => item.TableName.Equals("DataToDuty") && item.DataId.Equals(a.Id)).Select(item => item.PhotoPath).ToList(),
        //                    FireUnitId = a.FireUnitId,
        //                    Id = a.Id,
        //                    UserId = a.UserId,
        //                    Status = a.Status,
        //                    DutyUserName = a_b != null ? a_b.Name : "",
        //                    DutyUserPhone = a_b != null ? a_b.Account : "",
        //                };

        //    List<GetDataDutyInfoOutput> lstOutput = query.OrderBy(item => item.CreationTime).ToList();

        //    foreach (var item in lstOutput)
        //    {
        //        foreach (var f in item.DutyPhtosPath)
        //        {
        //            item.DutyPhotosBase64.Add(ImageHelper.ThumbImg(_hostingEnv.ContentRootPath + f.Replace("Src", "App_Data/Files")));
        //        }
        //        if (item.Status != DutyOrPatrolStatus.Normal)
        //        {
        //            var breakDown = await _repBreakDown.FirstOrDefaultAsync(d => d.DataId.Equals(item.Id));
        //            if (breakDown != null)
        //            {
        //                item.ProblemRemark = breakDown.ProblemRemark;
        //                item.ProblemVoiceUrl = breakDown.ProblemVoiceUrl;
        //                item.VoiceLength = breakDown.VoiceLength;
        //                item.ProblemPhtosPath = photos.Where(d => d.TableName.Equals("DataToDuty_Problem") && d.DataId.Equals(item.Id)).Select(d => d.PhotoPath).ToList();
        //                foreach (var f in item.ProblemPhtosPath)
        //                {
        //                    item.ProblemPhotosBase64.Add(ImageHelper.ThumbImg(_hostingEnv.ContentRootPath + f.Replace("Src", "App_Data/Files")));
        //                }
        //            }
        //        }
        //    }

        //    return lstOutput;
        //}
    }
}
