using Abp.Domain.Repositories;
using FireProtectionV1.Enterprise.Model;
using FireProtectionV1.FireWorking.Dto;
using FireProtectionV1.FireWorking.Model;
using GovFire;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireProtectionV1.FireWorking.Manager
{
    public class FaultManager: IFaultManager
    {
        IRepository<Detector> _repDetector;
        IRepository<BreakDown> _repBreakDown;
        IRepository<FireUnit> _fireUnitRep;
        IRepository<Fault> _faultRep;
        IDeviceManager _deviceManager;
        public FaultManager(
            IRepository<Detector> repDetector,
            IRepository<BreakDown> repBreakDown,
            IDeviceManager deviceManager,
            IRepository<FireUnit> fireUnitRep,
            IRepository<Fault> faultRep
            )
        {
            _repDetector = repDetector;
            _repBreakDown = repBreakDown;
            _deviceManager = deviceManager;
            _fireUnitRep = fireUnitRep;
            _faultRep = faultRep;
        }
        public async Task<AddDataOutput> AddNewFault(AddNewFaultInput input)
        {
            Detector detector = _deviceManager.GetDetector(input.Identify, input.Origin);
            if (detector == null)
            {
                return new AddDataOutput()
                {
                    IsDetectorExit = false
                };
            }
            var id= _faultRep.InsertAndGetId(new Fault()
            {
                FireUnitId = detector.FireUnitId,
                DetectorId = detector.Id,
                FaultRemark=input.FaultRemark,
                ProcessState=(byte)Common.Enum.HandleStatus.UuResolve
            });
            detector.FaultNum++;
            detector.LastFaultId = id;
            await _repDetector.UpdateAsync(detector);

            var breakdown = new BreakDown()
            {
                DataId = id,
                FireUnitId = detector.FireUnitId,
                HandleStatus = (byte)Common.Enum.HandleStatus.UuResolve,
                Remark = input.FaultRemark,
                Source = (byte)Common.Enum.SourceType.Terminal
            };
            var breakdownId=await _repBreakDown.InsertAndGetIdAsync(breakdown);
            var detectorType = await _deviceManager.GetDetectorTypeAsync(detector.DetectorTypeId);
            var detectorTypeName = detectorType == null ? "" : detectorType.Name;
            var fireunit = await _fireUnitRep.FirstOrDefaultAsync(p => p.Id == detector.FireUnitId);
            DataApi.UpdateEvent(new GovFire.Dto.EventDto()
            {
                id = breakdownId.ToString(),
                state = breakdown.HandleStatus == 3 ? "1" : "0",
                createtime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                donetime = "",
                eventcontent = $"{input.FaultRemark},{detector.Identify},{detectorTypeName}",
                eventtype = BreakDownWords.GetSource(breakdown.Source),
                firecompany = fireunit == null ? "" : fireunit.Name,
                lat = fireunit == null ? "" : fireunit.Lat.ToString(),
                lon = fireunit == null ? "" : fireunit.Lng.ToString(),
                fireUnitId = ""
            });

            //var detectorType = _deviceManager.GetDetectorType(input.DetectorGBType);
            //var fireunit = await _repFireUnit.FirstOrDefaultAsync(detector.FireUnitId);
            //DataApi.UpdateEvent(new GovFire.Dto.EventDto()
            //{
            //    id = "123456789",
            //    state = "0",
            //    createtime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
            //    donetime = "",
            //    eventcontent = "线路损坏",
            //    eventtype = "有源部件故障监测",
            //    firecompany = "未来中心",
            //    lat = "30.656422",
            //    lon = "104.102073"
            //});

            return new AddDataOutput()
            {
                IsDetectorExit = true
            };
        }
        public IQueryable<Fault> GetFaultDataAll()
        {
            return _faultRep.GetAll();
        }
        public IQueryable<Fault> GetFaultDataMonth(int year, int month)
        {
            return _faultRep.GetAll().Where(p => p.CreationTime.Year == year && p.CreationTime.Month == month);
        }
        public IQueryable<PendingFaultFireUnitOutput> GetPendingFaultFireUnits()
        {
            return from a in _faultRep.GetAll().GroupBy(p => p.FireUnitId).Select(p => new
                    {
                        FireUnitId = p.Key,
                        FaultCount = p.Count(),
                        PendingCount = p.Where(p1 => p1.ProcessState == 0).Count()
                    })
                   join b in _fireUnitRep.GetAll()
                   on a.FireUnitId equals b.Id
                   select new PendingFaultFireUnitOutput()
                   {
                       FireUnitId = a.FireUnitId,
                       FireUnitName = b.Name,
                       Count = a.FaultCount,
                       PendingCount = a.PendingCount
                   };
        }
        public IQueryable<Fault> GetFaults(IQueryable<Detector> detectors, DateTime start, DateTime end)
        {
            return from a in detectors
                   join b in _faultRep.GetAll().Where(p => p.CreationTime >= start && p.CreationTime <= end)
                   on a.Id equals b.DetectorId
                   select b;
        }
    }
}
