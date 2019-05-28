using Abp.Domain.Repositories;
using FireProtectionV1.FireWorking.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FireProtectionV1.FireWorking.Manager
{
    public class DetectorManager: IDetectorManager
    {
        IRepository<Gateway> _gatewayRep;
        IRepository<Detector> _detectorRep;
        public DetectorManager(IRepository<Detector> detectorRep,
            IRepository<Gateway> gatewayRep)
        {
            _detectorRep = detectorRep;
            _gatewayRep = gatewayRep;
        }
        public void AddDetector(int detectorTypeId,byte sysTypeId,string name,int gatewayId,int fireunitId)
        {
            _detectorRep.Insert(new Detector()
            {
                DetectorTypeId = detectorTypeId,
                FireSysType = sysTypeId,
                Name = name,
                GatewayId = gatewayId,
                FireUnitId = fireunitId
            });
        }
    }
}
